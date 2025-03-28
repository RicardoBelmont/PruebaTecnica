using AndroidX.Biometric;
using AndroidX.Core.Content;
using AndroidX.Fragment.App;
using PruebaTecnica.Presentation.Platforms.Android.Biometric;

namespace PruebaTecnica.Presentation.NativeServices.Biometric
{

    public partial class BiometricAuthentication
    {
        public partial bool IsInitialized()
        {
            return isInitialized;
        }

        public partial bool IsAvailable()
        {
            return isAvailable;
        }

        public partial BiometricType GetBiometricType()
        {
            return biometricType;
        }

        public partial BiometricAvailability GetAvailability()
        {
            if (!isInitialized) return BiometricAvailability.NoSecuredDevice;
            if (!OperatingSystem.IsAndroidVersionAtLeast(23)) return BiometricAvailability.NoOSVersionSupported;

            var biometricManager = BiometricManager.From(Android.App.Application.Context);

            // Weak makes easier to match the finger/face, but with less security
            var status = biometricManager.CanAuthenticate(BiometricManager.Authenticators.BiometricWeak);
            if (ContextCompat.CheckSelfPermission(Android.App.Application.Context, Android.Manifest.Permission.UseBiometric) != Android.Content.PM.Permission.Granted
               && ContextCompat.CheckSelfPermission(Android.App.Application.Context, Android.Manifest.Permission.UseFingerprint) != Android.Content.PM.Permission.Granted)
                return BiometricAvailability.NoPermission;
            else if (status == BiometricManager.BiometricErrorHwUnavailable) return BiometricAvailability.NoHardware;
            else if (status == BiometricManager.BiometricErrorNoHardware) return BiometricAvailability.NoHardware;
            else if (status == BiometricManager.BiometricErrorNoneEnrolled) return BiometricAvailability.NoFingerprints;
            else if (status == BiometricManager.BiometricErrorUnsupported) return BiometricAvailability.NoOSVersionSupported;

            return BiometricAvailability.Available;
        }

        public partial void Authenticate(string reason, Action<BiometricStatusResult> result)
        {
            if (!isInitialized || !isAvailable) return;
            try
            {
                var context = Android.App.Application.Context;
                var executor = ContextCompat.GetMainExecutor(context);
                var callback = new AuthenticationCallback(result);
                var promtInfo = new BiometricPrompt.PromptInfo.Builder()
                    .SetTitle(Labels.Labels.BiometricPromptTitle)
                    .SetDescription(Labels.Labels.BiometricPromptDescription)
                    .SetNegativeButtonText(Labels.Labels.Cancel)
                    .Build();

                var fActivity = Platform.CurrentActivity as FragmentActivity;
                var prompt = new AndroidX.Biometric.BiometricPrompt(fActivity!, executor, callback);
                prompt.Authenticate(promtInfo);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        partial void Init()
        {
            isInitialized = true;
            var availability = GetAvailability();
            isAvailable = availability == BiometricAvailability.Available;
            biometricType = (
                availability == BiometricAvailability.Available ||
                availability == BiometricAvailability.NoFingerprints ||
                availability == BiometricAvailability.NoPermission) ? BiometricType.Fingerprint : BiometricType.None;
        }
    }
}
