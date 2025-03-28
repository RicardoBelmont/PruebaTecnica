using Microsoft.Maui.Controls.PlatformConfiguration;
using PruebaTecnica.Presentation.NativeServices.Biometric;

namespace PruebaTecnica.Presentation.NativeServices.Biometric
{
    public partial class BiometricAuthentication
    {
        public partial bool IsInitialized()
        {
            return false;
        }

        public partial bool IsAvailable()
        {
            return false;
        }

        public partial BiometricType GetBiometricType()
        {
            return biometricType;
        }

        public partial BiometricAvailability GetAvailability()
        {
            return BiometricAvailability.NoHardware;

        }

        public partial void Authenticate(string reason, Action<BiometricStatusResult> result)
        {

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
