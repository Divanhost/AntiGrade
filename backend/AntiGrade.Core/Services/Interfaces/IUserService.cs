using System.Collections.Generic;
using System.Threading.Tasks;
using AntiGrade.Shared.InputModels;
using AntiGrade.Shared.Models.Identity;
using AntiGrade.Shared.ViewModels;

namespace AntiGrade.Core.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<RolesView>> GetAllRoles();
        Task<UserView> GetUserById(int userId);
        Task<User> CreateUser(UserDto userModel);
        Task<User> UpdateUserByID(int userId, UserDto user);
        Task<bool> DeleteById(int userId);
        Task<string> CheckUserNameExists(string username);
        Task<string> CheckEmailExists(string email);
        Task<bool> CheckPasswordAsync(string userName, string password);
    }
}
