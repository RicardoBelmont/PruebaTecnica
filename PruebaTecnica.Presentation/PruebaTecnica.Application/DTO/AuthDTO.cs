using PruebaTecnica.Domain.Entities;

namespace PruebaTecnica.Application.DTO
{
    public class AuthDTO
    {
        public required string Email { get; set; }
        public required string Password { get; set; }

        public Auth ToDomain()
        {
            return new Auth()
            {
                Email = Email,
                Password = Password
            };
        }

        public static AuthDTO FromDomain(Auth domain)
        {
            return new AuthDTO()
            {
                Email = domain.Email,
                Password = domain.Password
            };
        }
    }
}
