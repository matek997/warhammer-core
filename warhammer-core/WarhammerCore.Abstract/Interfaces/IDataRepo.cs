using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WarhammerCore.Abstract.Models;
using WarhammerCore.Abstract.Models.User;

namespace WarhammerCore.Abstract.Interfaces
{
    /// <summary>
    /// Data repository. Changes have already been verified so we can change anything without any problem.
    /// </summary>
    public interface IDataRepo
    {
        #region Profession

        /// <summary>
        /// Get list of all properties IDs.
        /// </summary>
        Task<IEnumerable<string>> GetProfessionsAsync();

        /// <summary>
        /// Get profession object by the id.
        /// </summary>
        /// <param name="professionId">Unique id of the profession.</param>
        Task<Profession> GetProfessionAsync(string professionId);

        /// <summary>
        /// Create new profession in the database.
        /// </summary>
        /// <param name="profession">Created profession with all elements set.</param>
        /// <returns>Bool based on the request status.</returns>
        Task<bool> CreateProfessionAsync(Profession profession);

        #endregion Profession

        #region User

        /// <summary>
        /// Create user in the database.
        /// </summary>
        /// <param name="userInfo">User details from registration page.</param>
        /// <returns>ID of the user.</returns>
        Task<string> CreateUserAsync(string email, string password);

        /// <summary>
        /// Get the user by his/her unique ID.
        /// </summary>
        /// <param name="userId">Unique user ID (GUID).</param>
        /// <returns>User account information or an EMPTY user when user was not found in the database.</returns>
        Task<UserInfo> GetUserByIdAsync(string userId);

        /// <summary>
        /// Get the user by his/her unique mail address.
        /// </summary>
        /// <returns>User account information or an EMPTY user when user was not found in the database.</returns>
        Task<UserInfo> GetUserByEmailAsync(string email);

        #endregion User
    }
}