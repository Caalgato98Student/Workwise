namespace Papeleria.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Puestos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Puestos()
        {
            Empleados = new HashSet<Empleados>();
        }

        [Key]
        public int ID_Puesto { get; set; }

        public int ID_Departamento { get; set; }

        [DisplayName("Puesto")]
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [DisplayName("Descripción")]
        [Column(TypeName = "text")]
        [Required]
        public string Descripcion { get; set; }

        [DisplayName("Salario base")]
        public decimal? Salario_Base { get; set; }

        public virtual Departamentos Departamentos { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Empleados> Empleados { get; set; }
    }
}
