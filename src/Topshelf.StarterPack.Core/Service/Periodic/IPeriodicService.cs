using System.Timers;

namespace Topshelf.StarterPack.Core.Service.Periodic
{
    /// <summary>
    /// Defines the interface of a periodic Windows service
    /// </summary>
    public interface IPeriodicService : IWindowsService
    {
        /// <summary>
        /// A Timer for triggered period expired events
        /// </summary>
        Timer ServiceTimer { get; }

        /// <summary>
        /// An <see cref="int"/> describing the number of milliseconds provided as
        /// the period of the <see cref="ServiceTimer"/>
        /// </summary>
        int ServiceTimerPeriod { get; }

        /// <summary>
        /// The method that will contain the logic executed at an interval
        /// of the period as described by <see cref="ServiceTimerPeriod"/> .
        /// </summary>
        void OnPeriodExpired();
    }
}
