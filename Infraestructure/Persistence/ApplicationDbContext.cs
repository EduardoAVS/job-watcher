using JobWatcher.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace JobWatcher.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<UserPreferences> UserPreferences => Set<UserPreferences>();
    public DbSet<TrackedPage> TrackedPages => Set<TrackedPage>();
    public DbSet<JobPosting> JobPostings => Set<JobPosting>();
    public DbSet<Notification> Notifications => Set<Notification>();

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // =========================
        // User
        // =========================
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("Users");

            entity.HasKey(u => u.Id);

            entity.HasIndex(u => u.TelegramId)
                  .IsUnique();
        });

        // =========================
        // UserPreferences (1:1)
        // =========================
        modelBuilder.Entity<UserPreferences>(entity =>
        {
            entity.ToTable("UserPreferences");

            entity.HasKey(p => p.UserId);

            entity.HasOne<User>()
                  .WithOne()
                  .HasForeignKey<UserPreferences>(p => p.UserId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // =========================
        // TrackedPage
        // =========================
        modelBuilder.Entity<TrackedPage>(entity =>
        {
            entity.ToTable("TrackedPages");

            entity.HasKey(p => p.Id);

            entity.HasIndex(p => p.Url)
                  .IsUnique();
        });

        // =========================
        // JobPosting
        // =========================
        modelBuilder.Entity<JobPosting>(entity =>
        {
            entity.ToTable("JobPostings");

            entity.HasKey(j => j.Id);

            entity.HasIndex(j => j.Url)
                  .IsUnique();

            entity.HasOne(j => j.TrackedPage)
                  .WithMany()
                  .HasForeignKey(j => j.TrackedPageId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // =========================
        // Notification (PK composta)
        // =========================
        modelBuilder.Entity<Notification>(entity =>
        {
            entity.ToTable("Notifications");

            entity.HasKey(n => new { n.UserId, n.JobPostingId });

            entity.HasOne<User>()
                  .WithMany()
                  .HasForeignKey(n => n.UserId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne<JobPosting>()
                  .WithMany()
                  .HasForeignKey(n => n.JobPostingId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }
}