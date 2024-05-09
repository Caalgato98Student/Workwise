using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Papeleria.Models
{
    public partial class ClasesDB : DbContext
    {
        public ClasesDB()
            : base("name=ClasesDB")
        {
        }

        public virtual DbSet<Departamentos> Departamentos { get; set; }
        public virtual DbSet<Empleados> Empleados { get; set; }
        public virtual DbSet<Evaluaciones_Desempeno> Evaluaciones_Desempeno { get; set; }
        public virtual DbSet<Horarios> Horarios { get; set; }
        public virtual DbSet<Puestos> Puestos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Departamentos>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Departamentos>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<Departamentos>()
                .HasMany(e => e.Puestos)
                .WithRequired(e => e.Departamentos)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Departamentos>()
                .HasMany(e => e.Empleados)
                .WithRequired(e => e.Departamentos)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Empleados>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Empleados>()
                .Property(e => e.Apellido_Paterno)
                .IsUnicode(false);

            modelBuilder.Entity<Empleados>()
                .Property(e => e.Apellido_Materno)
                .IsUnicode(false);

            modelBuilder.Entity<Empleados>()
                .Property(e => e.Genero)
                .IsUnicode(false);

            modelBuilder.Entity<Empleados>()
                .Property(e => e.Direccion)
                .IsUnicode(false);

            modelBuilder.Entity<Empleados>()
                .Property(e => e.Ciudad)
                .IsUnicode(false);

            modelBuilder.Entity<Empleados>()
                .Property(e => e.Pais)
                .IsUnicode(false);

            modelBuilder.Entity<Empleados>()
                .Property(e => e.Telefono)
                .IsUnicode(false);

            modelBuilder.Entity<Empleados>()
                .Property(e => e.Correo_Electronico)
                .IsUnicode(false);

            modelBuilder.Entity<Empleados>()
                .Property(e => e.Salario)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Empleados>()
                .HasMany(e => e.Evaluaciones_Desempeno)
                .WithRequired(e => e.Empleados)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Evaluaciones_Desempeno>()
                .Property(e => e.Calificacion)
                .HasPrecision(5, 2);

            modelBuilder.Entity<Evaluaciones_Desempeno>()
                .Property(e => e.Comentarios)
                .IsUnicode(false);

            modelBuilder.Entity<Horarios>()
                .Property(e => e.Nombre_Turno)
                .IsUnicode(false);

            modelBuilder.Entity<Horarios>()
                .Property(e => e.Dias_Semana)
                .IsUnicode(false);

            modelBuilder.Entity<Horarios>()
                .HasMany(e => e.Empleados)
                .WithRequired(e => e.Horarios)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Puestos>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Puestos>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<Puestos>()
                .Property(e => e.Salario_Base)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Puestos>()
                .HasMany(e => e.Empleados)
                .WithRequired(e => e.Puestos)
                .WillCascadeOnDelete(false);
        }
    }
}
