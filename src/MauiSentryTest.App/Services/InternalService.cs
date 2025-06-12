using MauiSentryTest.Common.Services;

namespace MauiSentryTest.App.Services
{
    /// <summary>
    /// Example service within the app project itself.
    /// </summary>
    public class InternalService : ISingletonService
    {
        #region Methods

        #region Public

        /// <summary>
        /// Throw an exception from within the <see cref="InternalService"/>.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Always thrown for demonstration.
        /// </exception>
        public void ThrowException()
        {
            // Throw a test exception from a service within the app project itself.
            throw new InvalidOperationException("Text exception in the InternalService.");
        }

        #endregion

        #endregion
    }
}
