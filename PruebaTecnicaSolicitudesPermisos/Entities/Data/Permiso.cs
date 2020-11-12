using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Data
{
    public partial class Permiso
    {
        [Key]
        public int PermisoId { get; set; }
        public string NombreEmpleado { get; set; }
        public string ApellidosEmpleado { get; set; }

        [ForeignKey("TipoPermiso")]
        public int? TipoPermisoId { get; set; }
        public DateTime FechaPermiso { get; set; }

        public virtual TipoPermiso TipoPermiso { get; set; }
    }
}
