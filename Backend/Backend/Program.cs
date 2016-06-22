using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Service.ProjectService;

namespace Backend
{
    class Program
    {
        static void Main(string[] args)
        {
            var settings = new NinjectSettings { LoadExtensions = true };
            var kernel = new StandardKernel(settings);
            kernel.Load(@"C:\Users\Егор\Documents\Visual Studio 2015\Projects\Backend\Backend\Backend\XmlConfiguration.xml");
            var server = kernel.Get<WebServer>();
            server.Start(() => kernel);
            Console.ReadKey();
        }
    }
}
