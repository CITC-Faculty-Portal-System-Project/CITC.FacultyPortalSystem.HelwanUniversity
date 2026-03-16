using Shared.Dtos.Auth;
using Shared.Dtos.IdentityModule;

namespace Services.Abstraction.Contracts
{
    public interface IAuthenticationService
    {
        //Register ==> return RegisterResultDto [UserName, Token, Email, Password] ==> Take Parameters [NationalNumber]
        Task<UserResultDto> RegisterAsync(RegisterDto registerDto);

        //Login ==> return UserResultDto [UserName, Token, Email] ==> Take Parameters [NationalNumber]
        Task<LoginClaims> LoginAsync(LoginDto loginDto);

        //Get Current User
        Task<UserResultDto> GetCurrentUserAsync(string userEmail);

        //Check If Email Exists
        Task<bool> CheckEmailExistAsync(string userEmail);

        //Check If User with National Number Exists and Get Email
        public Task<UserRegistrationClientDto> GetUserInfoFromExternalService(string nationalNumber);

        //Confirm Email (Send OTP Email)
        Task ConfirmEmail(string userEmail);

        //Verify OTP 
        Task<bool> VerifyOTPAsync(OTPSendDTO otpSendDto);

        //Reset Password
        Task<bool> ResetPasswordAsync(ResetPasswordDto passwordDto);

        //Getting Current Logged User Email Without the need from user to pass the email to end-point
        public string GetLoggedUserEmail();
        
    }
}
