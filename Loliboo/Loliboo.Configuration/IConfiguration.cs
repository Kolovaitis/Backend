using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loliboo.Configuration
{
    public interface IConfiguraiton
    {
        string Host { get; }
        string NameDatabase { get; }
    }
}
