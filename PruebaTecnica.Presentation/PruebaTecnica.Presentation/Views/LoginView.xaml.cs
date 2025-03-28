using PruebaTecnica.Presentation.ViewModel;

namespace PruebaTecnica.Presentation.Views;

public partial class LoginView : ContentPage
{
	public LoginView(LoginViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}