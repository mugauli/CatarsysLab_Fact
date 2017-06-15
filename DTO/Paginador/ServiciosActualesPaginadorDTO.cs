using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Paginador
{
    public class ServiciosActualesPaginadorDTO
    {
        public int IdProyecto_Asignacion { get; set; }
        public string Nombre { get; set; }
        public string Cliente { get; set; }
        public string Ultima_Factura { get; set; }
        public string Termino { get; set; }
        public string Semaforo { get; set; }
    }
}
