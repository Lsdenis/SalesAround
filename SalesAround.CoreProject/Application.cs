using MvvmCross.ViewModels;
using SalesAround.CoreProject.ViewModels;

namespace SalesAround.CoreProject
{
    public class Application : MvxApplication
    {
        public override void Initialize()
        {
            RegisterAppStart<MainViewModel>();
        }
    }
}