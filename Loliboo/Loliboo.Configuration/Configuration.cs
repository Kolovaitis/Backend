using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loliboo.Configuration
{
    public class Configuration : IConfiguraiton
    {
        public string Host => "http://localhost:9000";
        public string NameDatabase { get; } = "loliboo";
    }
}
