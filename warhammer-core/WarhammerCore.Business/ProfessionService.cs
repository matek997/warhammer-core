using System.Collections.Generic;
using System.Threading.Tasks;
using WarhammerCore.Abstract.Interfaces;
using WarhammerCore.Abstract.Models;

namespace WarhammerCore.Business
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public class ProfessionService : IProfessionService
    {
        private readonly IDataRepo _repo;
        public ProfessionService(IDataRepo repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public async Task<Profession> GetProfession(string professionId)
        {
            return await _repo.GetProfessionAsync(professionId);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public async Task<IEnumerable<string>> GetProfessionsAsync()
        {
            return await _repo.GetProfessionsAsync();
        }
    }
}
