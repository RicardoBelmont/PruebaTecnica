using PruebaTecnica.Domain.Entities;

namespace PruebaTecnica.Domain.Repositories
{
    public interface IUserValidatorRules
    {
        bool ValidateCredentials(Auth request);
    }
}
