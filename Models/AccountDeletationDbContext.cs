using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AccountDeletion.Models;

public partial class AccountDeletationDbContext : DbContext
{
    public AccountDeletationDbContext()
    {
    }

    public AccountDeletationDbContext(DbContextOptions<AccountDeletationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<App> Apps { get; set; }

    public virtual DbSet<AppConfig> AppConfigs { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRequest> UserRequests { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;database=account_deletation_db;user id=root;password=12345", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.36-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<App>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("app");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AppId)
                .HasMaxLength(22)
                .HasColumnName("app_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .HasColumnName("name");
        });

        modelBuilder.Entity<AppConfig>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("app_config");

            entity.HasIndex(e => e.AppId, "fk_app_config_app_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AppId).HasColumnName("app_id");
            entity.Property(e => e.BannerColor)
                .HasMaxLength(10)
                .HasColumnName("banner_color");
            entity.Property(e => e.ButtonColor)
                .HasMaxLength(10)
                .HasColumnName("button_color");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.FormColor)
                .HasMaxLength(10)
                .HasColumnName("form_color");
            entity.Property(e => e.InputLabel)
                .HasMaxLength(45)
                .HasColumnName("input_label");
            entity.Property(e => e.Logo)
                .HasMaxLength(100)
                .HasColumnName("logo");
            entity.Property(e => e.Placeholder)
                .HasMaxLength(45)
                .HasColumnName("placeholder");

            entity.HasOne(d => d.App).WithMany(p => p.AppConfigs)
                .HasForeignKey(d => d.AppId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_app_config_app");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("user");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.DateOfBirth)
                .HasMaxLength(45)
                .HasColumnName("date_of_birth");
            entity.Property(e => e.Email)
                .HasMaxLength(45)
                .HasColumnName("email");
            entity.Property(e => e.Password)
                .HasMaxLength(120)
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(45)
                .HasColumnName("phone");
            entity.Property(e => e.UserName)
                .HasMaxLength(45)
                .HasColumnName("user_name");
        });

        modelBuilder.Entity<UserRequest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("user_request");

            entity.HasIndex(e => e.AppId, "fk_user_request_app_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AppId).HasColumnName("app_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Data)
                .HasMaxLength(45)
                .HasColumnName("data");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("'pending'")
                .HasColumnType("enum('pending','success','failed')")
                .HasColumnName("status");

            entity.HasOne(d => d.App).WithMany(p => p.UserRequests)
                .HasForeignKey(d => d.AppId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_user_request_app");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
