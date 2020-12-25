using System.Collections.Generic;
using System.Linq;

namespace WarhammerCore.Abstract.Models
{
    public class Profession
    {
        #region Public Properties

        public string Id { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public string Role { get; set; }
        public bool IsAdvanced { get; set; }
        public MainProfile MainProfile { get; set; }
        public SecondaryProfile SecondaryProfile { get; set; }
        public List<string> AdvanceTo { get; set; }
        public List<Skill> Skills { get; set; }
        public List<string> Talents { get; set; }
        public List<string> Trappings { get; set; }
        public int NumberOfSkills { get; set; }
        public int NumberOfTalents { get; set; }
        public int NumberOfAdvances { get; set; }

        #endregion Public Properties

        public Profession(string id,
            string label,
            string description,
            string role,
            bool isAdvanced,
            MainProfile mainProfile,
            SecondaryProfile secondaryProfile,
            List<string> advanceTo,
            List<Skill> skills,
            List<string> talents,
            List<string> trappings,
            int numberOfAdvances)
        {
            Id = id;
            Label = label;
            Description = description;
            Role = role;
            IsAdvanced = isAdvanced;
            MainProfile = mainProfile;
            SecondaryProfile = secondaryProfile;
            AdvanceTo = advanceTo;
            Skills = skills;
            Talents = talents;
            Trappings = trappings;
            NumberOfTalents = talents.Count;
            NumberOfAdvances = numberOfAdvances;
            NumberOfSkills = GetSkillsCount();
        }

        /// <summary>
        /// Recursively get the count of skills.
        /// </summary>
        /// <returns></returns>
        private int GetSkillsCount()
        {
            return Skills.Sum((skill) => GetChildrenCount(skill, 0));
        }

        /// <summary>
        /// Recursively get the cound of skilsl children.
        /// </summary>
        private int GetChildrenCount(Skill skill, int count)
        {
            return skill.List.Count == 0 ? count + 1 : skill.List.Sum(childSkill => GetChildrenCount(childSkill, count));
        }
    }
}