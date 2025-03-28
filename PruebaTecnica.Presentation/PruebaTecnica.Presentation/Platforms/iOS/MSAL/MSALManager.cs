using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.Presentation.NativeServices.MSAL
{
    public partial class MSALManager
    {
        
        public partial async Task<string> Authenticate()
        {
            var result = await _client.AcquireTokenInteractive(new string[] { "User.Read" })
                        .ExecuteAsync();
            return result.AccessToken;
        }
    }
}
