using blackpoolgigs.web.Models.ViewModels;

namespace blackpoolgigs.web.Services.Interfaces
{
    public interface IEmail
    {
        void Send(ContactViewModel contact, string ipAddress);
        void Send(ClaimBandViewModel claim, string ipAddress);
    }
}