using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Gestion
{
    public class ProyectosDTO
    {
        public int Id_Proyectos { get; set; }
        public Nullable<int> Id_Clientes_Proyectos { get; set; }
        public Nullable<int> Id_Empresa { get; set; }
        public string Nombre_Proyectos { get; set; }
        public Nullable<System.DateTime> Fecha_Ini_Proyectos { get; set; }
        public Nullable<System.DateTime> Fecha_Fin_Proyectos { get; set; }
        public string Fecha_Ini_ProyectosSTR { get; set; }
        public string Fecha_Fin_ProyectosSTR { get; set; }
        public Nullable<decimal> Costo_Proyectos { get; set; }
        public Nullable<int> Id_Moneda_Proyectos { get; set; }
        public Nullable<int> Numero_Facturas_Proyectos { get; set; }
        public Nullable<int> Id_Tipo_Cambio_Proyectos { get; set; }
        public string Comentarios_Proyecto { get; set; }
        public Nullable<int> Id_IVA_Proyectos { get; set; }
        public Nullable<bool> Estado { get; set; }

        //public virtual C_IVA C_IVA { get; set; }
        //public virtual C_Moneda C_Moneda { get; set; }
        //public virtual C_Tipo_Cambio C_Tipo_Cambio { get; set; }
        //public virtual Clientes Clientes { get; set; }
    }
}
