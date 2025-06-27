using AppAPI.IRepository;
using AppAPI.IService;

namespace AppAPI.Service
{
    public class EmailService : IEmailService
    {
        private readonly IEmailRepo _emailRepo;
        public EmailService(IEmailRepo emailRepo)
        {
            _emailRepo = emailRepo;
        }
        public async Task SendVerificationCode(string email, string code)
        {
            await _emailRepo.SendVerificationCode(email, code);
        }
    }
}
