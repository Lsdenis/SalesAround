using System;
using Android.Runtime;
using MvvmCross.Platforms.Android.Core;
using MvvmCross.Platforms.Android.Views;

namespace SalesAround.Android
{
    public class Application : MvxAndroidApplication<MvxAndroidSetup<Core.Application>, Core.Application>
    {
        public Application(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }
    }
}