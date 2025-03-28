namespace PruebaTecnica.Presentation.NativeServices.MSAL
{
    public partial class MSALManager
    {
        public partial async Task<string> Authenticate()
        {
            var result = await _client.AcquireTokenInteractive(new string[] { "User.Read" })
                         .WithParentActivityOrWindow(Platform.CurrentActivity)
                         .WithUseEmbeddedWebView(true)
                         .ExecuteAsync()
                         .ConfigureAwait(true);

            return result.AccessToken;
        }
    }
}
