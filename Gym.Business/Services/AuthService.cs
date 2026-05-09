using Gym.Business.DTOs;
using Gym.Business.Interfaces;
using Gym.Business.LogicResults;
using BCrypt.Net;
using Gym.Data.Interfaces;
namespace Gym.Business.Services

{
    public class AuthService : IAuthService
    {

        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository) {

            _userRepository =  userRepository;
        }

        public async Task<OperationResult<UserDTO>> LoginAsync(string username, string password) {

            var exist = await _userRepository.ExistsAsync(username);
            if (!exist)
            {
                return OperationResult<UserDTO>.Fail("Usuario No se encontro. ");
            }


            var user = await _userRepository.GetByUsernameAsync(username);



            if (!user.IsActive)
            {
               
                return OperationResult<UserDTO>.Fail("usuario Inactivo.");
            }


            bool isValid = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);

            if (!isValid )
            {
                return OperationResult<UserDTO>.Fail("Contraseña incorrecta");
            }


            await _userRepository.UpdateLastAccessAsync(user.UserId);


            var userDTO = new UserDTO
            {
                UserId = user.UserId,
                Username = user.Username,
                FullName = user.FullName,
                Role = user.Role
            };

            return OperationResult<UserDTO>.Ok(userDTO, "Login exitoso");


        }
}
}
