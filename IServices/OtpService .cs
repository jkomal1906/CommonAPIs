using CommonAPIs.DTOs;
using CommonAPIs.Services;

namespace CommonAPIs.ImpService
{
    public class OtpService : IOtpService
    {
        private readonly IEmailService _emailService;

        public OtpService(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task<string> SendOtpAsync(SendOtpRequestDto request)
        {
            // Generate a random 6-digit OTP
            var otp = new Random().Next(100000, 999999).ToString();

            if (!string.IsNullOrEmpty(request.Email))
            {
                string subject = "Your OTP Code";
                string body = $"Your OTP code is <b>{otp}</b>. It is valid for 5 minutes.";
                await _emailService.SendEmailAsync(request.Email, subject, body);
            }

            // You can also integrate with Twilio or any SMS service if PhoneNumber is provided

            return otp;
        }
    }
}
