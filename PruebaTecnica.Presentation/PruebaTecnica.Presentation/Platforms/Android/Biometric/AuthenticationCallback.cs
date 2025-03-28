using Java.Lang;
using PruebaTecnica.Presentation.NativeServices.Biometric;
using AndroidX.Biometric;

namespace PruebaTecnica.Presentation.Platforms.Android.Biometric
{

    internal class AuthenticationCallback : BiometricPrompt.AuthenticationCallback
    {
        private Action<BiometricStatusResult> _callbackResult;

        public AuthenticationCallback(Action<BiometricStatusResult> callbackResult)
        {
            _callbackResult = callbackResult;
        }

        public override void OnAuthenticationError(int errorCode, ICharSequence errString)
        {
            base.OnAuthenticationError(errorCode, errString);
            if (OperatingSystem.IsAndroidVersionAtLeast(28))
            {
                BiometricStatusResult status;
                switch (errorCode)
                {
                    case 7:
                        status = BiometricStatusResult.TooManyAttempts;
                        break;
                    case 5:
                    case 10:
                        status = BiometricStatusResult.Canceled;
                        break;
                    default:
                        status = BiometricStatusResult.Error;
                        break;
                }

                _callbackResult.Invoke(status);
            }
        }

        public override void OnAuthenticationFailed()
        {
            base.OnAuthenticationFailed();
            _callbackResult.Invoke(BiometricStatusResult.Failed);
        }

        public override void OnAuthenticationSucceeded(BiometricPrompt.AuthenticationResult result)
        {
            base.OnAuthenticationSucceeded(result);
            _callbackResult.Invoke(BiometricStatusResult.Success);
        }
    }
}
