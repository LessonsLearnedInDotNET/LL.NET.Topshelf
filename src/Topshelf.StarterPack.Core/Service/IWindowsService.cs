namespace Topshelf.StarterPack.Core.Service
{
    /// <summary>
    /// Defines the interface of a Windows service
    /// </summary>
    public interface IWindowsService
    {
        /// <summary>
        /// Defines the method that will be called when a Windows service is started
        /// </summary>
        void Start();
        
        /// <summary>
        /// Defines the method that will be called when a Windows service is stopped
        /// </summary>
        void Stop();
    }
}
