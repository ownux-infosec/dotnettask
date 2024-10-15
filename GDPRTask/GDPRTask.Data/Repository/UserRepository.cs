
using GDPRTask.Data.Model;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDPRTask.Data.Repository
{
    public class UserRepository
    {
        private readonly IMongoCollection<User> _users;

        public UserRepository(IConfiguration configuration) // Accept IConfiguration
        {
            var client = new MongoClient(configuration.GetConnectionString("MongoDbConnection"));
            var database = client.GetDatabase("UserDB");
            _users = database.GetCollection<User>("Users");
        }

        public void CreateUser(User user)
        {
            _users.InsertOne(user);
        }

        public User GetUserById(string id)
        {
            return _users.Find(user => user.Id == id).FirstOrDefault();
        }

        public void UpdateUser(User user)
        {
            var existingUser = GetUserById(user.Id);
            if (existingUser != null)
            {
                existingUser.Name = user.Name;
                existingUser.Email = user.Email;
                existingUser.GdprConsent = user.GdprConsent;
            }
        }
        public List<User> GetAllUsers()
        {
            return _users.Find(user => true).ToList();
        }

        public void DeleteUser(string id)
        {
            var result = _users.DeleteOne(user => user.Id == id);
            if (result.DeletedCount == 0)
            {
                // Handle the case where no user was found to delete
                throw new KeyNotFoundException($"User with ID {id} not found.");
            }
        }
    }
}
