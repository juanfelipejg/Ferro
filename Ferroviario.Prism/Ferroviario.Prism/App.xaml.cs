using Prism;
using Prism.Ioc;
using Ferroviario.Prism.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Ferroviario.Common.Services;
using Ferroviario.Prism.Views;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Ferroviario.Prism
{
    public partial class App
    {
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            await NavigationService.NavigateAsync("NavigationPage/RequestsPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IApiService, ApiService>();
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<RequestsPage, RequestsPageViewModel>();
            containerRegistry.RegisterForNavigation<ShiftsPage, ShiftsPageViewModel>();
        }
    }
}
