using System;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using AutoMapper;
using BL.DTOs;
using BL.DTOs.Filters;
using BL.QueryObjects.Common;
using BL.Services.Common;
using DAL.Entities;
using Infrastructure;
using Infrastructure.Query;

namespace BL.Services.Users
{
    public class UserApplicantService : ServiceBase, IUserApplicantService
    {
        private const int PBKDF2IterCount = 100000;
        private const int PBKDF2SubkeyLength = 160 / 8;
        private const int saltSize = 128 / 8;

        private readonly IRepository<Applicant> applicantRepository;
        private readonly QueryObjectBase<UserDto, User, UserFilterDto, IQuery<User>> userQueryObject;

        public UserApplicantService(IMapper mapper, IRepository<Applicant> applicantRepository, QueryObjectBase<UserDto, User, UserFilterDto, IQuery<User>> userQueryObject)
            : base(mapper)
        {
            this.applicantRepository = applicantRepository;
            this.userQueryObject = userQueryObject;
        }

        public async Task<UserDto> GetUserAccordingToUsernameAsync(string username)
        {
            var queryResult = await userQueryObject.ExecuteQuery(new UserFilterDto { Username = username });
            return queryResult.Items.SingleOrDefault();
        }

        public async Task<Guid> RegisterUserAsync(UserApplicantCreateDto userDto)
        {
            var applicant = Mapper.Map<Applicant>(userDto);

            if (await GetIfUserExistsAsync(applicant.Username))
            {
                throw new ArgumentException();
            }

            var password = CreateHash(userDto.Password);
            applicant.PasswordHash = password.Item1;
            applicant.PasswordSalt = password.Item2;

            applicantRepository.Create(applicant);

            return applicant.Id;
        }

        public async Task<(bool success, string roles)> AuthorizeUserAsync(string username, string password)
        {
            var userResult = await userQueryObject.ExecuteQuery(new UserFilterDto { Username = username });
            var user = userResult.Items.SingleOrDefault();

            var succ = user != null && VerifyHashedPassword(user.PasswordHash, user.PasswordSalt, password);

            var roles = user?.Roles ?? "";
            return (succ, roles);
        }

        private async Task<bool> GetIfUserExistsAsync(string username)
        {
            var queryResult = await userQueryObject.ExecuteQuery(new UserFilterDto { Username = username });
            return (queryResult.Items.Count() == 1);
        }

        private bool VerifyHashedPassword(string hashedPassword, string salt, string password)
        {
            var hashedPasswordBytes = Convert.FromBase64String(hashedPassword);
            var saltBytes = Convert.FromBase64String(salt);

            using (var deriveBytes = new Rfc2898DeriveBytes(password, saltBytes, PBKDF2IterCount))
            {
                var generatedSubkey = deriveBytes.GetBytes(PBKDF2SubkeyLength);
                return hashedPasswordBytes.SequenceEqual(generatedSubkey);
            }
        }

        private Tuple<string, string> CreateHash(string password)
        {
            using (var deriveBytes = new Rfc2898DeriveBytes(password, saltSize, PBKDF2IterCount))
            {
                byte[] salt = deriveBytes.Salt;
                byte[] subkey = deriveBytes.GetBytes(PBKDF2SubkeyLength);

                return Tuple.Create(Convert.ToBase64String(subkey), Convert.ToBase64String(salt));
            }
        }
    }
}
