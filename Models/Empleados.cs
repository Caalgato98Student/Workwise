namespace Papeleria.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Empleados
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Empleados()
        {
            Evaluaciones_Desempeno = new HashSet<Evaluaciones_Desempeno>();
        }

        [Key]
        public int ID_Empleado { get; set; }

        [DisplayName("Nombre")]
        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        [DisplayName("Apellido Paterno")]
        [Required]
        [StringLength(50)]
        public string Apellido_Paterno { get; set; }

        [DisplayName("Apellido Materno")]
        [StringLength(50)]
        public string Apellido_Materno { get; set; }

        [DisplayName("Fecha de Nacimiento")]
        [Column(TypeName = "date")]
        public DateTime Fecha_Nacimiento { get; set; }

        [DisplayName("Género")]
        [StringLength(10)]
        public string Genero { get; set; }

        [DisplayName("Dirección")]
        [Required]
        [StringLength(100)]
        public string Direccion { get; set; }

        [DisplayName("Ciudad")]
        [Required]
        [StringLength(50)]
        public string Ciudad { get; set; }

        [DisplayName("País")]
        [StringLength(50)]
        public string Pais { get; set; }

        [DisplayName("Teléfono")]
        [StringLength(20)]
        public string Telefono { get; set; }

        [DisplayName("Correo Electrónico")]
        [EmailAddress(ErrorMessage = "El formato del correo electrónico debe ser usuario@example.com.")]
        [Required]
        [StringLength(100)]
        public string Correo_Electronico { get; set; }

        [DisplayName("Fecha de Contratación")]
        [Column(TypeName = "date")]
        public DateTime? Fecha_Contratacion { get; set; }

        [DisplayName("Salario")]
        [Required]
        public decimal Salario { get; set; }

        [DisplayName("Departamento")]
        public int ID_Departamento { get; set; }

        [DisplayName("Puesto")]
        public int ID_Puesto { get; set; }

        [DisplayName("Turno")]
        public int ID_Turno { get; set; }

        public virtual Departamentos Departamentos { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Evaluaciones_Desempeno> Evaluaciones_Desempeno { get; set; }

        public virtual Puestos Puestos { get; set; }

        public virtual Horarios Horarios { get; set; }
    }
}
