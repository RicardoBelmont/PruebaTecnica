using Microsoft.Identity.Client;

namespace PruebaTecnica.Presentation.NativeServices.MSAL
{
    public partial class MSALManager
    {
        public IPublicClientApplication _client { get; private set; }
        public MSALManager(IPublicClientApplication _client)
        {
            this._client = _client;
        }
        public partial Task<string> Authenticate();
    }
}
