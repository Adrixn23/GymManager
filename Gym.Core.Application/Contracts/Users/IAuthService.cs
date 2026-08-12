using System.Threading.Tasks;
using Gym.Core.Application.DTOs;
using Gym.Core.Domain.Common.Results;

namespace Gym.Core.Application.Contracts
{
    public interface IAuthService
    {
        Task<OperationResult<UserDTO>> LoginAsync(string username, string password);
    }
}
