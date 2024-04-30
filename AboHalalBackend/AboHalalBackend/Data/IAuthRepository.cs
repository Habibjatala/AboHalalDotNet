using AboHalalBackend.Dtos.User;
using AboHalalBackend.Models;

namespace AboHalalBackend.Data
{
    public interface IAuthRepository
    {
        Task<ServiceResponse<int>> Register(AddRegisterDto request);
       Task<ServiceResponse<AuthenticatedUserDto>> Login(String username, string password);
        Task<bool> UserExists(string username);
    }
}
