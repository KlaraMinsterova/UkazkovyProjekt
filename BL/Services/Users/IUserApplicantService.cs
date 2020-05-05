using System;
using System.Threading.Tasks;
using BL.DTOs;

namespace BL.Services.Users
{
    public interface IUserApplicantService
    {
        Task<Guid> RegisterUserAsync(UserApplicantCreateDto user);
        Task<(bool success, string roles)> AuthorizeUserAsync(string username, string password);
        Task<UserDto> GetUserAccordingToUsernameAsync(string username);
    }
}
