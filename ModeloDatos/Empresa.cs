//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ModeloDatos
{
    using System;
    using System.Collections.Generic;
    
    public partial class Empresa
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Empresa()
        {
            this.Asignacion = new HashSet<Asignacion>();
            this.Clientes = new HashSet<Clientes>();
            this.Empleados = new HashSet<Empleados>();
            this.Facturas = new HashSet<Facturas>();
            this.Proyectos = new HashSet<Proyectos>();
        }
    
        public int Id_Empresa { get; set; }
        public string Nombre_Empresa { get; set; }
        public string Razon_Social_Empresa { get; set; }
        public string RFC_Empresa { get; set; }
        public string Calle_Empresa { get; set; }
        public string No_Ext_Empresa { get; set; }
        public string No_Int_Empresa { get; set; }
        public string Colonia_Empresa { get; set; }
        public Nullable<short> CP_Empresa { get; set; }
        public string Del_Mun_Empresa { get; set; }
        public string Estado_Dom_Empresa { get; set; }
        public string Email_Empresa { get; set; }
        public Nullable<System.DateTime> Fecha_Creacion_Empresa { get; set; }
        public Nullable<bool> Estado_Empresa { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Asignacion> Asignacion { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Clientes> Clientes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Empleados> Empleados { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Facturas> Facturas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Proyectos> Proyectos { get; set; }
    }
}
