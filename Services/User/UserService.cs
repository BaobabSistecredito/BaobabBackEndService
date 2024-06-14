using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaobabBackEndSerice.Models;
using BaobabBackEndSerice.Data;
using BaobabBackEndService.Repository.User;
using BaobabBackEndService.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using Microsoft.AspNetCore.Http.HttpResults;
using BaobabBackEndService.Dto;


namespace BaobabBackEndService.Services.User
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ResponseUtils<TokenModel>> CreateToken(AuthResponse authResponse)
        {
            var User_Auth = await _userRepository.UserAuth(authResponse.Username, authResponse.Password);


            Console.WriteLine("---------------------------------asdasd------------------------------------------------");
            Console.WriteLine(User_Auth);
            Console.WriteLine("---------------------------------asdasd------------------------------------------------");
            if (User_Auth == null)
            {
                return new ResponseUtils<TokenModel>(false, message: "El usuario o la contraseña no se encuentra en la base de datos");


            }
            else if(User_Auth != null)
            {

                var Secret_key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("3C7HJGIRJIKSOKSDIJFIDJFDJFDJF23234E"));
                var signin_credentials = new SigningCredentials(Secret_key, SecurityAlgorithms.HmacSha256);
                var Token_configure = new JwtSecurityToken(
                    issuer: @Environment.GetEnvironmentVariable("JwtToke"),
                    audience: @Environment.GetEnvironmentVariable("JwtToke"),
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: signin_credentials
                );
                var token_write = new JwtSecurityTokenHandler().WriteToken(Token_configure);
                var token = new TokenModel()
                {
                    Token = token_write
                };
                return new ResponseUtils<TokenModel>(true, code: token, message: "Todo oki");
            }else
            {
                return new ResponseUtils<TokenModel>(false, message: "El usuario o la contraseña no se encuentra en la base de datos");
            }


        }
    }
}