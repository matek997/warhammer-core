using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WarhammerCore.Data.Models
{
    public partial class WarhammerDbContext : DbContext
    {
        public WarhammerDbContext()
        {
        }

        public WarhammerDbContext(DbContextOptions<WarhammerDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Advance> Advances { get; set; }
        public virtual DbSet<MainProfile> MainProfiles { get; set; }
        public virtual DbSet<Profession> Professions { get; set; }
        public virtual DbSet<ProfessionSkill> ProfessionSkills { get; set; }
        public virtual DbSet<ProfessionTalent> ProfessionTalents { get; set; }
        public virtual DbSet<ProfessionTrapping> ProfessionTrappings { get; set; }
        public virtual DbSet<SecondaryProfile> SecondaryProfiles { get; set; }
        public virtual DbSet<Skill> Skills { get; set; }
        public virtual DbSet<SkillList> SkillLists { get; set; }
        public virtual DbSet<Talent> Talents { get; set; }
        public virtual DbSet<Trapping> Trappings { get; set; }
        public virtual DbSet<VwProfessionSkillsWithParent> VwProfessionSkillsWithParents { get; set; }
        public virtual DbSet<VwSkillsWithParent> VwSkillsWithParents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Advance>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Advance");

                entity.Property(e => e.AdvanceTo)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ProfessionId)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.AdvanceToNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.AdvanceTo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Advance_To_Profession");

                entity.HasOne(d => d.Profession)
                    .WithMany()
                    .HasForeignKey(d => d.ProfessionId)
                    .HasConstraintName("FK_Advance_Profession");
            });

            modelBuilder.Entity<MainProfile>(entity =>
            {
                entity.ToTable("MainProfile");

                entity.Property(e => e.Id)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Profession>(entity =>
            {
                entity.ToTable("Profession");

                entity.Property(e => e.Id)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasMaxLength(2500)
                    .IsUnicode(false);

                entity.Property(e => e.Label)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.MainProfile)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Notes)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.SecondaryProfile)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Source)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.MainProfileNavigation)
                    .WithMany(p => p.Professions)
                    .HasForeignKey(d => d.MainProfile)
                    .HasConstraintName("FK_Profession_MainProfile");

                entity.HasOne(d => d.SecondaryProfileNavigation)
                    .WithMany(p => p.Professions)
                    .HasForeignKey(d => d.SecondaryProfile)
                    .HasConstraintName("FK_Profession_SecondaryProfile");
            });

            modelBuilder.Entity<ProfessionSkill>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ProfessionSkill");

                entity.Property(e => e.ProfessionId)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SkillId)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Profession)
                    .WithMany()
                    .HasForeignKey(d => d.ProfessionId)
                    .HasConstraintName("FK_ProfessionSkill_Profession");

                entity.HasOne(d => d.Skill)
                    .WithMany()
                    .HasForeignKey(d => d.SkillId)
                    .HasConstraintName("FK_ProfessionSkill_Skill");
            });

            modelBuilder.Entity<ProfessionTalent>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ProfessionTalent");

                entity.Property(e => e.ProfessionId)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TalentId)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Profession)
                    .WithMany()
                    .HasForeignKey(d => d.ProfessionId)
                    .HasConstraintName("FK_ProfessionTalent_Profession");

                entity.HasOne(d => d.Talent)
                    .WithMany()
                    .HasForeignKey(d => d.TalentId)
                    .HasConstraintName("FK_ProfessionTalent_Talent");
            });

            modelBuilder.Entity<ProfessionTrapping>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ProfessionTrapping");

                entity.Property(e => e.ProfessionId)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TrappingId)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Profession)
                    .WithMany()
                    .HasForeignKey(d => d.ProfessionId)
                    .HasConstraintName("FK_ProfessionTrapping_Profession");

                entity.HasOne(d => d.Trapping)
                    .WithMany()
                    .HasForeignKey(d => d.TrappingId)
                    .HasConstraintName("FK_ProfessionTrapping_Trapping");
            });

            modelBuilder.Entity<SecondaryProfile>(entity =>
            {
                entity.ToTable("SecondaryProfile");

                entity.Property(e => e.Id)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Skill>(entity =>
            {
                entity.ToTable("Skill");

                entity.Property(e => e.Id)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Label)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Operator)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TargetEnum)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SkillList>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("SkillList");

                entity.HasIndex(e => e.ParentId, "IX_SkillList");

                entity.Property(e => e.ChildId)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ParentId)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Child)
                    .WithMany()
                    .HasForeignKey(d => d.ChildId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SkillList_Skill_child");

                entity.HasOne(d => d.Parent)
                    .WithMany()
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_SkillList_Skill");
            });

            modelBuilder.Entity<Talent>(entity =>
            {
                entity.ToTable("Talent");

                entity.Property(e => e.Id)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Trapping>(entity =>
            {
                entity.ToTable("Trapping");

                entity.Property(e => e.Id)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwProfessionSkillsWithParent>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_ProfessionSkillsWithParents");

                entity.Property(e => e.Id)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ParentId)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ProfessionId)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwSkillsWithParent>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_SkillsWithParents");

                entity.Property(e => e.Id)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ParentId)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
