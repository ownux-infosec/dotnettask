using GDPRTask.Data.Model;
using GDPRTask.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDPRTask.Service.Services
{
    public class UserService : IUserService
    {
        private readonly UserRepository _userRepository;

        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public void AddUser(User user)
        {
            // Add business logic here if needed
            _userRepository.CreateUser(user);
        }

        public void UpdateUser(User user)
        {
            _userRepository.UpdateUser(user);
        }
        public User GetUser(string id)
        {
            return _userRepository.GetUserById(id);
        }

        public List<User> GetAllUsers()
        {
            return _userRepository.GetAllUsers();
        }
        public void DeleteUser(string id) // Implementation for deleting a user
        {
            _userRepository.DeleteUser(id); // Ensure this method exists in your repository
        }
    }
}
