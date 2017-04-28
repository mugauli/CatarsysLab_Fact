using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Paginador
{
    public class ProyectosPaginadorDTO
    {
        public Nullable<int> IdProyecto { get; set; }
        public string Cliente { get; set; }
        public string NombreProyecto { get; set; }
        public string Facturado { get; set; }
        public string RestaFacturar { get; set; }
        public string Total_Proyecto { get; set; }
        public string Prox_Facturacion { get; set; }
        public string Estado { get; set; }
        public string Comentarios { get; set; }
    }
}
