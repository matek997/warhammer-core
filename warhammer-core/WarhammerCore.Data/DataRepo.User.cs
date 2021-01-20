using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using WarhammerCore.Abstract.Models.User;
using WarhammerCore.Data.Models;

namespace WarhammerCore.Data
{
    public partial class DataRepo
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public async Task<string> CreateUserAsync(string email, string password)
        {
            string id = Guid.NewGuid().ToString();

            UserEntity userEntity = new UserEntity()
            {
                Id = id,
                Email = email,
                Password = password
            };

            await _db.Users.AddAsync(userEntity);
            await _db.SaveChangesAsync();

            return id;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public async Task<UserInfo> GetUserByIdAsync(string userId)
        {
            UserEntity row = await _db.Users.SingleOrDefaultAsync(x => x.Id == userId);

            if (row == default) return UserInfo.Empty;

            UserInfo user = new UserInfo(row.Id, row.Email, row.Password);

            return user;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public async Task<UserInfo> GetUserByEmailAsync(string email)
        {
            UserEntity row = await _db.Users.SingleOrDefaultAsync(x => x.Email == email);

            if (row == default) return UserInfo.Empty;

            UserInfo user = new UserInfo(row.Id, row.Email, row.Password);

            return user;
        }
    }
}
