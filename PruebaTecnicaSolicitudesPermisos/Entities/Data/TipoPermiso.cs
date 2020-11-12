using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities.Data
{
    public partial class TipoPermiso
    {
        public TipoPermiso()
        {
            Permiso = new HashSet<Permiso>();
        }

        [Key]
        public int TipoPermisoId { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Permiso> Permiso { get; set; }
    }
}
