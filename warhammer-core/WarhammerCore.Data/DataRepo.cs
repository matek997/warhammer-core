using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarhammerCore.Abstract.Interfaces;
using WarhammerCore.Data.Models;
using ProfessionModel = WarhammerCore.Abstract.Models.Profession;
using Skill = WarhammerCore.Abstract.Models.Skill;

namespace WarhammerCore.Data
{
    public class DataRepo : IDataRepo
    {
        private readonly WarhammerDbContext _db;

        public DataRepo(WarhammerDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public async Task<IEnumerable<string>> GetProfessionsAsync()
        {
            return await _db.Professions.Select((profession) => profession.Id).ToListAsync();
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public async Task<ProfessionModel> GetProfessionAsync(string professionId)
        {
            Profession profession = await _db.Professions.SingleOrDefaultAsync(profession => profession.Id == professionId);

            if (profession == default) return null;

            WarhammerCore.Abstract.Models.MainProfile mainProfile = await GetMainProfileAsync(profession.MainProfile);
            WarhammerCore.Abstract.Models.SecondaryProfile secondaryProfile = await GetSecondaryProfileAsync(profession.SecondaryProfile);
            List<string> advances = await _db.Advances.Where(advance => advance.ProfessionId == professionId).Select(advance => advance.AdvanceTo).ToListAsync();

            List<string> professionTalents = await _db.ProfessionTalents.Where(professionTalent => professionTalent.ProfessionId == professionId).Select(professionTalent => professionTalent.TalentId).ToListAsync();
            List<string> talents = await _db.Talents.Where(talent => professionTalents.Contains(talent.Id)).Select(talent => talent.Text).ToListAsync();

            List<string> professionTrappings = await _db.ProfessionTrappings.Where(professionTrapping => professionTrapping.ProfessionId == professionId).Select(professionTrapping => professionTrapping.TrappingId).ToListAsync();
            List<string> trappings = await _db.Trappings.Where(trapping => professionTrappings.Contains(trapping.Id)).Select(trapping => trapping.Text).ToListAsync();
            var skills = GetSkillsAsync(professionId).Select(s => GetChildSkillsAsync(s)).ToList();
            return new ProfessionModel(professionId, profession.Label, profession.Description, profession.Role, profession.IsAdvanced, mainProfile, secondaryProfile, advances, skills, talents, trappings, profession.NumberOfAdvances);
        }

        private async Task<WarhammerCore.Abstract.Models.MainProfile> GetMainProfileAsync(string mainProfileId)
        {
            MainProfile mp = await _db.MainProfiles.SingleOrDefaultAsync(mainProfile => mainProfile.Id == mainProfileId);
            return new WarhammerCore.Abstract.Models.MainProfile(mp.Ws, mp.Bs, mp.S, mp.T, mp.Ag, mp.Int, mp.Wp, mp.Fel);
        }

        private async Task<WarhammerCore.Abstract.Models.SecondaryProfile> GetSecondaryProfileAsync(string secondaryProfileId)
        {
            SecondaryProfile sp = await _db.SecondaryProfiles.SingleOrDefaultAsync(secondaryProfile => secondaryProfile.Id == secondaryProfileId);
            return new WarhammerCore.Abstract.Models.SecondaryProfile(sp.A, sp.W, sp.Sb, sp.Tb, sp.M, sp.Mag, sp.Ip, sp.Fp);
        }

        /// <summary>
        /// Recursively get skills from the database for the given profession.
        /// </summary>
        private List<Skill> GetSkillsAsync(string professionId)
        {
            List<string> firstLevelProfessionSkillIds = _db.ProfessionSkills.Where(professionSkill => professionSkill.ProfessionId == professionId).Select(professionSkill => professionSkill.SkillId).ToList();
            return _db.Skills.Where(skill => firstLevelProfessionSkillIds.Contains(skill.Id)).Select(s => new Skill(s.Id, s.Type, s.TargetEnum, s.Label, s.Operator)).ToList();
        }

        /// <summary>
        /// Recursively get skills children from the database.
        /// </summary>
        private Skill GetChildSkillsAsync(Skill skillEntity)
        {
            List<string> childSkillIds = _db.SkillLists.Where(skillList => skillList.ParentId == skillEntity.Id).Select(skillList => skillList.ChildId).ToList();

            Skill skill = new Skill(skillEntity.Id, skillEntity.Type, skillEntity.TargetEnum, skillEntity.Key, skillEntity.Operator);

            if (childSkillIds.Count == 0) return skill;

            List<Skill> childSkillsAsync = _db.Skills.Where(skill => childSkillIds.Contains(skill.Id)).Select(childSkill => new Skill(childSkill.Id, childSkill.Type, childSkill.TargetEnum, childSkill.Label, childSkill.Operator)).ToList();
            skill.List = childSkillsAsync.Select(s => GetChildSkillsAsync(s)).ToList();
            return skill;
        }
    }
}