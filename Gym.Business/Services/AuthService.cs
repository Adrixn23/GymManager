
using Gym.Business.DTOs;
using Gym.Business.Interfaces;
using Gym.Business.LogicResults;
using Gym.Data.Interfaces;

namespace Gym.Business.Services

{
    public class AuthService : IAuthService
    {

        private readonly IUnitOfWork _unitOfWork;
        public AuthService(IUnitOfWork unitOfWork)
        {

            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<UserDTO>> LoginAsync(string username, string password) {

            var exist = await _unitOfWork.Users.ExistsAsync(username);
            if (!exist)
            {
                return OperationResult<UserDTO>.Fail("Usuario No se encontro. ");
            }


            var user = await _unitOfWork.Users.GetByUsernameAsync(username);



            if (!user.IsActive)
            {
               
                return OperationResult<UserDTO>.Fail("usuario Inactivo.");
            }


            bool isValid = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);

            if (!isValid )
            {
                return OperationResult<UserDTO>.Fail("Contraseña incorrecta");
            }


            await _unitOfWork.Users.UpdateLastAccessAsync(user.UserId);
            await _unitOfWork.SaveChangesAsync();

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
