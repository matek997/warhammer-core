using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarhammerCore.Abstract.Models;
using WarhammerCore.Data.Models;

namespace WarhammerCore.Data
{
    public partial class DataRepo
    {
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
        public async Task<Profession> GetProfessionAsync(string professionId)
        {
            ProfessionEntity profession = await _db.Professions.SingleOrDefaultAsync(profession => profession.Id == professionId);

            if (profession == default) return null;

            MainProfile mainProfile = await GetMainProfileAsync(profession.MainProfile);
            SecondaryProfile secondaryProfile = await GetSecondaryProfileAsync(profession.SecondaryProfile);

            List<string> advancesTo = await _db.Advances.Where(advance => advance.ProfessionId == professionId).Select(advance => advance.AdvanceTo).ToListAsync();
            List<string> advancesFrom = await _db.Advances.Where(advance => advance.AdvanceTo == professionId).Select(advance => advance.AdvanceTo).ToListAsync();

            List<string> professionTalents = await _db.ProfessionTalents.Where(professionTalent => professionTalent.ProfessionId == professionId).Select(professionTalent => professionTalent.TalentId).ToListAsync();
            List<string> talents = await _db.Talents.Where(talent => professionTalents.Contains(talent.Id)).Select(talent => talent.Text).ToListAsync();

            List<string> professionTrappings = await _db.ProfessionTrappings.Where(professionTrapping => professionTrapping.ProfessionId == professionId).Select(professionTrapping => professionTrapping.TrappingId).ToListAsync();
            List<string> trappings = await _db.Trappings.Where(trapping => professionTrappings.Contains(trapping.Id)).Select(trapping => trapping.Text).ToListAsync();

            List<Skill> skills = GetSkills(professionId).Select(s => GetChildSkills(s)).ToList();

            return new Profession(professionId, profession.Label, profession.Description, profession.Role, profession.Notes, profession.Source, profession.IsAdvanced, mainProfile, secondaryProfile, advancesFrom, advancesTo, skills, talents, trappings, profession.NumberOfAdvances);
        }

        private async Task<MainProfile> GetMainProfileAsync(string mainProfileId)
        {
            MainProfileEntity mp = await _db.MainProfiles.SingleOrDefaultAsync(mainProfile => mainProfile.Id == mainProfileId);
            return new MainProfile(mp.Ws, mp.Bs, mp.S, mp.T, mp.Ag, mp.Int, mp.Wp, mp.Fel);
        }

        private async Task<SecondaryProfile> GetSecondaryProfileAsync(string secondaryProfileId)
        {
            SecondaryProfileEntity sp = await _db.SecondaryProfiles.SingleOrDefaultAsync(secondaryProfile => secondaryProfile.Id == secondaryProfileId);
            return new SecondaryProfile(sp.A, sp.W, sp.Sb, sp.Tb, sp.M, sp.Mag, sp.Ip, sp.Fp);
        }

        /// <summary>
        /// Recursively get skills from the database for the given profession.
        /// </summary>
        private List<Skill> GetSkills(string professionId)
        {
            List<string> firstLevelProfessionSkillIds = _db.ProfessionSkills.Where(professionSkill => professionSkill.ProfessionId == professionId).Select(professionSkill => professionSkill.SkillId).ToList();
            return _db.Skills.Where(skill => firstLevelProfessionSkillIds.Contains(skill.Id))
                             .Select(s => new Skill(s.Id, s.Type, s.TargetEnum, s.Label, s.Operator)).ToList();
        }

        /// <summary>
        /// Recursively get skills children from the database.
        /// </summary>
        private Skill GetChildSkills(Skill skillEntity)
        {
            List<string> childSkillIds = _db.SkillLists.Where(skillList => skillList.ParentId == skillEntity.Id).Select(skillList => skillList.ChildId).ToList();

            Skill skill = new Skill(skillEntity.Id, skillEntity.Type, skillEntity.TargetEnum, skillEntity.Key, skillEntity.Operator);

            if (childSkillIds.Count == 0) return skill;

            List<Skill> childSkillsAsync = _db.Skills.Where(skill => childSkillIds.Contains(skill.Id))
                                                     .Select(childSkill => new Skill(childSkill.Id, childSkill.Type, childSkill.TargetEnum, childSkill.Label, childSkill.Operator))
                                                     .ToList();
            skill.List = childSkillsAsync.Select(s => GetChildSkills(s)).ToList();
            return skill;
        }
    }
}