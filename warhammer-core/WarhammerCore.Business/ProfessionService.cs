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
        public async Task<Profession> GetProfessionAsync(string professionId)
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

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public async Task<bool> CreateProfessionAsync(ProfessionCreationModel model)
        {
            Profession profession = new Profession(model.Id, model.Label, model.Description, model.Role, model.Notes, model.Source, model.IsAdvanced, model.MainProfile, model.SecondaryProfile, model.AdvanceFrom, model.AdvanceTo, new List<Skill>(), new List<string>(), new List<string>(), 0);
            return await _repo.CreateProfessionAsync(profession);
        }
    }
}