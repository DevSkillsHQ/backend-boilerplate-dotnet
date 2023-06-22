using backend.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace backend.Data
{
    public class PresentationVotesDBContex : DbContext
    {
        public DbSet<Presentation> Presentation { get; set; }
        public DbSet<Poll> Poll { get; set; }
        public DbSet<Option> Option { get; set; }
        public DbSet<Vote> Vote { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Presentations_database.db");
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Poll>()
                .HasOne(p => p.Options)
                .WithMany()
                .HasForeignKey(p => p.OptionsKey)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Vote>()
                .HasOne(v => v.Poll)
                .WithMany()
                .HasForeignKey(v => v.PollId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Vote>()
                .HasOne(v => v.Option)
                .WithMany()
                .HasForeignKey(v => v.OptionKey)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Presentation>()
                .HasOne(p => p.Polls)
                .WithMany()
                .HasForeignKey(p => p.PollsId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Presentation>()
                    .Property(e => e.CurrentPollIndex)
                .ValueGeneratedOnAdd();
        }


    }
}


