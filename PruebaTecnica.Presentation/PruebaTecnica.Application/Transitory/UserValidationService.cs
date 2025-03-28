using PruebaTecnica.Application.DTO;
using PruebaTecnica.Application.ApplicationServices;
using PruebaTecnica.Domain.Repositories;

namespace PruebaTecnica.Application.Transitory
{
    class UserValidationService : IUserValidationService
    {
        private readonly IUserValidatorRules _domainRules;
        public UserValidationService(IUserValidatorRules _domainRules)
        {
            this._domainRules = _domainRules;
        }
        public bool ValidateCredentials(AuthDTO dto)
        {
            return _domainRules.ValidateCredentials(dto.ToDomain());
        }
    }
}
