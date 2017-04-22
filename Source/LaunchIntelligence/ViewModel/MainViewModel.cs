using GalaSoft.MvvmLight;

namespace LaunchIntelligence.ViewModel
{
   
    public class MainViewModel : ViewModelBase
    {
        private BasePivotChartViewModel _basePivotChartViewModel;

        public MainViewModel()
        {
            BasePivotChartViewModel = new BasePivotChartViewModel();
        }

        public BasePivotChartViewModel BasePivotChartViewModel
        {
            get { return _basePivotChartViewModel; }
            set { Set(() => BasePivotChartViewModel, ref _basePivotChartViewModel, value); }
        }

    }
}