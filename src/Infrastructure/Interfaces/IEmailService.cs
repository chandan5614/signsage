using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string body);
        Task SendEmailAsync(string to, string subject, string body, string attachmentFilePath);
    }
}
