using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaobabBackEndSerice.Data;
using BaobabBackEndSerice.Models;
using BaobabBackEndService.Utils;
using Microsoft.EntityFrameworkCore;

namespace BaobabBackEndService.Repository.User
{
    public interface IUserRepository
    {
        
        Task<MarketingUser> UserAuth(string User,string Password);


        //Task<MarketingUser> Verificar(string Email,string password);

        

         
        
    }
}