using System;

namespace WarhammerCore.Abstract.Models.User
{
    /// <summary>
    /// User authorization information that contains token.
    /// </summary>
    public class UserAuthInfo
    {
        public string UserId { get; }
        public string Token { get; }

        public UserAuthInfo(string userId, string token)
        {
            if (string.IsNullOrEmpty(token)) throw new ArgumentNullException(nameof(token));

            UserId = userId;
            Token = token;
        }
    }
}
