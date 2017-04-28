using DTO.Catalogos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Gestion
{
    public class AsignacionDTO
    {
        public int Id_Asignacion { get; set; }
        public Nullable<int> Id_Empleado_Asignacion { get; set; }
        public Nullable<int> Id_Cliente_Asignacion { get; set; }
        public Nullable<int> Id_Tipo_Asignacion { get; set; }
        public Nullable<int> Id_Estatus_Asignacion { get; set; }
        public Nullable<int> Id_Corte_Asignacion { get; set; }
        public Nullable<int> Id_Moneda_Asignacion { get; set; }
        public Nullable<int> Id_Periodo_Asignacion { get; set; }
        public Nullable<int> Id_Empresa { get; set; }
        public Nullable<int> Id_IVA_Asignacion { get; set; }
        public Nullable<System.DateTime> Fecha_Ini_Asignacion { get; set; }
        public Nullable<System.DateTime> Fecha_Fin_Asignacion { get; set; }
        public Nullable<decimal> Costo_Pactado_Asignacion { get; set; }
        public string Duracion { get; set; }
        public string Fecha_Ini_AsignacionSTR { get; set; }
        public string Fecha_Fin_AsignacionSTR { get; set; }


        public virtual ClientesDTO Clientes { get; set; }
        //public virtual C_Corte C_Corte { get; set; }
        public virtual C_EstatusDTO C_Estatus { get; set; }
        //public virtual C_IVA C_IVA { get; set; }
        //public virtual C_Moneda C_Moneda { get; set; }
        //public virtual C_Periodos C_Periodos { get; set; }
        //public virtual C_Tipo_Asignacion C_Tipo_Asignacion { get; set; }
        public virtual EmpleadosDTO Usuarios { get; set; }
    }
}
