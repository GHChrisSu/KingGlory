using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace KingOfGlory.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                x.Service<StartService>(s =>
                {
                    s.ConstructUsing(name => new StartService());
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });
                x.RunAsLocalSystem();

                x.SetDescription("KingOfGlory");
                x.SetDisplayName("KingOfGlory");
                x.SetServiceName("KingOfGlory");
            });
        }
    }

    public class StartService
    {
        public void Start()
        {
            Console.WriteLine("starting...");
        }

        public void Stop()
        {
            Console.WriteLine("stoping...");
        }
    }
}
