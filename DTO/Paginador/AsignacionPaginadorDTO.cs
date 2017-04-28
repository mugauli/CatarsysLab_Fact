using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Paginador
{
    public class AsignacionPaginadorDTO
    {
        public int IdAsignacion { get; set; }
        public string Consultor { get; set; }
        public string Cliente { get; set; }
        public string Fecha_Inicio { get; set; }
        public string Duracion { get; set; }
        public string Fecha_Fin { get; set; }
        public string Prox_Facturacion { get; set; }
        public string Estado { get; set; }
    }
}
