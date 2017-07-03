using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Gestion
{
    public class PagosFacturasDTO
    {
        public int IdPago { get; set; }
        public int IdFactura { get; set; }
        public string Descripcion { get; set; }
        public Nullable<bool> Activo { get; set; }
        public Nullable<decimal> Monto { get; set; }
        public string Concepto { get; set; }
        public virtual ICollection<DocumentosFacturasDTO> DocumentosFacturas { get; set; }
    }
}
