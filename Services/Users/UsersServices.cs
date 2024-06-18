using BaobabBackEndSerice.Models;
using BaobabBackEndService.Utils;
using BaobabBackEndService.Repository.Users;
using BaobabBackEndService.DTOs;
using BaobabBackEndService.ExternalServices.Jwt;

namespace BaobabBackEndService.Services.Users
{
    public class UsersServices : IUsersServices
    {
        private readonly IJwtService _jwtService;
        private readonly IUsersRepository _usersRepository;

        public UsersServices(IUsersRepository usersRepository, IJwtService jwtService)
        {
            _usersRepository = usersRepository;
            _jwtService = jwtService;

        }
        // ------------------------ ADD NEW USER:
        public async Task<ResponseUtils<MarketingUser>> CreateUser(MarketingUser newUser)
        {
            try
            {
                // Se confirma si el 'Email' existe en la base de datos:
                var existEmail = await _usersRepository.GetMarketingUserByEmail(newUser);
                // Condicional que determina si se ha encontrado el Email:
                if (existEmail == null)
                {
                    // Se crea una instancia de la clase (PasswordHasher):
                    var passHasher = new PasswordHasher();
                    // Se inicializa una variable con la password encriptada utilizando el método de la clase (EncryptPassword):
                    var EncriptedPassword = passHasher.EncryptPassword(newUser.Password);
                    // Se actualiza la contraseña en el modelo:
                    newUser.Password = EncriptedPassword;
                    // Se agrega el nuevo usuario a la entidad 'MarketingUsers:
                    await _usersRepository.AddNewUser(newUser);
                    // Retorno de la respuesta éxitosa con la estructura de la clase 'ResponseUtils':
                    return new ResponseUtils<MarketingUser>(true, new List<MarketingUser> { newUser }, 201, message: "¡Usuario registrado!");
                }
                else
                {
                    return new ResponseUtils<MarketingUser>(false, null, 406, message: "¡El email ya se encuentra registrado!");
                }
            }
            catch (Exception ex)
            {
                return new ResponseUtils<MarketingUser>(false, null, 500, message: $"Error interno del servidor: {ex.Message}");
            }
        }

        public async Task<ResponseUtils<string>> UserLoginAsync(UserLoginDTO user)
        {
            MarketingUser validateUSer = await _usersRepository.UserLoginAsync(user);
            var EncriptedPassword = new PasswordHasher().VerifyPassword(validateUSer.Password, user.Password);
            if (validateUSer == null || !EncriptedPassword)
            {
                return new ResponseUtils<string>(false, null, 406, message: "La contraseña o el usuario son incorrectos");
            }

            var token = _jwtService.GenerateJwtToken(validateUSer);

            return new ResponseUtils<string>(true, new List<string> { token });

        }

    }
}