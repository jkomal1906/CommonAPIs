using CommonAPIs.DTOs;

namespace CommonAPIs.Services
{
    public interface IUserService // Changed 'class' to 'interface' to fix CS0501
    {

        // Registration
        Task<RegistrationResponseDto> RegisterAsync(RegistrationRequestDto RegistrationRequestDto);

        // Login
        LoginResponseDto Login(LoginRequestDto loginDto);

        //Forgot Password
        Task<ForgotPasswordResponseDto> ForgotPasswordAsync(ForgotPasswordRequestDto request);

        //Reset Password
        Task<bool> ResetPasswordAsync(ResetPasswordRequestDto request);



    }
}
