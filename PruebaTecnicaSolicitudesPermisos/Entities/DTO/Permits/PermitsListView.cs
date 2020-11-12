using System;
namespace Entities.DTO.Permits
{
    public class PermitsListView
    {
        public int PermisoId { get; set; }
        public string NombreEmpleado { get; set; }
        public string ApellidosEmpleado { get; set; }
        public string TipoPermiso { get; set; }
        public DateTime FechaPermiso { get; set; }
    }
}
