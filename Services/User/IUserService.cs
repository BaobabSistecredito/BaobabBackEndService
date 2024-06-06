using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaobabBackEndSerice.Models;
using BaobabBackEndService.Utils;
using BaobabBackEndService.Dto;

namespace BaobabBackEndService.Services.User
{
    public interface IUserService
    {
        Task<ResponseUtils<TokenModel>> CreateToken(AuthResponse authResponse);
        
        
    }
}