using Backend.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Service
{
    public interface IService
    {
        User GetUserByEmail(string email);
        Task Registration(User user);
    }
}
