using EShop.Domain.Interfaces;
using EShop.Domain.Models;
using EShop.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Infra.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MyEShopContext _context;
        public UserRepository(MyEShopContext context)
        {
            _context = context;
        }

        public void Delete(User user)
        {
            _context.Users.Remove(user);
        }

        public async void Delete(int id)
        {
            var user = await GetUserByIdAsync(id);
            _context.Users.Remove(user);
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }


        public async Task InsertAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task<bool> IsUserExistsByEmail(string email)
        {
            return await _context.Users.AnyAsync(x=>x.UserEmail==email);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(User user)
        {
            _context.Users.Update(user);
        }


        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.UserEmail == email);
        }

        public async Task<User> GetUserByConfirmCode(string ConfirmCode)
        {
             return  await _context.Users.FirstOrDefaultAsync(x=>x.ConfirmCode==ConfirmCode); 
        }
    }
}
