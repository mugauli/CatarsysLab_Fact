﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class DB_9F97CF_CatarsysSGCEntities : DbContext
    {
        public DB_9F97CF_CatarsysSGCEntities()
            : base("name=DB_9F97CF_CatarsysSGCEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Asignacion> Asignacion { get; set; }
        public virtual DbSet<C_Corte> C_Corte { get; set; }
        public virtual DbSet<C_Estado_Factura> C_Estado_Factura { get; set; }
        public virtual DbSet<C_Estatus> C_Estatus { get; set; }
        public virtual DbSet<C_IVA> C_IVA { get; set; }
        public virtual DbSet<C_Metodo_Pago> C_Metodo_Pago { get; set; }
        public virtual DbSet<C_Moneda> C_Moneda { get; set; }
        public virtual DbSet<C_Periodos> C_Periodos { get; set; }
        public virtual DbSet<C_Tipo_Asignacion> C_Tipo_Asignacion { get; set; }
        public virtual DbSet<C_Tipo_Cambio> C_Tipo_Cambio { get; set; }
        public virtual DbSet<Clientes> Clientes { get; set; }
        public virtual DbSet<Contactos> Contactos { get; set; }
        public virtual DbSet<ctPermisos> ctPermisos { get; set; }
        public virtual DbSet<EmpleadoPermiso> EmpleadoPermiso { get; set; }
        public virtual DbSet<Empresa> Empresa { get; set; }
        public virtual DbSet<Facturas> Facturas { get; set; }
        public virtual DbSet<Proyectos> Proyectos { get; set; }
        public virtual DbSet<DocumentosFacturas> DocumentosFacturas { get; set; }
        public virtual DbSet<Empleados> Empleados { get; set; }
    
        public virtual ObjectResult<sp_GetAsigPaginacion_Result> sp_GetAsigPaginacion(Nullable<int> page, Nullable<int> size, Nullable<int> sort, Nullable<int> company, string sortDireccion, string filter, ObjectParameter totalrow)
        {
            var pageParameter = page.HasValue ?
                new ObjectParameter("page", page) :
                new ObjectParameter("page", typeof(int));
    
            var sizeParameter = size.HasValue ?
                new ObjectParameter("size", size) :
                new ObjectParameter("size", typeof(int));
    
            var sortParameter = sort.HasValue ?
                new ObjectParameter("sort", sort) :
                new ObjectParameter("sort", typeof(int));
    
            var companyParameter = company.HasValue ?
                new ObjectParameter("company", company) :
                new ObjectParameter("company", typeof(int));
    
            var sortDireccionParameter = sortDireccion != null ?
                new ObjectParameter("sortDireccion", sortDireccion) :
                new ObjectParameter("sortDireccion", typeof(string));
    
            var filterParameter = filter != null ?
                new ObjectParameter("filter", filter) :
                new ObjectParameter("filter", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_GetAsigPaginacion_Result>("sp_GetAsigPaginacion", pageParameter, sizeParameter, sortParameter, companyParameter, sortDireccionParameter, filterParameter, totalrow);
        }
    
        public virtual ObjectResult<sp_GetProyectosPaginacion_Result> sp_GetProyectosPaginacion(Nullable<int> page, Nullable<int> size, Nullable<int> sort, Nullable<int> company, string sortDireccion, string filter, Nullable<int> filter2, ObjectParameter totalrow)
        {
            var pageParameter = page.HasValue ?
                new ObjectParameter("page", page) :
                new ObjectParameter("page", typeof(int));
    
            var sizeParameter = size.HasValue ?
                new ObjectParameter("size", size) :
                new ObjectParameter("size", typeof(int));
    
            var sortParameter = sort.HasValue ?
                new ObjectParameter("sort", sort) :
                new ObjectParameter("sort", typeof(int));
    
            var companyParameter = company.HasValue ?
                new ObjectParameter("company", company) :
                new ObjectParameter("company", typeof(int));
    
            var sortDireccionParameter = sortDireccion != null ?
                new ObjectParameter("sortDireccion", sortDireccion) :
                new ObjectParameter("sortDireccion", typeof(string));
    
            var filterParameter = filter != null ?
                new ObjectParameter("filter", filter) :
                new ObjectParameter("filter", typeof(string));
    
            var filter2Parameter = filter2.HasValue ?
                new ObjectParameter("filter2", filter2) :
                new ObjectParameter("filter2", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_GetProyectosPaginacion_Result>("sp_GetProyectosPaginacion", pageParameter, sizeParameter, sortParameter, companyParameter, sortDireccionParameter, filterParameter, filter2Parameter, totalrow);
        }
    
        public virtual ObjectResult<sp_GetEmpleadosPaginacion_Result> sp_GetEmpleadosPaginacion(Nullable<int> page, Nullable<int> size, Nullable<int> sort, Nullable<int> company, string sortDireccion, string filter, ObjectParameter totalrow)
        {
            var pageParameter = page.HasValue ?
                new ObjectParameter("page", page) :
                new ObjectParameter("page", typeof(int));
    
            var sizeParameter = size.HasValue ?
                new ObjectParameter("size", size) :
                new ObjectParameter("size", typeof(int));
    
            var sortParameter = sort.HasValue ?
                new ObjectParameter("sort", sort) :
                new ObjectParameter("sort", typeof(int));
    
            var companyParameter = company.HasValue ?
                new ObjectParameter("company", company) :
                new ObjectParameter("company", typeof(int));
    
            var sortDireccionParameter = sortDireccion != null ?
                new ObjectParameter("sortDireccion", sortDireccion) :
                new ObjectParameter("sortDireccion", typeof(string));
    
            var filterParameter = filter != null ?
                new ObjectParameter("filter", filter) :
                new ObjectParameter("filter", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_GetEmpleadosPaginacion_Result>("sp_GetEmpleadosPaginacion", pageParameter, sizeParameter, sortParameter, companyParameter, sortDireccionParameter, filterParameter, totalrow);
        }
    
        public virtual ObjectResult<sp_GetClientesPaginacion_Result> sp_GetClientesPaginacion(Nullable<int> page, Nullable<int> size, Nullable<int> sort, Nullable<int> company, string sortDireccion, string filter, ObjectParameter totalrow)
        {
            var pageParameter = page.HasValue ?
                new ObjectParameter("page", page) :
                new ObjectParameter("page", typeof(int));
    
            var sizeParameter = size.HasValue ?
                new ObjectParameter("size", size) :
                new ObjectParameter("size", typeof(int));
    
            var sortParameter = sort.HasValue ?
                new ObjectParameter("sort", sort) :
                new ObjectParameter("sort", typeof(int));
    
            var companyParameter = company.HasValue ?
                new ObjectParameter("company", company) :
                new ObjectParameter("company", typeof(int));
    
            var sortDireccionParameter = sortDireccion != null ?
                new ObjectParameter("sortDireccion", sortDireccion) :
                new ObjectParameter("sortDireccion", typeof(string));
    
            var filterParameter = filter != null ?
                new ObjectParameter("filter", filter) :
                new ObjectParameter("filter", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_GetClientesPaginacion_Result>("sp_GetClientesPaginacion", pageParameter, sizeParameter, sortParameter, companyParameter, sortDireccionParameter, filterParameter, totalrow);
        }
    
        public virtual ObjectResult<sp_GetEmpresaPaginacion_Result> sp_GetEmpresaPaginacion(Nullable<int> page, Nullable<int> size, Nullable<int> sort, Nullable<int> company, string sortDireccion, string filter, ObjectParameter totalrow)
        {
            var pageParameter = page.HasValue ?
                new ObjectParameter("page", page) :
                new ObjectParameter("page", typeof(int));
    
            var sizeParameter = size.HasValue ?
                new ObjectParameter("size", size) :
                new ObjectParameter("size", typeof(int));
    
            var sortParameter = sort.HasValue ?
                new ObjectParameter("sort", sort) :
                new ObjectParameter("sort", typeof(int));
    
            var companyParameter = company.HasValue ?
                new ObjectParameter("company", company) :
                new ObjectParameter("company", typeof(int));
    
            var sortDireccionParameter = sortDireccion != null ?
                new ObjectParameter("sortDireccion", sortDireccion) :
                new ObjectParameter("sortDireccion", typeof(string));
    
            var filterParameter = filter != null ?
                new ObjectParameter("filter", filter) :
                new ObjectParameter("filter", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_GetEmpresaPaginacion_Result>("sp_GetEmpresaPaginacion", pageParameter, sizeParameter, sortParameter, companyParameter, sortDireccionParameter, filterParameter, totalrow);
        }
    
        public virtual ObjectResult<sp_GetFacturasPaginacion_Result> sp_GetFacturasPaginacion(Nullable<int> page, Nullable<int> size, Nullable<int> sort, Nullable<int> company, string sortDireccion, string filter, Nullable<int> filterCliente, Nullable<int> filterEstado, Nullable<System.DateTime> filterPeriodo, ObjectParameter totalrow)
        {
            var pageParameter = page.HasValue ?
                new ObjectParameter("page", page) :
                new ObjectParameter("page", typeof(int));
    
            var sizeParameter = size.HasValue ?
                new ObjectParameter("size", size) :
                new ObjectParameter("size", typeof(int));
    
            var sortParameter = sort.HasValue ?
                new ObjectParameter("sort", sort) :
                new ObjectParameter("sort", typeof(int));
    
            var companyParameter = company.HasValue ?
                new ObjectParameter("company", company) :
                new ObjectParameter("company", typeof(int));
    
            var sortDireccionParameter = sortDireccion != null ?
                new ObjectParameter("sortDireccion", sortDireccion) :
                new ObjectParameter("sortDireccion", typeof(string));
    
            var filterParameter = filter != null ?
                new ObjectParameter("filter", filter) :
                new ObjectParameter("filter", typeof(string));
    
            var filterClienteParameter = filterCliente.HasValue ?
                new ObjectParameter("filterCliente", filterCliente) :
                new ObjectParameter("filterCliente", typeof(int));
    
            var filterEstadoParameter = filterEstado.HasValue ?
                new ObjectParameter("filterEstado", filterEstado) :
                new ObjectParameter("filterEstado", typeof(int));
    
            var filterPeriodoParameter = filterPeriodo.HasValue ?
                new ObjectParameter("filterPeriodo", filterPeriodo) :
                new ObjectParameter("filterPeriodo", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_GetFacturasPaginacion_Result>("sp_GetFacturasPaginacion", pageParameter, sizeParameter, sortParameter, companyParameter, sortDireccionParameter, filterParameter, filterClienteParameter, filterEstadoParameter, filterPeriodoParameter, totalrow);
        }
    
        public virtual ObjectResult<sp_GetFacturasPendientesPaginacion_Result> sp_GetFacturasPendientesPaginacion(Nullable<int> page, Nullable<int> size, Nullable<int> sort, Nullable<int> company, string sortDireccion, string filter, Nullable<int> filterCliente, Nullable<int> filterEstado, Nullable<System.DateTime> filterPeriodo, ObjectParameter totalrow)
        {
            var pageParameter = page.HasValue ?
                new ObjectParameter("page", page) :
                new ObjectParameter("page", typeof(int));
    
            var sizeParameter = size.HasValue ?
                new ObjectParameter("size", size) :
                new ObjectParameter("size", typeof(int));
    
            var sortParameter = sort.HasValue ?
                new ObjectParameter("sort", sort) :
                new ObjectParameter("sort", typeof(int));
    
            var companyParameter = company.HasValue ?
                new ObjectParameter("company", company) :
                new ObjectParameter("company", typeof(int));
    
            var sortDireccionParameter = sortDireccion != null ?
                new ObjectParameter("sortDireccion", sortDireccion) :
                new ObjectParameter("sortDireccion", typeof(string));
    
            var filterParameter = filter != null ?
                new ObjectParameter("filter", filter) :
                new ObjectParameter("filter", typeof(string));
    
            var filterClienteParameter = filterCliente.HasValue ?
                new ObjectParameter("filterCliente", filterCliente) :
                new ObjectParameter("filterCliente", typeof(int));
    
            var filterEstadoParameter = filterEstado.HasValue ?
                new ObjectParameter("filterEstado", filterEstado) :
                new ObjectParameter("filterEstado", typeof(int));
    
            var filterPeriodoParameter = filterPeriodo.HasValue ?
                new ObjectParameter("filterPeriodo", filterPeriodo) :
                new ObjectParameter("filterPeriodo", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_GetFacturasPendientesPaginacion_Result>("sp_GetFacturasPendientesPaginacion", pageParameter, sizeParameter, sortParameter, companyParameter, sortDireccionParameter, filterParameter, filterClienteParameter, filterEstadoParameter, filterPeriodoParameter, totalrow);
        }
    
        public virtual ObjectResult<sp_GetProyectosActualesPaginacion_Result> sp_GetProyectosActualesPaginacion(Nullable<int> page, Nullable<int> size, Nullable<int> sort, Nullable<int> company, string sortDireccion, string filter, Nullable<int> filterCliente, ObjectParameter totalrow)
        {
            var pageParameter = page.HasValue ?
                new ObjectParameter("page", page) :
                new ObjectParameter("page", typeof(int));
    
            var sizeParameter = size.HasValue ?
                new ObjectParameter("size", size) :
                new ObjectParameter("size", typeof(int));
    
            var sortParameter = sort.HasValue ?
                new ObjectParameter("sort", sort) :
                new ObjectParameter("sort", typeof(int));
    
            var companyParameter = company.HasValue ?
                new ObjectParameter("company", company) :
                new ObjectParameter("company", typeof(int));
    
            var sortDireccionParameter = sortDireccion != null ?
                new ObjectParameter("sortDireccion", sortDireccion) :
                new ObjectParameter("sortDireccion", typeof(string));
    
            var filterParameter = filter != null ?
                new ObjectParameter("filter", filter) :
                new ObjectParameter("filter", typeof(string));
    
            var filterClienteParameter = filterCliente.HasValue ?
                new ObjectParameter("filterCliente", filterCliente) :
                new ObjectParameter("filterCliente", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_GetProyectosActualesPaginacion_Result>("sp_GetProyectosActualesPaginacion", pageParameter, sizeParameter, sortParameter, companyParameter, sortDireccionParameter, filterParameter, filterClienteParameter, totalrow);
        }
    
        public virtual ObjectResult<sp_GetAsignacionesActualesPaginacion_Result> sp_GetAsignacionesActualesPaginacion(Nullable<int> page, Nullable<int> size, Nullable<int> sort, Nullable<int> company, string sortDireccion, string filter, ObjectParameter totalrow)
        {
            var pageParameter = page.HasValue ?
                new ObjectParameter("page", page) :
                new ObjectParameter("page", typeof(int));
    
            var sizeParameter = size.HasValue ?
                new ObjectParameter("size", size) :
                new ObjectParameter("size", typeof(int));
    
            var sortParameter = sort.HasValue ?
                new ObjectParameter("sort", sort) :
                new ObjectParameter("sort", typeof(int));
    
            var companyParameter = company.HasValue ?
                new ObjectParameter("company", company) :
                new ObjectParameter("company", typeof(int));
    
            var sortDireccionParameter = sortDireccion != null ?
                new ObjectParameter("sortDireccion", sortDireccion) :
                new ObjectParameter("sortDireccion", typeof(string));
    
            var filterParameter = filter != null ?
                new ObjectParameter("filter", filter) :
                new ObjectParameter("filter", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_GetAsignacionesActualesPaginacion_Result>("sp_GetAsignacionesActualesPaginacion", pageParameter, sizeParameter, sortParameter, companyParameter, sortDireccionParameter, filterParameter, totalrow);
        }
    
        public virtual ObjectResult<sp_GetCarteraVencidaPaginacion_Result> sp_GetCarteraVencidaPaginacion(Nullable<int> page, Nullable<int> size, Nullable<int> sort, Nullable<int> company, string sortDireccion, string filter, Nullable<int> filterEstado, Nullable<System.DateTime> filterPeriodo, ObjectParameter totalrow)
        {
            var pageParameter = page.HasValue ?
                new ObjectParameter("page", page) :
                new ObjectParameter("page", typeof(int));
    
            var sizeParameter = size.HasValue ?
                new ObjectParameter("size", size) :
                new ObjectParameter("size", typeof(int));
    
            var sortParameter = sort.HasValue ?
                new ObjectParameter("sort", sort) :
                new ObjectParameter("sort", typeof(int));
    
            var companyParameter = company.HasValue ?
                new ObjectParameter("company", company) :
                new ObjectParameter("company", typeof(int));
    
            var sortDireccionParameter = sortDireccion != null ?
                new ObjectParameter("sortDireccion", sortDireccion) :
                new ObjectParameter("sortDireccion", typeof(string));
    
            var filterParameter = filter != null ?
                new ObjectParameter("filter", filter) :
                new ObjectParameter("filter", typeof(string));
    
            var filterEstadoParameter = filterEstado.HasValue ?
                new ObjectParameter("filterEstado", filterEstado) :
                new ObjectParameter("filterEstado", typeof(int));
    
            var filterPeriodoParameter = filterPeriodo.HasValue ?
                new ObjectParameter("filterPeriodo", filterPeriodo) :
                new ObjectParameter("filterPeriodo", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_GetCarteraVencidaPaginacion_Result>("sp_GetCarteraVencidaPaginacion", pageParameter, sizeParameter, sortParameter, companyParameter, sortDireccionParameter, filterParameter, filterEstadoParameter, filterPeriodoParameter, totalrow);
        }
    }
}
