

namespace PruebaTecnica.Presentation.NativeServices.Biometric
{
    public partial class BiometricAuthentication
    {
        bool isInitialized;
        bool isAvailable;
        BiometricType biometricType;

        public partial bool IsInitialized();
        public partial bool IsAvailable();
        public partial BiometricType GetBiometricType();

        partial void Init();

        public partial BiometricAvailability GetAvailability();

        public partial void Authenticate(string reason, Action<BiometricStatusResult> callback);

        public BiometricAuthentication()
        {
            Init();
        }
    }
}
