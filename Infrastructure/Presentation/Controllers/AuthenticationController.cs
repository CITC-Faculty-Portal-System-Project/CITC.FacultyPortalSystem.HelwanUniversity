using Microsoft.AspNetCore.Authorization;
using Services.Abstraction.Contracts;
using Shared.Dtos.IdentityModule;
using System.Security.Claims;

namespace Presentation.Controllers
{
    public class AuthenticationController(IServiceManager _serviceManager) : ApiController
    {
        [ProducesResponseType(typeof(UserResultDto), StatusCodes.Status200OK)]
        [HttpPost("Register")]
        public async Task<ActionResult<UserResultDto>> RegisterAsync([FromQuery]RegisterDto registerDto)
            => Ok(await _serviceManager.AuthenticationService.RegisterAsync(registerDto));

        [ProducesResponseType(typeof(UserResultDto), StatusCodes.Status200OK)]
        [HttpPost("Login")]
        public async Task<ActionResult<string>> LoginAsync(LoginDto loginDto)
        {
            var token = await _serviceManager.AuthenticationService.LoginAsync(loginDto);
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(30)
            };

            Response.Cookies.Append("jwtToken", token, cookieOptions);
            return Ok("Logged In");
        }



        [ProducesResponseType(typeof(ResetPasswordDto), StatusCodes.Status200OK)]
        [HttpPost("ResetPassword")]
        public async Task<ActionResult> ResetPassword(ResetPasswordDto resetPasswordDto, string email)
            => Ok(await _serviceManager.AuthenticationService.ResetPasswordAsync(resetPasswordDto, email));

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("ConfirmEmail")]
        public async Task<ActionResult> ConfirmEmail(string userEmail)
        {
            await _serviceManager.AuthenticationService.ConfirmEmail(userEmail);
            return Ok(new ApiResponseHandler($"OTP Sent To {userEmail} Succefully."));
        }
           
        [ProducesResponseType(typeof(OTPSendDTO), StatusCodes.Status200OK)]
        [HttpPost("VerifyOTP")]
        public async Task<ActionResult> VerifyOTP(OTPSendDTO otpSendDto, string email)
            => Ok(await _serviceManager.AuthenticationService.VerifyOTPAsync(otpSendDto, email));

        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [HttpGet("EmailExist")]
        public async Task<ActionResult<bool>> CheckEmailExistAsync(string email)
            => Ok(await _serviceManager.AuthenticationService.CheckEmailExistAsync(email));

        [ProducesResponseType(typeof(UserResultDto), StatusCodes.Status200OK)]
        [Authorize]
        [HttpGet("GetCurrentUser")]
        public async Task<ActionResult<UserResultDto>> GetCurrentUserAsync()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _serviceManager.AuthenticationService.GetCurrentUserAsync(email ?? "");
            return Ok(user);
        }
    }
}
