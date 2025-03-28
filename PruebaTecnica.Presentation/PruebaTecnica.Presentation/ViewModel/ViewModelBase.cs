using CommunityToolkit.Mvvm.ComponentModel;

namespace PruebaTecnica.Presentation.ViewModel
{
    public partial class ViewModelBase:ObservableObject
    {
        [ObservableProperty]
        bool isBusy;

        public ViewModelBase()
        {
            this.PropertyChanged += ViewModelBase_PropertyChanged;
        }

        private void ViewModelBase_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            //if (e.PropertyName == nameof(IsBusy))
            //{
            //    if (IsBusy)
            //    {
            //        lpv = new LoaderPopupView();
            //        lpv.IsBusy = true;
            //    }
            //    else if (!IsBusy && lpv != null)
            //        lpv.IsBusy = false;
            //}
        }
    }
}
