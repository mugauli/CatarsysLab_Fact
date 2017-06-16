using AutoMapper;
using DTO.General;
using DTO.Gestion;
using DTO.Paginador;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModeloDatos.Gestion
{
    public class FacturasData
    {
        /// <summary>
        /// Guardar Factura
        /// </summary>
        /// <returns>Lista de tipo asignacion</returns>
        public MethodResponseDTO<int> GuardarFactura(FacturasDTO factura)
        {
            using (var context = new DB_9F97CF_CatarsysSGCEntities())
            {
                try
                {
                    var response = new MethodResponseDTO<int>() { Code = 0, Result = 1 };


                    if (factura.IdFactura == 0)
                    {
                        Facturas facturasDB = Mapper.Map<Facturas>(factura);
                        context.Facturas.Add(facturasDB);
                    }
                    else
                    {
                        var fact = context.Facturas.SingleOrDefault(x => x.IdFactura == factura.IdFactura);

                        fact.IdFactura = factura.IdFactura;
                        fact.IdCliente = factura.IdCliente;
                        fact.IdEmpresa = factura.IdEmpresa;
                        fact.IdAsignacion = factura.IdAsignacion;
                        fact.IdProyecto = factura.IdProyecto;
                        fact.C_Id_IVA = factura.C_Id_IVA;
                        fact.C_Id_Moneda = factura.C_Id_Moneda;
                        fact.C_Id_Tipo_Cambio = factura.C_Id_Tipo_Cambio;
                        fact.C_Id_Metodo_Pago = factura.C_Id_Metodo_Pago;
                        fact.Tipo_Factura = factura.Tipo_Factura;
                        fact.Fecha_Inicio_Factura = factura.Fecha_Inicio_Factura;
                        fact.Fecha_fin_Factura = factura.Fecha_fin_Factura;
                        fact.No_Factura = factura.No_Factura;
                        fact.Descuento_Factura = factura.Descuento_Factura;
                        fact.Monto_Factura = factura.Monto_Factura;
                        fact.Ultimos_4_digitos_Factura = factura.Ultimos_4_digitos_Factura;
                    }
                    context.SaveChanges();

                    return response;
                }
                catch (DbEntityValidationException e)
                {
                    string Result = string.Empty;
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Result += string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Result += string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    Result += ("Code: -100, Mensaje: " + e.Message + ", InnerException: " + (e.InnerException != null ? e.InnerException.Message : ""));
                    return new MethodResponseDTO<int> { Code = -100, Result = 0, Message = e.Message };
                }
                catch (Exception ex)
                {
                    return new MethodResponseDTO<int> { Code = -100, Result = 0, Message = "GuardarAsignacion: " + ex.Message };
                }
            }
        }

        /// <summary>
        /// Guardar Facturas
        /// </summary>
        /// <returns>Lista de tipo asignacion</returns>
        public MethodResponseDTO<int> GuardarFacturas(List<FacturasDTO> ltsFacturas)
        {
            using (var context = new DB_9F97CF_CatarsysSGCEntities())
            {
                try
                {
                    var response = new MethodResponseDTO<int>() { Code = 0, Result = 1 };

                    foreach (var factura in ltsFacturas)
                    {
                        if(factura.Id_Estado_Factura == 1)
                        if (factura.IdFactura == 0)
                        {
                            Facturas facturasDB = Mapper.Map<Facturas>(factura);
                            context.Facturas.Add(facturasDB);
                        }
                        else
                        {
                            var fact = context.Facturas.SingleOrDefault(x => x.IdFactura == factura.IdFactura);

                            fact.IdFactura = factura.IdFactura;
                            fact.IdCliente = factura.IdCliente;
                            fact.IdEmpresa = factura.IdEmpresa;
                            fact.IdAsignacion = factura.IdAsignacion;
                            fact.IdProyecto = factura.IdProyecto;
                            fact.C_Id_IVA = factura.C_Id_IVA;
                            fact.C_Id_Moneda = factura.C_Id_Moneda;
                            fact.C_Id_Tipo_Cambio = factura.C_Id_Tipo_Cambio;
                            fact.C_Id_Metodo_Pago = factura.C_Id_Metodo_Pago;
                            fact.Tipo_Factura = factura.Tipo_Factura;
                            fact.Fecha_Inicio_Factura = factura.Fecha_Inicio_Factura;
                            fact.Fecha_fin_Factura = factura.Fecha_fin_Factura;
                            fact.No_Factura = factura.No_Factura;
                            fact.Descuento_Factura = factura.Descuento_Factura;
                            fact.Monto_Factura = factura.Monto_Factura;
                            fact.Ultimos_4_digitos_Factura = factura.Ultimos_4_digitos_Factura;
                        }
                        context.SaveChanges();
                    }

                    return response;
                }
                catch (DbEntityValidationException e)
                {
                    string Result = string.Empty;
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Result += string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Result += string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    Result += ("Code: -100, Mensaje: " + e.Message + ", InnerException: " + (e.InnerException != null ? e.InnerException.Message : ""));
                    return new MethodResponseDTO<int> { Code = -100, Result = 0, Message = e.Message };
                }
                catch (Exception ex)
                {
                    return new MethodResponseDTO<int> { Code = -100, Result = 0, Message = "GuardarAsignacion: " + ex.Message };
                }
            }
        }

        /// <summary>
        /// Borrar Facturas
        /// </summary>
        /// <returns>Lista de tipo asignacion</returns>
        public MethodResponseDTO<int> BorrarFacturas(int Id, int tipo)
        {
            using (var context = new DB_9F97CF_CatarsysSGCEntities())
            {
                try
                {
                    var response = new MethodResponseDTO<int>() { Code = 0, Result = 1 };

                    List<Facturas> ltsFacturas;
                    if (tipo == 1)
                        ltsFacturas = context.Facturas.Where(x => x.IdProyecto == Id && x.Id_Estado_Factura == 1).ToList();
                    else
                        ltsFacturas = context.Facturas.Where(x => x.IdAsignacion == Id && x.Id_Estado_Factura == 1).ToList();

                    foreach (var factura in ltsFacturas)
                    {

                        context.Facturas.Remove(factura);
                        context.SaveChanges();
                    }

                    return response;
                }
                catch (DbEntityValidationException e)
                {
                    string Result = string.Empty;
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Result += string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Result += string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    Result += ("Code: -100, Mensaje: " + e.Message + ", InnerException: " + (e.InnerException != null ? e.InnerException.Message : ""));
                    return new MethodResponseDTO<int> { Code = -100, Result = 0, Message = e.Message };
                }
                catch (Exception ex)
                {
                    return new MethodResponseDTO<int> { Code = -100, Result = 0, Message = "GuardarAsignacion: " + ex.Message };
                }
            }
        }


        /// <summary>
        /// Obtener Asignaciones
        /// Con idAsignacion = 0 regresara todas las asignaciones
        /// Con estatusAsignacion =  0 regresara las asignaciones de cualquier estado
        /// </summary>
        /// <param name="idFactura"></param>
        /// <param name="estatusAsignacion"></param>
        /// <returns></returns>
        public MethodResponseDTO<List<FacturasDTO>> ObtenerFacturas(int idFactura)
        {
            using (var context = new DB_9F97CF_CatarsysSGCEntities())
            {
                try
                {
                    var response = new MethodResponseDTO<List<FacturasDTO>>() { Code = 0 };


                    var asignaciones = context.Facturas.Where(x => (x.IdFactura == idFactura || idFactura == 0)).ToList();
                    response.Result = Mapper.Map<List<FacturasDTO>>(asignaciones);

                    return response;
                }
                catch (Exception ex)
                {
                    return new MethodResponseDTO<List<FacturasDTO>> { Code = -100, Message = "ObtenerAsignaciones: " + ex.Message };
                }
            }
        }

        public MethodResponseDTO<PaginacionDTO<List<FacturasPaginadorDTO>>> ObtenerFacturasPaginacion(int page, int size, int sort, string sortDireccion, string filter,int filterCliente,int filterEstado,DateTime filterPeriodo, int Id_Empresa)
        {
            using (var context = new DB_9F97CF_CatarsysSGCEntities())
            {
                try
                {
                    var response = new MethodResponseDTO<PaginacionDTO<List<FacturasPaginadorDTO>>>() { Code = 0 };

                    var responsePag = new PaginacionDTO<List<FacturasPaginadorDTO>>();


                    ObjectParameter totalrow = new ObjectParameter("totalrow", typeof(int));
                    var asignacion = context.sp_GetFacturasPaginacion(page, size, sort, Id_Empresa, sortDireccion, filter,filterCliente,filterEstado,filterPeriodo,totalrow).ToList();


                    responsePag.data = Mapper.Map<List<FacturasPaginadorDTO>>(asignacion);
                    responsePag.recordsTotal = Convert.ToInt32(totalrow.Value);
                    responsePag.draw = page;

                    response.Result = responsePag;
                    return response;
                }
                catch (Exception ex)
                {
                    return new MethodResponseDTO<PaginacionDTO<List<FacturasPaginadorDTO>>> { Code = -100, Message = "Obtner Facturas Paginador: " + ex.Message };
                }
            }
        }

        public MethodResponseDTO<PaginacionDTO<List<FacturasPaginadorDTO>>> ObtenerPendientesPorFacturarPaginacion(int page, int size, int sort, string sortDireccion, string filter, int filterCliente, int filterEstado, DateTime filterPeriodo, int Id_Empresa)
        {
            using (var context = new DB_9F97CF_CatarsysSGCEntities())
            {
                try
                {
                    var response = new MethodResponseDTO<PaginacionDTO<List<FacturasPaginadorDTO>>>() { Code = 0 };

                    var responsePag = new PaginacionDTO<List<FacturasPaginadorDTO>>();


                    ObjectParameter totalrow = new ObjectParameter("totalrow", typeof(int));
                    //var asignacion = context.sp_GetFacturasPendientesPaginacion(page, size, sort, Id_Empresa, sortDireccion, filter, filterCliente, filterEstado, filterPeriodo, totalrow).ToList();
                    var asignacion = context.sp_GetFacturasPendientesPaginacion(0, 10, 1, 1, "asc", "", 1, 1, filterPeriodo, totalrow).ToList();


                    responsePag.data = Mapper.Map<List<FacturasPaginadorDTO>>(asignacion);
                    responsePag.recordsTotal = Convert.ToInt32(totalrow.Value);
                    responsePag.draw = page;

                    response.Result = responsePag;
                    return response;
                }
                catch (Exception ex)
                {
                    return new MethodResponseDTO<PaginacionDTO<List<FacturasPaginadorDTO>>> { Code = -100, Message = "Obtner Facturas Paginador: " + ex.Message };
                }
            }
        }

        public MethodResponseDTO<PaginacionDTO<List<ProximosIngresosDTO>>> ObtenerProximosIngresosPaginacion(int page, int size, int sort, string sortDireccion, string filter, int filterCliente, int filterEstado, DateTime filterPeriodo, int Id_Empresa)
        {
            using (var context = new DB_9F97CF_CatarsysSGCEntities())
            {
                try
                {
                    var response = new MethodResponseDTO<PaginacionDTO<List<ProximosIngresosDTO>>>() { Code = 0 };

                    var responsePag = new PaginacionDTO<List<ProximosIngresosDTO>>();


                    ObjectParameter totalrow = new ObjectParameter("totalrow", typeof(int));
                    var asignacion = context.sp_GetFacturasPendientesPaginacion(page, size, sort, Id_Empresa, sortDireccion, filter, filterCliente, filterEstado, filterPeriodo, totalrow).ToList();
                    //var asignacion = context.sp_GetFacturasPendientesPaginacion(0, 10, 1, 1, "asc", "", 1, 1, filterPeriodo, totalrow).ToList();


                    responsePag.data = Mapper.Map<List<ProximosIngresosDTO>>(asignacion);
                    responsePag.recordsTotal = Convert.ToInt32(totalrow.Value);
                    responsePag.draw = page;

                    response.Result = responsePag;
                    return response;
                }
                catch (Exception ex)
                {
                    return new MethodResponseDTO<PaginacionDTO<List<ProximosIngresosDTO>>> { Code = -100, Message = "Obtner Facturas Paginador: " + ex.Message };
                }
            }
        }

        public MethodResponseDTO<PaginacionDTO<List<ServiciosActualesPaginadorDTO>>> ObtenerServiciosActualesPaginacion_Proyecto(int page, int size, int sort, string sortDireccion, string filter, int filterCliente, int filterEstado, DateTime filterPeriodo, int Id_Empresa)
        {
            using (var context = new DB_9F97CF_CatarsysSGCEntities())
            {
                try
                {
                    var response = new MethodResponseDTO<PaginacionDTO<List<ServiciosActualesPaginadorDTO>>>() { Code = 0 };

                    var responsePag = new PaginacionDTO<List<ServiciosActualesPaginadorDTO>>();


                    ObjectParameter totalrow = new ObjectParameter("totalrow", typeof(int));
                    var asignacion = context.sp_GetFacturasPendientesPaginacion(page, size, sort, Id_Empresa, sortDireccion, filter, filterCliente, filterEstado, filterPeriodo, totalrow).ToList();
                    //var asignacion = context.sp_GetProyectosActualesPaginacion(0, 10, 1, 1, "asc", "", 1, totalrow).ToList();


                    responsePag.data = Mapper.Map<List<ServiciosActualesPaginadorDTO>>(asignacion);
                    responsePag.recordsTotal = Convert.ToInt32(totalrow.Value);
                    responsePag.draw = page;

                    response.Result = responsePag;
                    return response;
                }
                catch (Exception ex)
                {
                    return new MethodResponseDTO<PaginacionDTO<List<ServiciosActualesPaginadorDTO>>> { Code = -100, Message = "Obtner Facturas Paginador: " + ex.Message };
                }
            }
        }

        public MethodResponseDTO<PaginacionDTO<List<ServiciosActualesPaginadorDTO>>> ObtenerServiciosActualesPaginacion_Asignacion(int page, int size, int sort, string sortDireccion, string filter, int filterCliente, int filterEstado, DateTime filterPeriodo, int Id_Empresa)
        {
            using (var context = new DB_9F97CF_CatarsysSGCEntities())
            {
                try
                {
                    var response = new MethodResponseDTO<PaginacionDTO<List<ServiciosActualesPaginadorDTO>>>() { Code = 0 };

                    var responsePag = new PaginacionDTO<List<ServiciosActualesPaginadorDTO>>();


                    ObjectParameter totalrow = new ObjectParameter("totalrow", typeof(int));
                    var asignacion = context.sp_GetAsignacionesActualesPaginacion(page, size, sort, Id_Empresa, sortDireccion, filter, totalrow).ToList();
                    //var asignacion = context.sp_GetAsignacionesActualesPaginacion(0, 10, 1, 1, "asc", "", totalrow).ToList();


                    responsePag.data = Mapper.Map<List<ServiciosActualesPaginadorDTO>>(asignacion);
                    responsePag.recordsTotal = Convert.ToInt32(totalrow.Value);
                    responsePag.draw = page;

                    response.Result = responsePag;
                    return response;
                }
                catch (Exception ex)
                {
                    return new MethodResponseDTO<PaginacionDTO<List<ServiciosActualesPaginadorDTO>>> { Code = -100, Message = "Obtner Facturas Paginador: " + ex.Message };
                }
            }
        }

        public MethodResponseDTO<PaginacionDTO<List<CarteraVencidaPaginadorDTO>>> ObtenerCarteraVencidaPaginacion(int page, int size, int sort, string sortDireccion, string filter, int filterCliente, int filterEstado, DateTime filterPeriodo, int Id_Empresa)
        {
            using (var context = new DB_9F97CF_CatarsysSGCEntities())
            {
                try
                {
                    var response = new MethodResponseDTO<PaginacionDTO<List<CarteraVencidaPaginadorDTO>>>() { Code = 0 };

                    var responsePag = new PaginacionDTO<List<CarteraVencidaPaginadorDTO>>();


                    ObjectParameter totalrow = new ObjectParameter("totalrow", typeof(int));
                    var asignacion = context.sp_GetCarteraVencidaPaginacion(page, size, sort, Id_Empresa, sortDireccion, filter, filterEstado, filterPeriodo, totalrow).ToList();
                    //var asignacion = context.sp_GetCarteraVencidaPaginacion(0, 10, 1, 1, "asc", "",1, filterPeriodo, totalrow).ToList();

                    responsePag.data = Mapper.Map<List<CarteraVencidaPaginadorDTO>>(asignacion);
                    responsePag.recordsTotal = Convert.ToInt32(totalrow.Value);
                    responsePag.draw = page;

                    response.Result = responsePag;
                    return response;
                }
                catch (Exception ex)
                {
                    return new MethodResponseDTO<PaginacionDTO<List<CarteraVencidaPaginadorDTO>>> { Code = -100, Message = "Obtner Facturas Paginador: " + ex.Message };
                }
            }
        }
    }
}
