//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ModeloDatos
{
    using System;
    using System.Collections.Generic;
    
    public partial class Asignacion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Asignacion()
        {
            this.Facturas = new HashSet<Facturas>();
        }
    
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
    
        public virtual Clientes Clientes { get; set; }
        public virtual C_Corte C_Corte { get; set; }
        public virtual Empresa Empresa { get; set; }
        public virtual C_Estatus C_Estatus { get; set; }
        public virtual C_IVA C_IVA { get; set; }
        public virtual C_Moneda C_Moneda { get; set; }
        public virtual C_Periodos C_Periodos { get; set; }
        public virtual C_Tipo_Asignacion C_Tipo_Asignacion { get; set; }
        public virtual Empleados Empleados { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Facturas> Facturas { get; set; }
    }
}
