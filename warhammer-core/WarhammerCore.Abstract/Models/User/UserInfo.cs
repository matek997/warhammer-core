using System;

namespace WarhammerCore.Abstract.Models.User
{
    /// <summary>
    /// User account.
    /// </summary>
    public class UserInfo
    {
        public string UserId { get; }
        public string Email { get; }
        public string HashedPassword { get; }

        public UserInfo(string userId, string email, string hashedPassword)
        {
            if (string.IsNullOrEmpty(email)) throw new ArgumentNullException(nameof(email));
            if (string.IsNullOrEmpty(hashedPassword)) throw new ArgumentNullException(nameof(hashedPassword));

            UserId = userId;
            Email = email;
            HashedPassword = hashedPassword;
        }

        /// <summary>
        /// User that doesn't exist.
        /// </summary>
        public static UserInfo Empty => new UserInfo(default, "...", "...");

        /// <summary>
        /// Whether the user was not found in the database.
        /// </summary>
        /// <param name="userInfo">Record from the database or an empty one.</param>
        public static bool IsNullOrEmpty(UserInfo userInfo) => userInfo.UserId == default;
    }
}