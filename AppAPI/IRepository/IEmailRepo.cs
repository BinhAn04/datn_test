namespace AppAPI.IRepository
{
    public interface IEmailRepo
    {
        Task SendVerificationCode(string toEmail, string code);
    }
}
