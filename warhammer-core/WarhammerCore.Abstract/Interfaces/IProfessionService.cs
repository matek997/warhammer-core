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
    }
}
