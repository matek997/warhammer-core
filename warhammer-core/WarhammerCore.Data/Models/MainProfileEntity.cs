using System;
using System.Collections.Generic;

#nullable disable

namespace WarhammerCore.Data.Models
{
    public partial class MainProfileEntity
    {
        public MainProfileEntity()
        {
            Professions = new HashSet<ProfessionEntity>();
        }

        public string Id { get; set; }
        public short Ws { get; set; }
        public short Bs { get; set; }
        public short S { get; set; }
        public short T { get; set; }
        public short Ag { get; set; }
        public short Int { get; set; }
        public short Wp { get; set; }
        public short Fel { get; set; }

        public virtual ICollection<ProfessionEntity> Professions { get; set; }
    }
}
