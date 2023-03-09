using SkiaSharp.Views.Forms;
using SkiaSharpIssue.ViewModels;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SkiaSharpIssue.Views
{
    public partial class AboutPage : ContentPage
    {
        AboutViewModel viewModel;
        public AboutPage()
        {
            BindingContext = new AboutViewModel();
            viewModel = BindingContext as AboutViewModel;
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (viewModel != null && viewModel.RefreshViewAction == null)
            {
                viewModel.RefreshViewAction = () =>
                {
                    RefreshCanvas();
                };
            }
        }

        private void OnCanvasViewPaintInstructions(object sender, SKPaintSurfaceEventArgs args)
        {
            var assembly = typeof(App).Assembly;
            viewModel.Paint(assembly, args);
        }

        public void RefreshCanvas()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                instructionsGraphic.InvalidateSurface();
            });
        }
    }
}