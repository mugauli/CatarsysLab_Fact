using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Paginador
{
    public class ProximosIngresosDTO
    {
        public Nullable<int> IdFactura { get; set; }
        public string Fecha { get; set; }
        public string Cliente { get; set; }
        public string Factura_Concepto { get; set; }
        public string Monto { get; set; }
        
    }
}
