using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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
                  margin: 0; padding: 0; background-color: #f5f7fb;
                  font-family: Tahoma, Arial; direction: rtl;
                  text-align: right; font-size: 18px;
                }}
                .container {{
                  max-width: 800px; margin: 40px auto;
                  background: #fff; border-radius: 10px;
                  border: 1px solid #ddd; box-shadow: 0 6px 16px rgba(0,0,0,0.15);
                  direction: rtl; text-align: right;
                }}
                .header {{
                  background-color:#19355a; color:white;
                  text-align:center; padding:30px 20px;
                  direction: rtl;
                }}
                .header img {{ max-height: 100px; margin-bottom: 15px; }}
                .content {{
                  padding:30px; line-height:2; color:#333;
                  direction: rtl; text-align: right;
                  unicode-bidi: plaintext;
                }}
                .footer {{
                  text-align:center; padding:15px; background:#f2f2f2;
                  font-size:14px; color:#666;
                  direction: rtl;
                }}
              </style>
            </head>

            <body dir=""rtl"" style=""margin:0;padding:0;background-color:#f5f7fb;direction:rtl;text-align:right;font-family:Tahoma,Arial;font-size:18px;"">
              <div class=""container"" dir=""rtl"" style=""max-width:800px;margin:40px auto;background:#fff;border-radius:10px;border:1px solid #ddd;box-shadow:0 6px 16px rgba(0,0,0,0.15);direction:rtl;text-align:right;"">
                  <div class=""header"" style=""background-color:#19355a;color:white;text-align:center;padding:30px 20px;direction:rtl;"">
                    <img src=""cid:helwanLogo"" alt=""Helwan University"" style=""max-height:100px;margin-bottom:15px;""/>
                    <h2 style=""margin:0;direction:rtl;"">{title}</h2>
                  </div>

                  <div class=""content"" dir=""rtl"" style=""padding:30px;line-height:2;color:#333;direction:rtl;text-align:right;unicode-bidi:plaintext;"">
                    {bodyContent}
                  </div>

                  <div class=""footer"" style=""text-align:center;padding:15px;background:#f2f2f2;font-size:14px;color:#666;direction:rtl;"">
                    جامعة العاصمة - جميع الحقوق محفوظة © {DateTime.Now.Year}
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
            if (string.IsNullOrWhiteSpace(to))
                _logger.LogWarning("Email Can't be Empty");

            using var client = CreateSmtpClient();
            using var mail = new MailMessage
            {
                From = new MailAddress(_configuration["SmtpSettings:UserName"] ?? throw new InvalidOperationException("SMTP From not configured")),
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
            string? email = await _cacheService.GetCachedValueAsync($"auth:email:{userId}");
            if (string.IsNullOrWhiteSpace(email))
                throw new InvalidOperationException("Email not found in cache");

            var content = BuildCredentialsBody(userName, password);
            var html = BuildBaseLayout("بوابة أعضاء هيئة التدريس", content);

            await SendAsync(email, "بيانات الدخول الخاصة بك", html);
        }

        public async Task SendOTPAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                _logger.LogWarning("Email Can't be Empty");

            int otp = Random.Shared.Next(100000, 999999);

            var content = BuildOTPBody(otp);
            var html = BuildBaseLayout("رمز إعادة تعيين كلمة المرور", content);

            await SendAsync(email, "رمز إعادة التعيين", html);

            await _cacheService.SetCachedValueAsync($"auth:otp:{otp}", otp.ToString(), TimeSpan.FromMinutes(5));
            await _cacheService.SetCachedValueAsync($"auth:email:{email.ToLower()}", email, TimeSpan.FromMinutes(15));
        }
    }
}
