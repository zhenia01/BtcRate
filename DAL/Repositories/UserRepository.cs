using System;
using System.Threading.Tasks;
using CSV;
using DAL.Entities;

namespace DAL.Repositories
{
    /// <summary>
    /// Repository to manipulate with User objects in a data storage
    /// </summary>
    public class UserRepository
    {
        private readonly CsvService _csvService;

        public UserRepository(CsvService csvService)
        {
            _csvService = csvService;
        }

        /// <summary>
        /// Creates user if email is unique
        /// </summary>
        public async Task CreateUser(User user)
        {
            var existingUser = await _csvService.Find<User>(u => u.Email == user.Email);

            if (existingUser == null)
            {
                await _csvService.Write(user);
            }
            else
            {
                throw new InvalidOperationException($"User with email {user.Email} already exist");
            }
        }

        /// <summary>
        /// Finds first occurence of a User in a data storage
        /// </summary>
        public Task<User?> FindUser(Predicate<User> predicate) => 
            _csvService.Find(predicate);
    }
}