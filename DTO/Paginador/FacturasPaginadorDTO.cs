using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Paginador
{
    public class FacturasPaginadorDTO
    {
        public Nullable<int> IdFactura { get; set; }
        public string Tipo { get; set; }
        public string Cliente { get; set; }
        public string Concepto { get; set; }
        public string Monto { get; set; }
        public string Facturado { get; set; }
        public string DiaFacturacion { get; set; }
        public string estado { get; set; }
    }
}
