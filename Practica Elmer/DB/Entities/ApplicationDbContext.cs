using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Practica_Elmer.DB.Entities
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Empresa> Empresas { get; set; } = null!;
        public virtual DbSet<Producto> Productos { get; set; } = null!;
        public virtual DbSet<RelProductoSucursal> RelProductoSucursals { get; set; } = null!;
        public virtual DbSet<Sucursale> Sucursales { get; set; } = null!;
        public virtual DbSet<Historico> Historicos { get; set; } = null!;



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Empresa>(entity =>
            {
                entity.ToTable("Empresas", "Empresa");

                entity.Property(e => e.Activo).HasDefaultValueSql("((0))");

                entity.Property(e => e.Contrasena)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Correo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.ToTable("Productos", "Almacen");

                entity.Property(e => e.Activo).HasDefaultValueSql("((0))");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Empresa)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.EmpresaId)
                    .HasConstraintName("FK__Productos__Activ__60A75C0F");
            });

            modelBuilder.Entity<RelProductoSucursal>(entity =>
            {
                entity.ToTable("RelProductoSucursal", "Almacen");

                entity.Property(e => e.PrecioUnitario).HasColumnType("decimal(7, 2)");

                entity.HasOne(d => d.Producto)
                    .WithMany(p => p.RelProductoSucursals)
                    .HasForeignKey(d => d.ProductoId)
                    .HasConstraintName("FK__RelProduc__Produ__6383C8BA");

                entity.HasOne(d => d.Sucursal)
                    .WithMany(p => p.RelProductoSucursals)
                    .HasForeignKey(d => d.SucursalId)
                    .HasConstraintName("FK__RelProduc__Sucur__6477ECF3");
            });

            modelBuilder.Entity<Sucursale>(entity =>
            {
                entity.ToTable("Sucursales", "Sucursal");

                entity.Property(e => e.Activo).HasDefaultValueSql("((0))");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Ubicacion)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Empresa)
                    .WithMany(p => p.Sucursales)
                    .HasForeignKey(d => d.EmpresaId)
                    .HasConstraintName("FK__Sucursale__Activ__5CD6CB2B");
            });


            modelBuilder.Entity<Historico>(entity =>
            {
                entity.ToTable("Historico", "Ventas");

                entity.Property(e => e.FechaVenta)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.TotalPagado).HasColumnType("decimal(7, 2)");
            });



            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
