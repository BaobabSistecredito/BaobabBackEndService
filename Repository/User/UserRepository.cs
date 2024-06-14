using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaobabBackEndSerice.Data;
using BaobabBackEndSerice.Models;
using BaobabBackEndService.Utils;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace BaobabBackEndService.Repository.User
{
    public class UserRepository : IUserRepository
    {
        private readonly BaobabDataBaseContext _context;

        public UserRepository(BaobabDataBaseContext context)
        {
            _context = context;
        }

        public async Task<MarketingUser> UserAuth(string User,string Password)
        {
            return await _context.MarketingUsers.FirstOrDefaultAsync(d => d.Username == User && d.Password == Password);
        }


    }
}