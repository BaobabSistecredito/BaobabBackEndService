using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaobabBackEndSerice.Models;

namespace BaobabBackEndService.Repository.Users
{
    public interface IUsersRepository
    {
        // ---------------------- GET USER EMAIL:
        Task<MarketingUser> GetMarketingUserByEmail(MarketingUser user);
        // ------------------------ ADD NEW USER:
        Task<MarketingUser> AddNewUser(MarketingUser newUser);
        // ------------------------ GET USER BY ID:
        Task<MarketingUser> GetMarketingUserById(int id);
    }
}