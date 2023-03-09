using SkiaSharpIssue.Services;
using SkiaSharpIssue.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SkiaSharpIssue
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
