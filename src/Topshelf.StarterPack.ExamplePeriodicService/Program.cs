using log4net;
using log4net.Config;
using System;
using System.IO;
using System.Reflection;

namespace Topshelf.StarterPack.ExamplePeriodicService
{
    class Program
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public const int TWENTY_SECONDS = 20000;

        static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                x.Service<ExampleDerivedPeriodicService>(s =>
                {
                    s.ConstructUsing(name => new ExampleDerivedPeriodicService(TWENTY_SECONDS));
                    s.WhenStarted(tc =>
                    {
                        XmlConfigurator.ConfigureAndWatch(new FileInfo(".\\log4net.config"));
                        tc.Start();
                    });
                    s.WhenStopped(tc => tc.Stop());
                });

                x.RunAsLocalSystem();

                x.SetDescription("Example Derived Periodic Service");
                x.SetDisplayName("ExampleDerivedPeriodicService");
                x.SetServiceName("ExampleDerivedPeriodicService");
            });
        }
    }
}
