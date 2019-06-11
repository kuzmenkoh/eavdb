using Microsoft.EntityFrameworkCore;

namespace EAVDB.Models
{
    public class EavContext : DbContext
    {
        public EavContext(DbContextOptions<EavContext> options) : base(options)
        { }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Attribute<Person>> PersonAttributes { get; set; }
        public DbSet<Record> Records { get; set; }
        public DbSet<Attribute<Record>> RecordAttributes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            BuildEntityModel<Person>(modelBuilder);
            BuildEntityModel<Record>(modelBuilder);
            modelBuilder.Entity<Record>()
                .HasOne(r => r.Person)
                .WithMany(p => p.Records)
                .HasForeignKey(r => r.PersonId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }

        protected void BuildEntityModel<TEntity>(ModelBuilder modelBuilder) where TEntity : Entity<TEntity>
        {
            modelBuilder.Entity<TEntity>()
                .HasKey(p => p.EntityId);
            modelBuilder.Entity<Attribute<TEntity>>()
                .HasKey(a => new { EntityID = a.EntityId, a.Name });
            modelBuilder.Entity<Attribute<TEntity>>()
                .HasOne(a => a.Entity)
                .WithMany(p => p.Attributes)
                .HasForeignKey(a => a.EntityId);
            modelBuilder.Entity<AttributeString<TEntity>>();
            modelBuilder.Entity<AttributeInt<TEntity>>();
        }
    }
}
