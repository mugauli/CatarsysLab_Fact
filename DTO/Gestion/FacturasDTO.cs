using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Gestion
{
    public class FacturasDTO
    {
        public int IdFactura { get; set; }
        public int IdCliente { get; set; }
        public int IdEmpresa { get; set; }
        public Nullable<int> IdAsignacion { get; set; }
        public Nullable<int> IdProyecto { get; set; }
        public Nullable<int> C_Id_IVA { get; set; }
        public Nullable<int> C_Id_Moneda { get; set; }
        public Nullable<int> C_Id_Tipo_Cambio { get; set; }
        public Nullable<int> C_Id_Metodo_Pago { get; set; }
        public string Tipo_Factura { get; set; }
        public Nullable<System.DateTime> Fecha_Inicio_Factura { get; set; }
        public Nullable<System.DateTime> Fecha_fin_Factura { get; set; }
        public string Fecha_Inicio_FacturaSTR { get; set; }
        public string Fecha_fin_FacturaSTR { get; set; }
        public Nullable<short> No_Factura { get; set; }
        public Nullable<decimal> Descuento_Factura { get; set; }
        public Nullable<decimal> Monto_Factura { get; set; }
        public Nullable<short> Ultimos_4_digitos_Factura { get; set; }
        public short Id_Estado_Factura { get; set; }
    }
}
