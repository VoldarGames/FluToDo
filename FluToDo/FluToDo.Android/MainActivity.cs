﻿using Android.App;
using Android.Content.PM;
using Android.OS;
using FluToDo.Droid.Resources;
using Xamarin.Forms;


namespace FluToDo.Droid
{
    [Activity(Label = "FluToDo", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            Forms.Init(this, bundle);
            RegisterDependencies();
            LoadApplication(new FluToDoApp());
        }

        private void RegisterDependencies()
        {
            OperatingSystemMethods.CurrentContext = this;
            DependencyService.Register<OperatingSystemMethods>();
        }
    }
}

