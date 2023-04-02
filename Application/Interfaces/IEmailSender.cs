using Application.Models;

namespace Application.Interfaces;

public interface IEmailSender
{
    Task SendEmailAsync(EmailMessage message);
}