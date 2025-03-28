using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Identity.Client;
using PruebaTecnica.Application.ApplicationServices;
using PruebaTecnica.Presentation.Controls;
using PruebaTecnica.Presentation.NativeServices.Biometric;
using PruebaTecnica.Presentation.NativeServices.MSAL;

namespace PruebaTecnica.Presentation.ViewModel
{
    public partial class LoginViewModel:ViewModelBase
    {
        private readonly BiometricHelper biometricHelper;
        private readonly IUserValidationService _userValidationService;
        private readonly MSALManager _manager;

        [ObservableProperty]
        string? email;

        [ObservableProperty]
        string? password;

        [ObservableProperty]
        bool isBiometricActivated;

        [ObservableProperty]
        ImageSource biometricIcon;

        [ObservableProperty]
        string biometricLegend;

        public LoginViewModel(IUserValidationService _userValidationService, BiometricHelper biometricHelper, IPublicClientApplication PCA)
        {
            _manager = new MSALManager(PCA);
            this._userValidationService = _userValidationService;
            this.biometricHelper = biometricHelper;
            IsBiometricActivated = biometricHelper.IsBiometricLoginActivated;
            if (IsBiometricActivated && biometricHelper.IsBiometricAvailable)
            {
                biometricIcon = biometricHelper.BiometricType == BiometricType.FaceID ?
                    (string)App.Current!.Resources["Faceid"] :
                    (string)App.Current!.Resources["Touchid"];
                biometricLegend = biometricHelper.BiometricType == BiometricType.FaceID ?
                    Labels.Labels.Login_FaceID : Labels.Labels.Login_TouchID;
            }
        }

        [RelayCommand]
        async Task OnLogin()
        {

            bool isValid = _userValidationService.ValidateCredentials(new Application.DTO.AuthDTO()
            {
                Email = Email,
                Password = Password
            });

            // Validate user default rules (dummy)
            if (!isValid)
            {
                _ = CustomToast.DisplayToast(Labels.Labels.EmptyLogin);
                return;
            }

            try
            {
                IsBusy = true;
                if (!IsBiometricActivated && biometricHelper.IsBiometricAvailable)
                {
                    string title = string.Empty;
                    string question = string.Empty;
                    switch (biometricHelper.BiometricType)
                    {
                        case BiometricType.FaceID:
                            {
                                title = Labels.Labels.Login_FaceID;
                                question = string.Format(Labels.Labels.BiometricLoginQuestion, Labels.Labels.FaceID);
                            }
                            break;
                        case BiometricType.TouchID:
                            {
                                title = Labels.Labels.Login_TouchID;
                                question = string.Format(Labels.Labels.BiometricLoginQuestion, Labels.Labels.TouchID);
                            }
                            break;
                        case BiometricType.Fingerprint:
                            {
                                title = Labels.Labels.Login_Fingerprint;
                                question = string.Format(Labels.Labels.BiometricLoginQuestion, Labels.Labels.Fingerprint);
                            }
                            break;
                    }
                    if (await Shell.Current.DisplayAlert(title, question, Labels.Labels.Yes, Labels.Labels.No))
                    {
                        var result = await biometricHelper.ActivateBiometricLogin(Email, Password);
                        IsBiometricActivated = true;
                        BiometricIcon = biometricHelper.BiometricType == BiometricType.FaceID ?
                            (string)App.Current!.Resources["Faceid"] :
                            (string)App.Current!.Resources["Touchid"];
                    }
                }

                Email = string.Empty;
                Password = string.Empty;

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        public void OnLoginWithBiometric()
        {
            try
            {
                if (!IsBiometricActivated) return;
                biometricHelper.Authenticate(Labels.Labels.BiometricLogin, async (email, password, success) =>
                {
                    if (success)
                    {
                        //App.Current!.Dispatcher.Dispatch(async () => await OnLogin());
                        Email = email;
                        Password = password;
                    }
                    else
                    {
                        _ = Shell.Current.DisplayAlert(Labels.Labels.Error, Labels.Labels.Error, Labels.Labels.OK);
                        IsBiometricActivated = false;
                    }
                });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                Shell.Current.DisplayAlert(Labels.Labels.Error, ex.Message, Labels.Labels.OK);
            }
        }

        [RelayCommand]
        public async Task OnLoginWithMSAL()
        {
            try
            {
                var token = await _manager.Authenticate();
                await Shell.Current.DisplayActionSheet("token", token, "OK");
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
    }
}
