using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Linq;
#nullable disable

namespace TodoApi.Models
{
    public partial class TodoApiContext : DbContext
    {
        public TodoApiContext()
        {
        }

        public TodoApiContext(DbContextOptions<TodoApiContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblStatus> TblStatuses { get; set; }
        public virtual DbSet<TblTodo> TblTodos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("DataSource=.\\Database\\TodoApi.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblStatus>(entity =>
            {
                entity.ToTable("tbl_status");

                entity.Property(e => e.Id)
                    .HasColumnType("integer")
                    .HasColumnName("id");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasColumnName("status");
            });

            modelBuilder.Entity<TblTodo>(entity =>
            {
                entity.ToTable("tbl_Todo");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.StatusId)
                    .HasColumnType("integer")
                    .HasColumnName("status_id");

                entity.Property(e => e.Todo)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasColumnName("todo");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.TblTodos)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
