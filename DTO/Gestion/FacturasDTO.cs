using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Gestion
{
    public class FacturasDTO
    {
         public int Id_Factura { get; set; }
         public decimal Monto { get; set; }
         public string FechaFactura { get; set; }
    }
}
