using log4net;
using System;
using System.Reflection;
using Topshelf.StarterPack.Core.Service.Periodic;

namespace Topshelf.StarterPack.ExamplePeriodicService
{
    public class ExampleDerivedPeriodicService : PeriodicServiceBase
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public ExampleDerivedPeriodicService(int period) : base(period)
        {
            Log.Info("ExampleDerivedPeriodicService constructor");
        }

        public override void OnPeriodExpired()
        {
            Log.InfoFormat("ExampleDerivedPeriodicService OnPeriodExpired \n\t{0}", DateTime.Now.ToString("HH:mm:ss"));
        }

        public override void OnStart()
        {
            Log.Info("ExampleDerivedPeriodicService OnStart");
        }

        public override void OnStop()
        {
            Log.Info("ExampleDerivedPeriodicService OnStop");
        }
    }
}
