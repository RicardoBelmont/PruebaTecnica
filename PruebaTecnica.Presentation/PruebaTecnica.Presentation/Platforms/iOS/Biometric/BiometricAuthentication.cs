using Foundation;
using LocalAuthentication;

namespace PruebaTecnica.Presentation.NativeServices.Biometric
{

    public partial class BiometricAuthentication
    {
        private LAContext _context;
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

        partial void Init()
        {
            _context = new LAContext();
            isInitialized = true;
            isAvailable = GetAvailability() == BiometricAvailability.Available;
            switch (_context.BiometryType)
            {
                case LABiometryType.FaceId:
                    biometricType = BiometricType.FaceID;
                    break;
                case LABiometryType.TouchId:
                    biometricType = BiometricType.TouchID;
                    break;
                default:
                    biometricType = BiometricType.None;
                    break;
            }
        }

        public partial BiometricAvailability GetAvailability()
        {
            if (_context.CanEvaluatePolicy(LAPolicy.DeviceOwnerAuthenticationWithBiometrics, out NSError error))
                return BiometricAvailability.Available;
            if (error == null)
                return BiometricAvailability.Unknown;

            switch (error.Code)
            {
                case (int)LAStatus.BiometryLockout: return BiometricAvailability.Lockout;
                case (int)LAStatus.BiometryNotEnrolled: return BiometricAvailability.NoFingerprints;
                case (int)LAStatus.PasscodeNotSet: return BiometricAvailability.NoSecuredDevice;
                case (int)LAStatus.BiometryNotAvailable: return BiometricAvailability.NoHardware;
                default: return BiometricAvailability.Unknown;
            }
        }

        public partial void Authenticate(string reason, Action<BiometricStatusResult> callback)
        {
            try
            {
                _context.LocalizedFallbackTitle = string.Empty;
                var replyHandler = new LAContextReplyHandler((success, error) =>
                {
                    BiometricStatusResult status = BiometricStatusResult.Error;

                    if (success)
                    {
                        status = BiometricStatusResult.Success;
                    }
                    else
                    {
                        if (error != null)
                        {
                            switch (error.Code)
                            {
                                case (int)LAStatus.AppCancel: status = BiometricStatusResult.Canceled; break;
                                case (int)LAStatus.SystemCancel: status = BiometricStatusResult.Canceled; break;
                                case (int)LAStatus.UserCancel: status = BiometricStatusResult.Canceled; break;
                                case (int)LAStatus.AuthenticationFailed: status = BiometricStatusResult.Failed; break;
                                case (int)LAStatus.BiometryLockout: status = BiometricStatusResult.TooManyAttempts; break;
                            }
                        }
                    }

                    callback.Invoke(status);
                });

                _context.EvaluatePolicy(LAPolicy.DeviceOwnerAuthenticationWithBiometrics, reason, replyHandler);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

    }
}
