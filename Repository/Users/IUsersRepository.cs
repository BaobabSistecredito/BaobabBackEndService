using BaobabBackEndSerice.Models;
using BaobabBackEndService.DTOs;

namespace BaobabBackEndService.Repository.Users
{
    public interface IUsersRepository
    {
        // ---------------------- GET USER EMAIL:
        Task<MarketingUser> GetMarketingUserByEmail(MarketingUser user);
        // ------------------------ ADD NEW USER:
        Task<MarketingUser> AddNewUser(MarketingUser newUser);
        // ------------------------ Get AND VALIDATE USER:
        Task<MarketingUser> UserLoginAsync(UserLoginDTO userLoginDTO);
        // ------------------------ GET USER BY ID:
        Task<MarketingUser> GetMarketingUserById(int id);
    }
}