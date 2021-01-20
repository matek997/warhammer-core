using System.Collections.Generic;

namespace WarhammerCore.Abstract.Models
{
    public class Skill
    {
        #region Public Properties

        public string Id { get; set; }
        public string Type { get; set; }
        public string TargetEnum { get; set; }
        public string Key { get; set; }
        public string Operator { get; set; }
        public List<Skill> List { get; set; }

        #endregion Public Properties

        /// <summary>
        /// Convert database object to back-end/front-end model. Skill without any children.
        /// </summary>
        public Skill(string id, string type, string targetEnum, string key, string op)
        {
            Id = id;
            Type = type;
            TargetEnum = targetEnum;
            Key = key;
            Operator = op;
            List = new List<Skill>();
        }

        /// <summary>
        /// Convert database object to back-end/front-end model. Skill with children.
        /// </summary>
        public Skill(Skill skill, List<Skill> list)
        {
            Id = skill.Id;
            Type = skill.Type;
            TargetEnum = skill.TargetEnum;
            Key = skill.Key;
            Operator = skill.Operator;
            List = list;
        }
    }
}