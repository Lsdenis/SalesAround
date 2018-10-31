using MvvmCross;
using MvvmCross.ViewModels;
using SalesAround.Core.Services;
using SalesAround.Core.ViewModels;

namespace SalesAround.Core
{
    public class Application : MvxApplication
    {
        public override void Initialize()
        {
            Mvx.IoCProvider.RegisterType<ILocationService, LocationService>();

            RegisterAppStart<MainViewModel>();
        }
    }
}