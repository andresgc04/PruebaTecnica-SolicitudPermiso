using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Entities.Data
{
    public partial class PermisosDBContext : DbContext
    {
        public PermisosDBContext()
        {
        }

        public PermisosDBContext(DbContextOptions<PermisosDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Permiso> Permiso { get; set; }
        public virtual DbSet<TipoPermiso> TipoPermiso { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=148.0.107.181; Database=PermisosDB; user id=AndresGc; password=Ag%04071997;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Permiso>(entity =>
            {
                entity.Property(e => e.PermisoId).HasColumnName("PermisoID");

                entity.Property(e => e.ApellidosEmpleado)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FechaPermiso).HasColumnType("datetime");

                entity.Property(e => e.NombreEmpleado)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TipoPermisoId).HasColumnName("TipoPermisoID");

                entity.HasOne(d => d.TipoPermiso)
                    .WithMany(p => p.Permiso)
                    .HasForeignKey(d => d.TipoPermisoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Permiso_TipoPermiso");
            });

            modelBuilder.Entity<TipoPermiso>(entity =>
            {
                entity.Property(e => e.TipoPermisoId).HasColumnName("TipoPermisoID");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
