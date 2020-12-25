using System.Collections.Generic;
using System.Threading.Tasks;
using WarhammerCore.Abstract.Models;

namespace WarhammerCore.Abstract.Interfaces
{
    /// <summary>
    /// Data repository. Changes have already been verified so we can change anything without any problem.
    /// </summary>
    public interface IDataRepo
    {
        /// <summary>
        /// Get list of all properties IDs. 
        /// </summary>
        Task<IEnumerable<string>> GetProfessionsAsync();

        /// <summary>
        /// Get profession object by the id.
        /// </summary>
        /// <param name="professionId">Unique id of the profession.</param>
        Task<Profession> GetProfessionAsync(string professionId);
    }
}
