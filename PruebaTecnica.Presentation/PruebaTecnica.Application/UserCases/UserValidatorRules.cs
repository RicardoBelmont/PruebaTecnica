using PruebaTecnica.Domain.Entities;
using PruebaTecnica.Domain.Repositories;

namespace PruebaTecnica.Application.UserCases
{
    public class UserValidatorRules : IUserValidatorRules
    {
        /// <summary>
        /// Validates request structure
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public bool ValidateCredentials(Auth request)
        {
            return !string.IsNullOrWhiteSpace(request.Email) && !string.IsNullOrWhiteSpace(request.Password);
        }
    }
}
