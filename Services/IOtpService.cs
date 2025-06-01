using CommonAPIs.DTOs;

namespace CommonAPIs.Services
{
    public interface IOtpService
    {
        Task<string> SendOtpAsync(SendOtpRequestDto request);

    }
}
