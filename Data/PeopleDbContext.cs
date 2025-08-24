using Microsoft.EntityFrameworkCore;
using PeopleAPI.Models;

namespace PeopleAPI.Data
{
    public class PeopleDbContext : DbContext
    {
        public PeopleDbContext(DbContextOptions<PeopleDbContext> options)
            : base(options)
        {
        }

        public DbSet<PersonModel> People { get; set; }
        public DbSet<ProfessionModel> Professions { get; set; }
        public DbSet<HobbyModel> Hobbies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //person-profession relationship (one-to-many)
            modelBuilder.Entity<PersonModel>()
                .HasOne(p => p.Profession) //person has 1 profession
                .WithMany() //profession has many people
                .HasForeignKey(p => p.ProfessionId)
                .IsRequired(false) //allows null profession
                .OnDelete(DeleteBehavior.SetNull);

            //person-hobby relationship (many-to-many) (auto join table)
            modelBuilder.Entity<PersonModel>()
                .HasMany(p => p.Hobbies) //person has many hobbies
                .WithMany() 
                .UsingEntity(j => j.ToTable("PersonHobbies"));
        }       
    
    }
}
