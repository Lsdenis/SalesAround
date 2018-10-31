using MvvmCross.ViewModels;

namespace SalesAround.Core
{
    public class Application : MvxApplication
    {
        public override void Initialize()
        {
            RegisterAppStart<MvxViewModel>();
        }
    }
}