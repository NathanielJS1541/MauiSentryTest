using MauiSentryTest.App.ViewModels;

namespace MauiSentryTest.App.Views
{
    public partial class HomePage : ContentPage
    {
        #region Construction

        public HomePage(HomePageViewModel viewModel)
        {
            BindingContext = viewModel;

            InitializeComponent();
        }

        #endregion
    }
}
