using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModeloDatos.Gestion
{
    public class DocumentosFacturasDTO
    {
        public int IdDocumento { get; set; }
        public int IdFactura { get; set; }
        public string Url { get; set; }
        public string Descripción { get; set; }
        public Nullable<bool> Activo { get; set; }
    }
}
