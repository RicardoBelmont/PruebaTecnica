namespace PruebaTecnica.Presentation.Resources.Styles;

public partial class Layouts : ResourceDictionary
{
	public Layouts()
	{
		InitializeComponent();
        int x = (int)(DeviceDisplay.Current.MainDisplayInfo.Width / DeviceDisplay.Current.MainDisplayInfo.Density);
        int y = (int)(DeviceDisplay.Current.MainDisplayInfo.Height / DeviceDisplay.Current.MainDisplayInfo.Density);
        int Height = x > y ? x : y;
        int Width = x < y ? x : y;


        this["Spacing"] = Width * 0.047;
        this["Padding"] = new Thickness(16);
        this["Width"] = Width;
        this["Height"] = Height;
        this["DisplayScale"] = DeviceDisplay.Current.MainDisplayInfo.Density;
    }
}