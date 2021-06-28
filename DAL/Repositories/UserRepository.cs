using System;
using System.Threading.Tasks;
using CSV;
using DAL.Entities;

namespace DAL.Repositories
{
    public class UserRepository
    {
        private readonly CsvService _csvService;

        public UserRepository(CsvService csvService)
        {
            _csvService = csvService;
        }

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

        public Task<User?> FindUser(Predicate<User> predicate) => 
            _csvService.Find(predicate);
    }
}