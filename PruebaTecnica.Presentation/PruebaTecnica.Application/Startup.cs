using PruebaTecnica.Application.ApplicationServices;
using PruebaTecnica.Application.Transitory;
using PruebaTecnica.Application.UserCases;
using PruebaTecnica.Domain.Repositories;

namespace PruebaTecnica.Application
{
    public class Startup
    {
        public List<Tuple<Type, Type>> RegisterServices()
        {
            List<Tuple<Type, Type>> MyServices = new List<Tuple<Type, Type>>();

            // Application 
            MyServices.Add(new Tuple<Type, Type>(typeof(IUserValidationService), typeof(UserValidationService)));

            // Rules
            MyServices.Add(new Tuple<Type, Type>(typeof(IUserValidatorRules), typeof(UserValidatorRules)));


            return MyServices;
        }
    }
}
