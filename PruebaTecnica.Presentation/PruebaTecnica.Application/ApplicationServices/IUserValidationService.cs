using PruebaTecnica.Application.DTO;

namespace PruebaTecnica.Application.ApplicationServices
{
    public interface IUserValidationService
    {
        bool ValidateCredentials(AuthDTO dto);
    }
}
