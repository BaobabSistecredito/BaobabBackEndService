using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaobabBackEndSerice.Models;
using BaobabBackEndService.Utils;

namespace BaobabBackEndService.Services.Users
{
    public interface IUsersServices
    {
        // ------------------------ ADD NEW USER:
        Task<ResponseUtils<MarketingUser>> CreateUser(MarketingUser newUser);
        // --------------------------------------
    }
}