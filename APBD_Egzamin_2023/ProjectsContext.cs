using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProjectsAndTasks;

public partial class ProjectsContext : DbContext
{
    public ProjectsContext()
    {
    }

    public ProjectsContext(DbContextOptions<ProjectsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<Task> Tasks { get; set; }

    public virtual DbSet<TaskType> TaskTypes { get; set; }

    public virtual DbSet<TeamMember> TeamMembers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=NBA-140-E1-PL;Initial Catalog=Projects;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.IdProject).HasName("Project_pk");

            entity.ToTable("Project");

            entity.Property(e => e.Deadline).HasColumnType("date");
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Task>(entity =>
        {
            entity.HasKey(e => e.IdTask).HasName("Task_pk");

            entity.ToTable("Task");

            entity.Property(e => e.Deadline).HasColumnType("date");
            entity.Property(e => e.Description).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.IdAssignedToNavigation).WithMany(p => p.TaskIdAssignedToNavigations)
                .HasForeignKey(d => d.IdAssignedTo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Task_TeamMember2");

            entity.HasOne(d => d.IdCreatorNavigation).WithMany(p => p.TaskIdCreatorNavigations)
                .HasForeignKey(d => d.IdCreator)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Task_TeamMember1");

            entity.HasOne(d => d.IdProjectNavigation).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.IdProject)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Task_Project");

            entity.HasOne(d => d.IdTaskTypeNavigation).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.IdTaskType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Task_TaskType");
        });

        modelBuilder.Entity<TaskType>(entity =>
        {
            entity.HasKey(e => e.IdTaskType).HasName("TaskType_pk");

            entity.ToTable("TaskType");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<TeamMember>(entity =>
        {
            entity.HasKey(e => e.IdTeamMember).HasName("TeamMember_pk");

            entity.ToTable("TeamMember");

            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
