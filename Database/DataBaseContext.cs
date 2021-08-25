using Microsoft.EntityFrameworkCore;

namespace NatStats.Database
{
    public partial class DataBaseContext : DbContext
    {
        public DataBaseContext() { }

        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options) { }

        public virtual DbSet<Character> Character { get; set; }
        public virtual DbSet<Campaign> Campaign { get; set; }
        public virtual DbSet<Class> Class { get; set; }
        public virtual DbSet<Proficiency> Proficiency { get; set; }
        public virtual DbSet<Skill> Skill { get; set; }
        public virtual DbSet<Combat> Combat { get; set; }
        public virtual DbSet<Session> Session { get; set; }
        public virtual DbSet<RollHeader> RollHeader { get; set; }
        public virtual DbSet<Roll> Roll { get; set; }
        public virtual DbSet<RollRecipient> RollRecipient { get; set; }
        public virtual DbSet<DamageType> DamageType { get; set; }
        public virtual DbSet<Ability> Ability { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=Database/NatStat.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Character>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.CampaignId).IsRequired();

                entity.Property(e => e.ClassId);

                entity.Property(e => e.Strength).IsRequired();

                entity.Property(e => e.Dexterity).IsRequired();

                entity.Property(e => e.Constitution).IsRequired();

                entity.Property(e => e.Intelligence).IsRequired();

                entity.Property(e => e.Wisdom).IsRequired();

                entity.Property(e => e.Charisma).IsRequired();

                entity.Property(e => e.ProficiencyBonus).IsRequired();

                entity.Property(e => e.Level);
            });

            modelBuilder.Entity<Campaign>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.InCombat);
            });

            modelBuilder.Entity<Class>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Proficiency>(entity =>
            {
                entity.HasKey(entity => entity.CharacterId);

                entity.HasKey(entity => entity.SkillId);
            });

            modelBuilder.Entity<Skill>(entity =>
            {
                entity.HasKey(entity => entity.Id);

                entity.Property(entity => entity.Name).IsRequired();

                entity.Property(entity => entity.Base).IsRequired();
            });

            modelBuilder.Entity<Combat>(entity =>
            {
                entity.HasKey(entity => entity.Id);

                entity.Property(entity => entity.CampaignId).IsRequired();

                entity.Property(entity => entity.Number);

                entity.Property(entity => entity.Description);
            });

            modelBuilder.Entity<Session>(entity =>
            {
                entity.HasKey(entity => entity.Id);

                entity.Property(entity => entity.CampaignId).IsRequired();

                entity.Property(entity => entity.Number);

                entity.Property(entity => entity.Description);
            });

            modelBuilder.Entity<RollHeader>(entity =>
            {
                entity.HasKey(entity => entity.Id);

                entity.Property(entity => entity.CharacterId);

                entity.Property(entity => entity.CombatId);

                entity.Property(entity => entity.SessionId);

                entity.Property(entity => entity.Name);

                entity.Property(entity => entity.RollType);

                entity.Property(entity => entity.FinalValue).IsRequired();
            });

            modelBuilder.Entity<Roll>(entity =>
            {
                entity.HasKey(entity => entity.Id);

                entity.Property(entity => entity.HeaderId).IsRequired();

                entity.Property(entity => entity.Description);

                entity.Property(entity => entity.Modifier).IsRequired();

                entity.Property(entity => entity.BonusModifier).IsRequired();

                entity.Property(entity => entity.DiceSides).IsRequired();

                entity.Property(entity => entity.DiceRoll).IsRequired();

                entity.Property(entity => entity.Total);

                entity.Property(entity => entity.IsFinal).IsRequired();
            });

            modelBuilder.Entity<RollRecipient>(entity =>
            {
                entity.HasKey(entity => entity.CharacterId);

                entity.HasKey(entity => entity.HeaderId);
            });

            modelBuilder.Entity<DamageType>(entity =>
            {
                entity.HasKey(entity => entity.Id);

                entity.Property(entity => entity.Name);
            });

            modelBuilder.Entity<Ability>(entity =>
            {
                entity.HasKey(entity => entity.Id);

                entity.Property(entity => entity.CharacterId).IsRequired();

                entity.Property(entity => entity.Name).IsRequired();

                entity.Property(entity => entity.HitCheckBase);

                entity.Property(entity => entity.HitCheckBonus);

                entity.Property(entity => entity.Effect1EffectType);

                entity.Property(entity => entity.Effect1DiceCount);

                entity.Property(entity => entity.Effect1DiceSides);

                entity.Property(entity => entity.Effect1Base);

                entity.Property(entity => entity.Effect1Bonus);

                entity.Property(entity => entity.Effect1DamageTypeId);

                entity.Property(entity => entity.HasSecondaryEffect);

                entity.Property(entity => entity.Effect2Base);

                entity.Property(entity => entity.Effect2Bonus);

                entity.Property(entity => entity.Effect2DiceCount);

                entity.Property(entity => entity.Effect2DiceSides);

                entity.Property(entity => entity.Effect2Base);

                entity.Property(entity => entity.Effect2Bonus);

                entity.Property(entity => entity.Effect2DamageTypeId);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
