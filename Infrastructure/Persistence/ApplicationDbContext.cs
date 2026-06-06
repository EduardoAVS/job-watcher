using JobWatcher.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace JobWatcher.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<UserPreferences> UserPreferences => Set<UserPreferences>();
    public DbSet<Company> Companies => Set<Company>();
    public DbSet<JobSource> JobSources => Set<JobSource>();
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

            entity.Property(u => u.Id)
                .ValueGeneratedOnAdd();

            entity.Property(u => u.TelegramId)
                .IsRequired();

            entity.HasIndex(u => u.TelegramId)
                .IsUnique();

            entity.Property(u => u.CreatedAt)
                .IsRequired();
        });

        // =========================
        // UserPreferences
        // =========================
        modelBuilder.Entity<UserPreferences>(entity =>
        {
            entity.ToTable("UserPreferences");

            entity.HasKey(up => up.UserId);

            entity.Property(up => up.Level)
                .HasConversion<string>()
                .HasMaxLength(50)
                .IsRequired(false);

            entity.Property(up => up.CreatedAt)
                .IsRequired();

            entity.Property(up => up.UpdatedAt)
                .IsRequired();

            entity.HasOne<User>()
                .WithOne()
                .HasForeignKey<UserPreferences>(up => up.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // =========================
        // Company
        // =========================
        modelBuilder.Entity<Company>(entity =>
        {
            entity.ToTable("Companies");

            entity.HasKey(c => c.Id);

            entity.Property(c => c.Id)
                .ValueGeneratedOnAdd();

            entity.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(200);

            entity.HasIndex(c => c.Name)
                .IsUnique();

            entity.Property(c => c.CreatedAt)
                .IsRequired();
        });

        // =========================
        // JobSource
        // =========================
        modelBuilder.Entity<JobSource>(entity =>
        {
            entity.ToTable("JobSources");

            entity.HasKey(js => js.Id);

            entity.Property(js => js.Id)
                .ValueGeneratedOnAdd();

            entity.Property(js => js.CompanyId)
                .IsRequired();

            entity.Property(js => js.Url)
                .IsRequired()
                .HasMaxLength(1000);

            entity.HasIndex(js => js.Url)
                .IsUnique();

            entity.Property(js => js.SourceType)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(50);

            entity.Property(js => js.CreatedAt)
                .IsRequired();

            entity.Property(js => js.LastCheckedAt)
                .IsRequired(false);

            entity.HasOne(js => js.Company)
                .WithMany()
                .HasForeignKey(js => js.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // =========================
        // JobPosting
        // =========================
        modelBuilder.Entity<JobPosting>(entity =>
        {
            entity.ToTable("JobPostings");

            entity.HasKey(jp => jp.Id);

            entity.Property(jp => jp.Id)
                .ValueGeneratedOnAdd();

            entity.Property(jp => jp.CompanyId)
                .IsRequired();

            entity.Property(jp => jp.JobSourceId)
                .IsRequired();

            entity.Property(jp => jp.Title)
                .IsRequired()
                .HasMaxLength(300);

            entity.Property(jp => jp.Url)
                .IsRequired()
                .HasMaxLength(1000);

            entity.HasIndex(jp => jp.Url)
                .IsUnique();

            entity.Property(jp => jp.Level)
                .HasConversion<string>()
                .HasMaxLength(50)
                .IsRequired(false);

            entity.Property(jp => jp.Location)
                .HasMaxLength(200)
                .IsRequired(false);

            entity.Property(jp => jp.CreatedAt)
                .IsRequired();

            entity.Property(jp => jp.LastSeenAt)
                .IsRequired(false);

            entity.Property(jp => jp.UpdatedAt)
                .IsRequired(false);

            entity.HasOne(jp => jp.Company)
                .WithMany()
                .HasForeignKey(jp => jp.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(jp => jp.JobSource)
                .WithMany()
                .HasForeignKey(jp => jp.JobSourceId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // =========================
        // Notification
        // =========================
        modelBuilder.Entity<Notification>(entity =>
        {
            entity.ToTable("Notifications");

            entity.HasKey(n => new { n.UserId, n.JobPostingId });

            entity.Property(n => n.Status)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(50);

            entity.Property(n => n.RetryCount)
                .IsRequired();

            entity.Property(n => n.CreatedAt)
                .IsRequired();

            entity.Property(n => n.SentAt)
                .IsRequired(false);

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