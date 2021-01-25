using System;
using System.Threading.Tasks;
using WarhammerCore.Abstract.Exceptions;
using WarhammerCore.Abstract.Interfaces;
using WarhammerCore.Abstract.Models.User;

namespace WarhammerCore.Business
{
    public class UserService : IUserService
    {
        private readonly IDataRepo _repo;
        public UserService(IDataRepo repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public async Task<UserAuthInfo> SignInAsync(string email, string password)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public async Task<bool> SignOutByTokenAsync(string token)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public async Task<bool> SignOutByUserIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public async Task<string> SignUpAsync(string email, string password)
        {
            UserInfo user = await _repo.GetUserByEmailAsync(email);

            if (!UserInfo.IsNullOrEmpty(user)) throw new AppBusinessException($"User with email {email} already registered", "EmailAreadyExists");

            string userId = await _repo.CreateUserAsync(email, password);

            return userId;
        }
    }
}