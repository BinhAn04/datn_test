namespace AppAPI.IService
{
    public interface  IEmailService
    {
        Task SendVerificationCode(string email, string code);
    }
}
