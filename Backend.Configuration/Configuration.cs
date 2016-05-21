using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Configuration
{
    public interface IConfiguraiton
    {
        string Host { get; }
    }

    public class Configuration : IConfiguraiton
    {
        public string Host => "http://localhost:9000";
    }
}
