using Prism;
using Prism.Ioc;
using Ferroviario.Prism.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Ferroviario.Common.Services;
using Ferroviario.Prism.Views;
using Ferroviario.Common.Helpers;
using Syncfusion.Licensing;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Ferroviario.Prism
{
    public partial class App
    {
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            SyncfusionLicenseProvider.RegisterLicense("MjY0MzEwQDMxMzgyZTMxMmUzME5rWlhvSHBaU1lLeWV4bzh6cHdvdVo3Z0xaSkt0Ukw5Wng5OUJac2h3U009");
            InitializeComponent();
            await NavigationService.NavigateAsync("/FerroviarioMasterDetailPage/NavigationPage/LoginPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IApiService, ApiService>();
            containerRegistry.Register<ITransformHelper, TransformHelper>();
            containerRegistry.Register<IFilesHelper, FilesHelper>();
            containerRegistry.Register<IRegexHelper, RegexHelper>();
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<RequestsPage, RequestsPageViewModel>();            
            containerRegistry.RegisterForNavigation<FerroviarioMasterDetailPage, FerroviarioMasterDetailPageViewModel>();
            containerRegistry.RegisterForNavigation<UsersPage, UserPageViewModel>();
            containerRegistry.RegisterForNavigation<ShiftsPage, ShiftsPageViewModel>();
            containerRegistry.RegisterForNavigation<ShiftDetailPage, ShiftDetailPageViewModel>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<RegisterPage, RegisterPageViewModel>();
            containerRegistry.RegisterForNavigation<RememberPasswordPage, RememberPasswordPageViewModel>();
            containerRegistry.RegisterForNavigation<ChangePasswordPage, ChangePasswordPageViewModel>();
            containerRegistry.RegisterForNavigation<ChangesPage, ChangesPageViewModel>();
            containerRegistry.RegisterForNavigation<RequestDetailPage, RequestDetailPageViewModel>();
            containerRegistry.RegisterForNavigation<CreateChangePage, CreateChangePageViewModel>();
            containerRegistry.RegisterForNavigation<ChangesTabbedPage, ChangesTabbedPageViewModel>();
            containerRegistry.RegisterForNavigation<CreateRequestPage, CreateRequestPageViewModel>();
        }
    }
}
