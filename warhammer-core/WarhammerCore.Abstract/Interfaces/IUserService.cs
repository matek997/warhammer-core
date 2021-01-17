using System;
using System.Threading.Tasks;
using WarhammerCore.Abstract.Models.User;

namespace WarhammerCore.Abstract.Interfaces
{
    /// <summary>
    /// Authentication and account creation service.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Create user account with provided email and password.
        /// </summary>
        Task<string> SignUpAsync(string email, string password);

        /// <summary>
        /// Login the user by comparing emails and password with hashed password.
        /// </summary>
        Task<UserAuthInfo> SignInAsync(string email, string password);

        /// <summary>
        /// Sign out the user by removing the token from active ones.
        /// </summary>
        Task<bool> SignOutByTokenAsync(string token);

        /// <summary>
        /// Sign out the user by removing all tokens linked with this user ID.
        /// </summary>
        Task<bool> SignOutByUserIdAsync(string userId);
    }
}