using CommonAPIs.DTOs;
using CommonAPIs.Services;
using Microsoft.AspNetCore.Mvc;

namespace CommonAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommonAPIController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IOtpService _otpService;

        public CommonAPIController(IUserService userService, IOtpService otpService)
        {
            _userService = userService;
            _otpService = otpService;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDto requestDto)
        {
            var result = await _userService.RegisterAsync(requestDto);
            if (result.Message == "User already exists")
                return Conflict(result);

            return Ok(result);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestDto loginDto)
        {
            try
            {
                var response = _userService.Login(loginDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequestDto request)
        {
            var result = await _userService.ForgotPasswordAsync(request);
            return Ok(result);
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequestDto request)
        {
            var result = await _userService.ResetPasswordAsync(request);
            if (!result)
                return NotFound(new { message = "User not found or invalid email." });

            return Ok(new { message = "Password updated successfully." });
        }

        [HttpPost("send-otp")]
        public async Task<IActionResult> SendOtp([FromBody] SendOtpRequestDto request)
        {
            try
            {
                var otp = await _otpService.SendOtpAsync(request);
                return Ok(new { message = "OTP sent successfully", otp }); // In production, don't return the OTP
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }


    }
}
