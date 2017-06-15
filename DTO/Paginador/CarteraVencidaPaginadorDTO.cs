using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Paginador
{
    public class CarteraVencidaPaginadorDTO
    {
        public int IdFactura { get; set; }
        public string Cliente { get; set; }
        public string Dias { get; set; }
        public string Monto { get; set; }
        public string Semaforo { get; set; }
    }
}
