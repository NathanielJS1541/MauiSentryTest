namespace MauiSentryTest.Common.Services
{
    /// <summary>
    /// Example singleton service in the Common library, to demonstrate stack traces from classes
    /// in other projects.
    /// </summary>
    public class CommonService : ISingletonService
    {
        #region Fields

        #region Private

        /// <summary>
        /// The last time that service was polled, or when the service was initialised. Before
        /// initialisation, this will be <see cref="DateTime.MinValue"/>.
        /// </summary>
        private DateTime _lastPollTime = DateTime.MinValue;

        #endregion

        #endregion

        #region Methods

        #region Public

        /// <summary>
        /// Initialise the <see cref="CommonService"/>.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown when <see cref="Initialise"/> is called on a service that is already
        /// initialised.
        /// </exception>
        public void Initialise()
        {
            if (_lastPollTime != DateTime.MinValue)
            {
                throw new InvalidOperationException("CommonService has already been initialised.");
            }

            _lastPollTime = DateTime.Now;
        }

        /// <summary>
        /// Poll the <see cref="CommonService"/>.
        /// </summary>
        /// <param name="pollTime">
        /// The <see cref="DateTime"/> indicating the time at which the service was polled.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown when <see cref="Initialise"/> has not been called before polling the service.
        /// </exception>
        public void PollService(DateTime pollTime)
        {
            if (_lastPollTime == DateTime.MinValue)
            {
                throw new InvalidOperationException(
                    "Test exception in CommonService.",
                    new InvalidOperationException("CommonService was polled before being initialised.")
                    );
            }

            _lastPollTime = pollTime;
        }

        #endregion

        #endregion
    }
}
