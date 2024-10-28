using EShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.Interfaces
{
    public  interface IUserRepository
    {
        Task<List<User>> GetAllAsync();
        Task<User?> GetUserByIdAsync(int id);
        Task InsertAsync(User user);
        Task SaveAsync();
        void Update(User user);
        void Delete(User user);
        void Delete(int id);
        Task DeleteUser(int id);
        Task<bool>IsUserExistsByEmail(string email);
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserByConfirmCode(string ConfirmCode);
    }
}
