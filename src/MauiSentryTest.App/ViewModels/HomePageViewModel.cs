using CommunityToolkit.Mvvm.Input;
using MauiSentryTest.App.Services;
using MauiSentryTest.Common.Services;

namespace MauiSentryTest.App.ViewModels
{
    public class HomePageViewModel : IViewModel
    {
        #region Fields

        #region Private

        private readonly CommonService _commonService;
        private readonly InternalService _internalService;

        #endregion

        #endregion

        #region Construction

        public HomePageViewModel(CommonService commonService, InternalService internalService)
        {
            // Store DI'd service instances.
            _commonService = commonService;
            _internalService = internalService;

            // Create IAsyncRelayCommands for the view to bind to.
            CommonServiceCrashCommand = new AsyncRelayCommand(CommonServiceCrashCommandTask);
            InternalServiceCrashCommand = new AsyncRelayCommand(InternalServiceCrashCommandTask);
            ViewModelCrashCommand = new AsyncRelayCommand(ViewModelCrashCommandTask);
        }

        #endregion

        #region Properties

        #region Public

        /// <summary>
        /// An <see cref="IAsyncRelayCommand"/> to throw an exception from a service in an external
        /// project.
        /// </summary>
        public IAsyncRelayCommand CommonServiceCrashCommand { get; }

        /// <summary>
        /// An <see cref="IAsyncRelayCommand"/> to throw an exception from a service within the app
        /// project.
        /// </summary>
        public IAsyncRelayCommand InternalServiceCrashCommand { get; }

        /// <summary>
        /// An <see cref="IAsyncRelayCommand"/> to throw an exception from within the ViewModel.
        /// </summary>
        public IAsyncRelayCommand ViewModelCrashCommand { get; }

        #endregion

        #endregion

        #region Methods

        #region Private

        /// <summary>
        /// The <see cref="Task"/> for the <see cref="CommonServiceCrashCommand"/>.
        /// </summary>
        private Task CommonServiceCrashCommandTask()
        {
            // Poll the service before initialisation to throw an exception.
            _commonService.PollService(DateTime.Now);

            return Task.CompletedTask;
        }

        /// <summary>
        /// The <see cref="Task"/> for the <see cref="InternalServiceCrashCommand"/>.
        /// </summary>
        private Task InternalServiceCrashCommandTask()
        {
            _internalService.ThrowException();

            return Task.CompletedTask;
        }

        #endregion

        #region Private Static

        /// <summary>
        /// The <see cref="Task"/> for the <see cref="ViewModelCrashCommand"/>.
        /// </summary>
        private static Task ViewModelCrashCommandTask()
        {
            throw new InvalidOperationException("Test exception in the HomePageViewModel.");
        }

        #endregion

        #endregion
    }
}
