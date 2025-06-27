using AppAPI.IRepository;
using AppAPI.ModelRestPassword;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace AppAPI.Repository
{
    public class EmailRepo : IEmailRepo
    {
        private readonly SmtpSettings _smtpSettings;
        public EmailRepo(IOptions<SmtpSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings.Value ?? throw new ArgumentNullException(nameof(smtpSettings));
        }
        public async Task SendVerificationCode(string toEmail, string code)
        {
            // Kiểm tra nếu thông tin email gửi và nhận bị trống hoặc null
            if (string.IsNullOrWhiteSpace(_smtpSettings.From) || string.IsNullOrWhiteSpace(toEmail))
            {
                throw new ArgumentNullException("Email address cannot be null or empty.");
            }

            // Khởi tạo client SmtpClient với thông tin cấu hình
            using var client = new SmtpClient(_smtpSettings.Host, _smtpSettings.Port)
            {
                EnableSsl = _smtpSettings.EnableSsl,
                Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password)
            };

            try
            {
                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_smtpSettings.From),
                    Subject = "MÃ XÁC MINH MẬT KHẨU",
                    Body = $"Mã xác minh của bạn là: {code} - ĐOẠN MÃ NÀY CHỈ TỒN TẠI TRONG 3 PHÚT",
                    IsBodyHtml = true
                };

                // Thêm địa chỉ nhận
                mailMessage.To.Add(toEmail);

                // Gửi email
                await client.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                // Log hoặc xử lý lỗi gửi email (tùy thuộc vào yêu cầu của bạn)
                throw new InvalidOperationException("Có lỗi xảy ra khi gửi email.", ex);
            }
        }
    }
}
