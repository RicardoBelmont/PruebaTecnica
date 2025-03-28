using Newtonsoft.Json;
using PruebaTecnica.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.Presentation.NativeServices.Biometric
{
    public class BiometricHelper
    {
        const string EMAIL_KEY = "username_key";
        const string BIOMETRIC_KEY = "biometric_key";

        BiometricAuthentication _biometricAuth;
        public bool IsBiometricAvailable => _biometricAuth.IsAvailable();
        public bool IsBiometricLoginActivated { get; private set; }
        public BiometricType BiometricType => _biometricAuth.GetBiometricType();
        public string SavedEmail => Preferences.Get(EMAIL_KEY, string.Empty);

        public BiometricHelper()
        {
            _biometricAuth = new BiometricAuthentication();
            if (Preferences.ContainsKey(BIOMETRIC_KEY))
                IsBiometricLoginActivated = Preferences.Get(BIOMETRIC_KEY, false);
        }

        public async Task<bool> ActivateBiometricLogin(string email, string password)
        {
            try
            {
                var creds = new Auth()
                {
                    Email = email,
                    Password = password
                };

                await SecureStorage.SetAsync(email, JsonConvert.SerializeObject(creds));
                Preferences.Set(EMAIL_KEY, email);
                Preferences.Set(BIOMETRIC_KEY, true);
                IsBiometricLoginActivated = true;
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return false;
            }
        }

        public bool DeactivateBiometricLogin()
        {
            try
            {
                if (!IsBiometricLoginActivated) return true;
                var email = Preferences.Get(EMAIL_KEY, string.Empty) as string;
                if (string.IsNullOrWhiteSpace(email)) return true;
                var result = SecureStorage.Remove(email);
                if (result)
                {
                    Preferences.Remove(EMAIL_KEY);
                    Preferences.Remove(BIOMETRIC_KEY);
                    IsBiometricLoginActivated = false;
                }
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return false;
            }
        }

        public void Authenticate(string reason, Action<string, string, bool> credential)
        {
            if (!Preferences.ContainsKey(EMAIL_KEY)) return;
            if (_biometricAuth.IsAvailable())
            {
                _biometricAuth.Authenticate(reason, async (result) =>
                {
                    try
                    {
                        if (result == BiometricStatusResult.Success)
                        {
                            string username = Preferences.Get(EMAIL_KEY, string.Empty) as string;
                            var js = await SecureStorage.GetAsync(username);
                            var credentials = JsonConvert.DeserializeObject<Auth>(js!);
                            credential.Invoke(credentials.Email, credentials.Password, true);
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                        Preferences.Remove(EMAIL_KEY);
                        Preferences.Remove(BIOMETRIC_KEY);
                        credential.Invoke(string.Empty, string.Empty, false);
                    }
                });
            }
        }

    }
}
