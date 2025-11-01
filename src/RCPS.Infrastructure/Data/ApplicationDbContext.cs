using Microsoft.EntityFrameworkCore;
using RCPS.Core.Entities;

namespace RCPS.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Client> Clients => Set<Client>();
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<ProjectMilestone> ProjectMilestones => Set<ProjectMilestone>();
    public DbSet<StatementOfWork> StatementsOfWork => Set<StatementOfWork>();
    public DbSet<ChangeRequest> ChangeRequests => Set<ChangeRequest>();
    public DbSet<TeamMember> TeamMembers => Set<TeamMember>();
    public DbSet<ProjectAssignment> ProjectAssignments => Set<ProjectAssignment>();
    public DbSet<EffortEntry> EffortEntries => Set<EffortEntry>();
    public DbSet<Invoice> Invoices => Set<Invoice>();
    public DbSet<InvoiceLine> InvoiceLines => Set<InvoiceLine>();
    public DbSet<Reminder> Reminders => Set<Reminder>();
    public DbSet<RevenueRecognition> RevenueRecognitions => Set<RevenueRecognition>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Client>(builder =>
        {
            builder.Property(x => x.Name).HasMaxLength(200);
            builder.Property(x => x.PrimaryContactEmail).HasMaxLength(200);
        });

        modelBuilder.Entity<Project>(builder =>
        {
            builder.Property(x => x.Name).HasMaxLength(200);
            builder.HasOne(x => x.Client)
                .WithMany(c => c.Projects)
                .HasForeignKey(x => x.ClientId);
        });

        modelBuilder.Entity<StatementOfWork>(builder =>
        {
            builder.Property(x => x.SOWNumber).HasMaxLength(100);
        });

        modelBuilder.Entity<ChangeRequest>(builder =>
        {
            builder.Property(x => x.Title).HasMaxLength(200);
        });

        modelBuilder.Entity<Invoice>(builder =>
        {
            builder.Property(x => x.InvoiceNumber).HasMaxLength(100);
            builder.HasMany(x => x.Lines)
                .WithOne(l => l.Invoice!)
                .HasForeignKey(l => l.InvoiceId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<TeamMember>(builder =>
        {
            builder.Property(x => x.FirstName).HasMaxLength(100);
            builder.Property(x => x.LastName).HasMaxLength(100);
        });

        modelBuilder.Entity<ProjectAssignment>(builder =>
        {
            builder.HasOne(x => x.Project)
                .WithMany(p => p.Assignments)
                .HasForeignKey(x => x.ProjectId);
            builder.HasOne(x => x.TeamMember)
                .WithMany(t => t.Assignments)
                .HasForeignKey(x => x.TeamMemberId);
        });

        modelBuilder.Entity<EffortEntry>(builder =>
        {
            builder.HasOne(x => x.Project)
                .WithMany(p => p.Efforts)
                .HasForeignKey(x => x.ProjectId);
            builder.HasOne(x => x.TeamMember)
                .WithMany(t => t.Efforts)
                .HasForeignKey(x => x.TeamMemberId);
        });

        modelBuilder.Entity<RevenueRecognition>(builder =>
        {
            builder.HasOne(x => x.Project)
                .WithMany(p => p.RevenueRecognitions)
                .HasForeignKey(x => x.ProjectId);
        });
    }
}
