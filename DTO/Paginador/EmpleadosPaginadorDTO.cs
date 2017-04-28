using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Paginador
{
    public class EmpleadosPaginadorDTO
    {
        public Nullable<int> IdEmpleado { get; set; }
        public string Nombre { get; set; }
        public string Puesto { get; set; }
        public string Email { get; set; }
        public string Movil { get; set; }
        public string Antiguedad { get; set; }
    }
}
