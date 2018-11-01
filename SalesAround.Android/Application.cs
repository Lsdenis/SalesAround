using System;
using Android.App;
using Android.Runtime;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace SalesAround.Android
{
    [Application]
    public class Application : MvxAppCompatApplication<MvxAppCompatSetup<CoreProject.Application>, CoreProject.Application>
    {
        public Application(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }
    }
}