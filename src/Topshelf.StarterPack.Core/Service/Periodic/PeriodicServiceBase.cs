using log4net;
using System;
using System.Reflection;
using System.Timers;

namespace Topshelf.StarterPack.Core.Service.Periodic
{
    /// <summary>
    /// An <see cref="abstract"/> <see cref="class"/> containing common behavior
    /// for every periodic Windows service. Implements <see cref="IPeriodicService"/>
    /// and <see cref="IWindowsService"/>. This uses a <see cref="log4net"/> <see cref="ILog"/>
    /// to log when the service is started, stopped, or the period expires.
    /// </summary>
    public abstract class PeriodicServiceBase : IPeriodicService
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public Timer ServiceTimer { get; private set; }
        public int ServiceTimerPeriod { get; private set; }

        /// <summary>
        /// Constructor accepting an <see cref="int"/> representing the number of miiliseconds
        /// set as the <see cref="ServiceTimer"/> period.
        /// </summary>
        /// <param name="period"></param>
        public PeriodicServiceBase(int period)
        {
            ServiceTimerPeriod = period;
        }

        /// <summary>
        /// The event handler for <see cref="Timer.Elapsed"/> event raised by
        /// the <see cref="ServiceTimer"/>. This method will catch and log any
        /// exceptions that occur and verify that the <see cref="ServiceTimer"/>
        /// will continue raising events.
        /// </summary>
        /// <param name="state"></param>
        /// <param name="elapsedEventArgs"></param>
        private void OnTimerExpired(object state, ElapsedEventArgs elapsedEventArgs)
        {
            try
            {
                Log.InfoFormat("Timer expired.");
                OnPeriodExpired();
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("An error has occurred during OnTimerExpired: \n{0}", ex);
            }
            finally
            {
                ServiceTimer.Enabled = true;
            }
        }


        /// <summary>
        /// The method that will be executed when the service is started.  This method
        /// initializes and starts the <see cref="ServiceTimer"/>. The <see cref="OnStart"/>
        /// abstract method will also be called if the service requires additional functionality
        /// on startup. Beware adding too much logic to the service <see cref="OnStart"/> method. 
        /// This method will catch and log any exceptions that occur on startup.
        /// </summary>
        public void Start()
        {
            try
            {
                Log.Info("Starting periodic service.");
                ServiceTimer = new Timer(ServiceTimerPeriod)
                {
                    AutoReset = false
                };
                ServiceTimer.Elapsed += OnTimerExpired;
                ServiceTimer.Start();
                OnStart();
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("An error occured while starting the service: \n{0}", ex);
            }
        }

        /// <summary>
        /// The method that will be executed when the service is stopped.  This method
        /// disposes of the <see cref="ServiceTimer"/>. The <see cref="OnStop"/>
        /// abstract method will also be called if the service requires additional functionality
        /// on stopping. This method will catch and log any exceptions that occur on stopping.
        /// </summary>
        public void Stop()
        {
            try
            {
                Log.Info("Stopping periodic service.");
                if (ServiceTimer != null)
                    ServiceTimer.Dispose();

                OnStop();
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("An error occured while stopping the service: \n{0}", ex);
            }
        }

        /// <summary>
        /// An method for adding additional functionality to <see cref="Start"/>.
        /// </summary>
        public abstract void OnStart();

        /// <summary>
        /// A method for adding additional functionality to <see cref="Stop"/>.
        /// </summary>
        public abstract void OnStop();

        /// <summary>
        /// A method for specifying the business logic that will be executed when the
        /// <see cref="ServiceTimer"/> expires.
        /// </summary>
        public abstract void OnPeriodExpired();
    }
}
