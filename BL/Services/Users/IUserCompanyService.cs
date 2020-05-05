using System;
using System.Threading.Tasks;
using BL.DTOs;

namespace BL.Services.Users
{
    public interface IUserCompanyService
    {
        Task<Guid> RegisterUserAsync(UserCompanyCreateDto user);
        Task<(bool success, string roles)> AuthorizeUserAsync(string username, string password);
        Task<UserDto> GetUserAccordingToUsernameAsync(string username);
    }
}
