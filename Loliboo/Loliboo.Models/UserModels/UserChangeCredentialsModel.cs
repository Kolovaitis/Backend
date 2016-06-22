using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loliboo.Models.UserModels
{
    public class UserChangeCredentialsModel
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string NewEmail { get; set; }
    }
}
