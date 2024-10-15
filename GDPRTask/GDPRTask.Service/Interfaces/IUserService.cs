using GDPRTask.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDPRTask.Service.Services
{
    public interface IUserService
    {
        void AddUser(User user);
       
        User GetUser(string id);
        List<User> GetAllUsers();
        void DeleteUser(string id);
        void UpdateUser(User user);
    }
}
