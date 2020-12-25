using System;
using System.Collections.Generic;

#nullable disable

namespace WarhammerCore.Data.Models
{
    public partial class SecondaryProfileEntity
    {
        public SecondaryProfileEntity()
        {
            Professions = new HashSet<ProfessionEntity>();
        }

        public string Id { get; set; }
        public short A { get; set; }
        public short W { get; set; }
        public short Sb { get; set; }
        public short Tb { get; set; }
        public short M { get; set; }
        public short Mag { get; set; }
        public short Ip { get; set; }
        public short Fp { get; set; }

        public virtual ICollection<ProfessionEntity> Professions { get; set; }
    }
}
