using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;

namespace Loliboo
{
    class Program
    {
        static void Main(string[] args)
        {
            var settings = new NinjectSettings { LoadExtensions = true };
            var kernel = new StandardKernel(settings);
            kernel.Load("XmlConfiguration.xml");
            var server = kernel.Get<WebServer>();
            server.Start(() => kernel);
            Console.ReadKey();
        }
    }
}
