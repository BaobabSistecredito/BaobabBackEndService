using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaobabBackEndSerice.Data;
using BaobabBackEndSerice.Models;
using BaobabBackEndService.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BaobabBackEndService.Repository.Users
{
    public class UsersRepository : IUsersRepository
    {
        private readonly BaobabDataBaseContext _context;

        public UsersRepository(BaobabDataBaseContext context)
        {
            _context = context;
        }
        // ---------------------- GET USER EMAIL:
        public async Task<MarketingUser> GetMarketingUserByEmail(MarketingUser user)
        {
            return await _context.MarketingUsers.FirstOrDefaultAsync(u => u.Email == user.Email);
        }
        // ------------------------ ADD NEW USER:
        public async Task<MarketingUser> AddNewUser(MarketingUser newUser)
        {
            _context.MarketingUsers.Add(newUser);
            await _context.SaveChangesAsync();
            return newUser;
        }

        public async Task<MarketingUser> UserLoginAsync(UserLoginDTO userLoginDTO)
        {
            return await _context.MarketingUsers.FirstOrDefaultAsync(d => d.Username == userLoginDTO.Username && d.Password == userLoginDTO.Password);
        }

    }
}