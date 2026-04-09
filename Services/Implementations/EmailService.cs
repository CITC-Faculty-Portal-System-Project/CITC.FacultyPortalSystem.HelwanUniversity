using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Shared.Enums.Logging;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Reflection;
using System.Text;

namespace Services.Implementations
{
    public class EmailService(IConfiguration _configuration, ICacheService _cacheService, ILogger<EmailService> _logger) : IEmailService
    {
        #region Helper Methods

        //CreateSmtpClient
        private SmtpClient CreateSmtpClient()
        {
            var smtpSection = _configuration.GetSection("SmtpSettings");
            var host = smtpSection["Host"] ?? throw new InvalidOperationException("SMTP Host not configured");
            var port = int.Parse(smtpSection["Port"] ?? throw new InvalidOperationException("SMTP Port not configured"));
            var user = smtpSection["UserName"] ?? throw new InvalidOperationException("SMTP Username not configured");
            var pass = smtpSection["Password"] ?? throw new InvalidOperationException("SMTP Password not configured");
            var enableSsl = bool.Parse(smtpSection["EnableSsl"] ?? "true");

            return new SmtpClient(host, port)
            {
                Credentials = new NetworkCredential(user, pass),
                EnableSsl = enableSsl
            };
        }

        private string BuildBaseLayout(string title, string bodyContent)
        {
               return $@"
                <!doctype html>
                <html lang=""ar"" dir=""rtl"">
                <head>
                  <meta charset=""utf-8"">
                  <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                  <style>
                    body {{
                      margin: 0; padding: 0;
                      font-family: Tahoma, Arial; direction: rtl;
                      text-align: right; font-size: 18px;
                    }}
                    .container {{
                      max-width: 800px; margin: 40px auto;
                      background: #fff; border-radius: 10px;
                      border: 1px solid #ddd;
                      direction: rtl; text-align: right;
                    }}
                    .header {{
                      background-color:#19355a; color:white;
                      text-align:center; padding:30px 20px;
                    }}
                    .header img {{ 
                      max-height: 200px;   
                      width: auto;
                      margin-bottom: 20px;
                    }}
                    .content {{
                      padding:30px; line-height:2; color:#333;
                      direction: rtl; text-align: right;
                      unicode-bidi: plaintext;
                    }}
                    .footer {{
                      text-align:center; padding:15px; background:#f2f2f2;
                      font-size:14px; color:#666;
                    }}
                  </style>
                </head>

                <body style=""margin:0;padding:0;background-color:#f5f7fb;direction:rtl;text-align:right;font-family:Tahoma,Arial;font-size:18px;"">
                  <div class=""container"" style=""max-width:800px;margin:40px auto;background:#fff;border-radius:10px;border:1px solid #ddd;"">
                      <div class=""header"" style=""background-color:#19355a;color:white;text-align:center;padding:30px 20px;"">
                        <img src=""cid:helwanLogo"" alt=""Helwan University"" style=""max-height:160px;width:auto;margin-bottom:20px;""/>
                        <h2 style=""margin:0;"">{title}</h2>
                      </div>

                      <div class=""content"" style=""padding:30px;line-height:2;color:#333;direction:rtl;text-align:right;unicode-bidi:plaintext;"">
                        {bodyContent}
                      </div>

                      <div class=""footer"" style=""text-align:center;padding:15px;background:#f2f2f2;font-size:14px;color:#666;"">
                        جميع الحقوق محفوظة - مركز الاتصالات و تكنولوجيا المعلومات © {DateTime.Now.Year}
                      </div>
                  </div>
                </body>
                </html>";
        }


        //BuildCredintialBody
        private string BuildCredentialsBody(string userName, string password)
        {
          return $@"
            <p>السيد/ة عضو هيئة التدريس،</p>
            <p>مرفق بيانات الدخول الخاصة بك:</p>

            <div style=""background:#f9f9f9;border:1px solid #e0e0e0;border-radius:8px;padding:20px;margin:25px 0;"">
              <p><strong>اسم المستخدم:</strong> {userName}</p>
              <p><strong>كلمة المرور:</strong> {password}</p>
            </div>

            <p>يرجى تغيير كلمة المرور بعد تسجيل الدخول.</p>
            <p style=""color:#b38e19;font-weight:bold;text-align:center;margin-top:20px;"">
              مركز الاتصالات وتكنولوجيا المعلومات - جامعة العاصمة
            </p>";
        }


        //BuildOTPBody
        private string BuildOTPBody(int otp)
        {
         return $@"
            <p>تم إنشاء رمز إعادة تعيين كلمة المرور الخاص بك:</p>

            <div style=""background:#f9f9f9;border:1px solid #e0e0e0;border-radius:8px;padding:20px;margin:25px 0;"">
              <p><strong>الرمز:</strong> {otp}</p>
            </div>

            <p style=""color:#b38e19;font-weight:bold;text-align:center;margin-top:20px;"">
              مركز الاتصالات وتكنولوجيا المعلومات - جامعة العاصمة
            </p>";
        }
        //SendAsync
        private async Task SendAsync(string to, string subject, string htmlBody)
        {
            var emailLog = new LogEntry
			{
				Category = Category.Authentication.ToString(),
				CategoryAction = CategoryAction.SendEmail.ToString(),
			};

			if (string.IsNullOrWhiteSpace(to))
            {
                #region Log
                emailLog.Timestamp = DateTime.Now;
				emailLog.Level = "Warning";
				emailLog.RenderedMessage = "Attempted to send email with empty recipient address.";
				emailLog.AdditionalData = "The SendAsync method was called with an empty or whitespace 'to' parameter, which is required for sending an email. This may indicate a bug in the code that calls SendAsync or an issue with how email addresses are being retrieved or passed to this method.";
				_logger.LogWarning("{@LogDetails}", emailLog); 
                #endregion
            }
                
            using var client = CreateSmtpClient();
            using var mail = new MailMessage
            {
                From = new MailAddress("no-reply@capu.edu.eg" , "Capital University Faculty Portal" ?? throw new InvalidOperationException("SMTP From not configured")),
                Subject = subject,
                IsBodyHtml = true
            };

            mail.To.Add(to);

            var view = AlternateView.CreateAlternateViewFromString(htmlBody, Encoding.UTF8, MediaTypeNames.Text.Html);

            // Embedded logo
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "Services.Assets.EmailResources.Capital_University_Logo.png"; // Ensure proper namespace
            using var logoStream = assembly.GetManifestResourceStream(resourceName);
            if (logoStream != null)
            {
                var logo = new LinkedResource(logoStream, MediaTypeNames.Image.Png)
                {
                    ContentId = "helwanLogo",
                    TransferEncoding = TransferEncoding.Base64
                };
                view.LinkedResources.Add(logo);
            }

            mail.AlternateViews.Add(view);
            await client.SendMailAsync(mail);
		}
        #endregion

        public async Task SendCredentialsAsync(Guid userId, string userName, string password)
        {
            var credentialsEmailLog = new LogEntry
            {
				Category = Category.Authentication.ToString(),
				CategoryAction = CategoryAction.SendCredentialsByEmail.ToString(),
			};

            string? email = await _cacheService.GetCachedValueAsync($"auth:email:{userId}");
            if (string.IsNullOrWhiteSpace(email))
            {
                #region Log
                credentialsEmailLog.Timestamp = DateTime.Now;
				credentialsEmailLog.Level = "Error";
				credentialsEmailLog.RenderedMessage = $"Email not found in cache for user: {userName}";
                credentialsEmailLog.AdditionalData = $"Attempted to send credentials email for user ID {userId} but no email was found in cache. This may indicate a caching issue or that the email was never cached for this user.";
                _logger.LogError("{@LogDetails}", credentialsEmailLog);
				#endregion
				throw new InvalidOperationException("Email not found in cache");
			}

            var content = BuildCredentialsBody(userName, password);
            var html = BuildBaseLayout("بوابة أعضاء هيئة التدريس", content);

            await SendAsync(email, "بيانات الدخول الخاصة بك", html);
            #region Log
            credentialsEmailLog.Timestamp = DateTime.Now;
			credentialsEmailLog.Level = "Information";
			credentialsEmailLog.RenderedMessage = $"Credentials email sent successfully to {email} for user {userName}.";
			credentialsEmailLog.AdditionalData = $"Sent credentials email to {email} for user Id {userId} with username : {userName} / password : {password} . This email contains the user's login credentials.";
			_logger.LogInformation("{@LogDetails}", credentialsEmailLog);
			#endregion
		}

        public async Task SendOTPAsync(string email)
        {
            var otpLog = new LogEntry
            {
                Category = Category.Authentication.ToString(),
				CategoryAction = CategoryAction.SendOTP.ToString(),
			};

            if (string.IsNullOrWhiteSpace(email))
            {
                #region Log
                otpLog.Timestamp = DateTime.Now;
				otpLog.Level = "Warning";
				otpLog.RenderedMessage = "Attempted to send OTP email with empty recipient address.";
				otpLog.AdditionalData = "The SendOTPAsync method was called with an empty or whitespace 'email' parameter, which is required for sending an OTP email.";
				_logger.LogWarning("{@LogDetails}", otpLog);
				#endregion
			}
                
            int otp = Random.Shared.Next(100000, 999999);

            var content = BuildOTPBody(otp);
            var html = BuildBaseLayout("رمز إعادة تعيين كلمة المرور", content);

            await SendAsync(email, "رمز إعادة التعيين", html);

            #region Log
            otpLog.Timestamp = DateTime.Now;
			otpLog.Level = "Information";
			otpLog.RenderedMessage = $"OTP email sent successfully to {email}.";
			otpLog.AdditionalData = $"Sent OTP email to {email} with OTP: {otp}. This email contains a one-time password for resetting the user's password.";
			_logger.LogInformation("{@LogDetails}", otpLog);
			#endregion

			await _cacheService.SetCachedValueAsync($"auth:otp:{otp}", otp.ToString(), TimeSpan.FromMinutes(5));
            await _cacheService.SetCachedValueAsync($"auth:otp:{email.ToLower()}", otp.ToString(), TimeSpan.FromMinutes(5));
            await _cacheService.SetCachedValueAsync($"auth:email:{email.ToLower()}", email, TimeSpan.FromMinutes(15));
        }
    }
}
