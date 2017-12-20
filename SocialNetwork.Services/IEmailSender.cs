using System.Threading.Tasks;

namespace SocialNetwork.Services
{
    public interface IEmailSender : IService
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}