using System;
using Android.App;
using Android.Runtime;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace SalesAround.Android
{
    [Application]
    public class Application : MvxAppCompatApplication<MvxAppCompatSetup<Core.Application>, Core.Application>
    {
        public Application(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }
    }
}