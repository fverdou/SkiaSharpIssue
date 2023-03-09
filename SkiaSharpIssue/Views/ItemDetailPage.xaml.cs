using SkiaSharpIssue.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace SkiaSharpIssue.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}