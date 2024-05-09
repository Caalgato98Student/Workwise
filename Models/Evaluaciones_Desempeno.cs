namespace Papeleria.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Evaluaciones_Desempeno
    {
        [Key]
        public int ID_Evaluacion { get; set; }

        [DisplayName("Nombre")]

        public int ID_Empleado { get; set; }

        [DisplayName("Fecha de evaluación")]
        [Column(TypeName = "date")]
        public DateTime? Mes_Evaluacion { get; set; }

        [DisplayName("Calificación")]
        public decimal Calificacion { get; set; }

        [DisplayName("Comentarios")]
        [Column(TypeName = "text")]
        public string Comentarios { get; set; }

        public virtual Empleados Empleados { get; set; }
    }
}
