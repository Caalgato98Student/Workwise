namespace Papeleria.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Horarios
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Horarios()
        {
            Empleados = new HashSet<Empleados>();
        }

        [Key]
        public int ID_Turno { get; set; }

        [DisplayName("Turno")]
        [Required]
        [StringLength(20)]
        public string Nombre_Turno { get; set; }

        [DisplayName("Días de la semana")]
        [StringLength(20)]
        public string Dias_Semana { get; set; }

        [DisplayName("Hora de entrada")]
        public TimeSpan Hora_Inicio { get; set; }

        [DisplayName("Hora de salida")]
        public TimeSpan Hora_Fin { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Empleados> Empleados { get; set; }
    }
}
