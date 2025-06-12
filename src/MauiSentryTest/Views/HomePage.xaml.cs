using MauiSentryTest.ViewModels;

namespace MauiSentryTest.Views
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
