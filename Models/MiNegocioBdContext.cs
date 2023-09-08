using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace NegocioApi.Models;

public partial class MiNegocioBdContext : DbContext
{
    public MiNegocioBdContext()
    {
    }

    public MiNegocioBdContext(DbContextOptions<MiNegocioBdContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categoria> Categoria { get; set; }

    public virtual DbSet<Entrada> Entradas { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Salida> Salida { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=desktop-c6smnv4\\sqlexpress; database=miNegocioBD; trusted_connection=true; trustservercertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("PK__Categori__A3C02A10283D679D");

            entity.Property(e => e.Nombre)
                .HasMaxLength(59)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Entrada>(entity =>
        {
            entity.HasKey(e => e.IdEntrada).HasName("PK__Entradas__BB164DEA13B9114C");

            entity.ToTable(tb =>
                {
                    tb.HasTrigger("update_product_quantity_on_entry_delete");
                    tb.HasTrigger("update_product_quantity_on_entry_update");
                    tb.HasTrigger("update_producto_quantity");
                });

            entity.Property(e => e.Costo).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Fecha).HasColumnType("date");

            entity.HasOne(d => d.oProducto).WithMany(p => p.Entrada)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK_Producto_Entrada");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PK__Producto__0988921023451CE5");

            entity.ToTable("Producto");

            entity.Property(e => e.Marca)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(59)
                .IsUnicode(false);
            entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.oCategoria).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdCategoria)
                .HasConstraintName("FK_IdCategoria");
        });

        modelBuilder.Entity<Salida>(entity =>
        {
            entity.HasKey(e => e.IdSalida).HasName("PK__Salida__5D69EC72E19CD683");

            entity.ToTable(tb =>
                {
                    tb.HasTrigger("update_product_quantity_salida_on_entry_delete");
                    tb.HasTrigger("update_product_quantity_salida_on_entry_update");
                    tb.HasTrigger("update_producto_quantity_salida");
                });

            entity.Property(e => e.Delivery).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Descuento).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Fecha).HasColumnType("date");
            entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Total).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.oProducto).WithMany(p => p.Salida)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK_Producto_Salida");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
