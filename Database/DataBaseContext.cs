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
            });

            modelBuilder.Entity<Campaign>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name).IsRequired();
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

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
