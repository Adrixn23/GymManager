using Gym.Business.DTOs;
using Gym.Business.LogicResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Business.Interfaces
{
    public interface IAuthService
    {

        Task<OperationResult<UserDTO>> LoginAsync(string username, string password);
    }
}
