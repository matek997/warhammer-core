using System.Collections.Generic;
using System.Threading.Tasks;
using WarhammerCore.Abstract.Models;

namespace WarhammerCore.Abstract.Interfaces
{
    /// <summary>
    /// Management of the professions. Validate whether the user requests before changing the DB.
    /// </summary>
    public interface IProfessionService
    {
        /// <summary>
        /// Get list of all properties IDs. 
        /// </summary>
        Task<IEnumerable<string>>GetProfessionsAsync();

        /// <summary>
        /// Get profession object by the id.
        /// </summary>
        /// <param name="professionId">Unique id of the profession.</param>
        Task<Profession> GetProfessionAsync(string professionId);

        /// <summary>
        /// Create new profession in the database.
        /// </summary>
        /// <param name="professionCreationModel">Model with basic properties for profession.</param>
        /// <returns>Bool based on the request status.</returns>
        Task<bool> CreateProfessionAsync(ProfessionCreationModel professionCreationModel);
    }
}
