using Prism.Navigation;

namespace Ferroviario.Prism.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
        public HomePageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Home Page";
        }
    }
}
