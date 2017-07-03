using AutoMapper;
using DTO.General;
using DTO.Gestion;
using DTO.Paginador;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModeloDatos.Gestion
{
    public class AsignacionData
    {
        /// <summary>
        /// Guardar Asignacion
        /// </summary>
        /// <returns>Lista de tipo asignacion</returns>
        public MethodResponseDTO<int> GuardarAsignacion(AsignacionDTO asignacion)
        {
            using (var context = new DB_9F97CF_CatarsysSGCEntities())
            {
                try
                {
                    var response = new MethodResponseDTO<int>() { Code = 0, Result = 1 };


                    if (asignacion.Id_Asignacion == 0)
                    {
                        Asignacion asignacionDB = Mapper.Map<Asignacion>(asignacion);
                        context.Asignacion.Add(asignacionDB);
                    }
                    else
                    {
                        var asig = context.Asignacion.SingleOrDefault(x => x.Id_Asignacion == asignacion.Id_Asignacion);

                        asig.Id_Asignacion = asignacion.Id_Asignacion;
                        asig.Id_Empleado_Asignacion = asignacion.Id_Empleado_Asignacion;
                        asig.Id_Cliente_Asignacion = asignacion.Id_Cliente_Asignacion;
                        asig.Id_Tipo_Asignacion = asignacion.Id_Tipo_Asignacion;
                        asig.Id_Estatus_Asignacion = asignacion.Id_Estatus_Asignacion;
                        asig.Id_Corte_Asignacion = asignacion.Id_Corte_Asignacion;
                        asig.Id_Moneda_Asignacion = asignacion.Id_Moneda_Asignacion;
                        asig.Id_Periodo_Asignacion = asignacion.Id_Periodo_Asignacion;
                        asig.Id_Empresa = asignacion.Id_Empresa;
                        asig.Id_IVA_Asignacion = asignacion.Id_IVA_Asignacion;
                        asig.Fecha_Ini_Asignacion = asignacion.Fecha_Ini_Asignacion;
                        asig.Fecha_Fin_Asignacion = asignacion.Fecha_Fin_Asignacion;
                        asig.Costo_Pactado_Asignacion = asignacion.Costo_Pactado_Asignacion;
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

        public MethodResponseDTO<int> GuardarFacturasAsignacioón(Asignacion asignacion)
        {
            try
            {
                var response = new MethodResponseDTO<int>() { Code = 0, Result = 1 };
                var _FacturasData = new FacturasData();

                var BrFacturas = _FacturasData.BorrarFacturas(asignacion.Id_Asignacion, 2);

                if (BrFacturas.Code != 0)
                {
                    return new MethodResponseDTO<int> { Code = -100, Result = 0, Message = "Borrar Facturas: " + BrFacturas.Message };
                }


                var fechaInicio = BrFacturas.Result;
                var fechaLimite = asignacion.Fecha_Fin_Asignacion;
                var periodo = asignacion.Id_Periodo_Asignacion;
                var facturasLst = new List<FacturasDTO>();
                var NoInicialFactura = _FacturasData.ObtenerNumeroMaxFactura(asignacion.Id_Asignacion, 1).Result;
                DateTime fecha = fechaInicio;

                while (fechaLimite < fecha)
                {
                    var fact = new FacturasDTO();
                    fact.IdFactura = 0;
                    fact.IdCliente = asignacion.Id_Cliente_Asignacion.Value;
                    fact.IdEmpresa = asignacion.Id_Empresa.Value;
                    fact.IdAsignacion = asignacion.Id_Asignacion;
                    fact.C_Id_IVA = asignacion.Id_IVA_Asignacion;
                    fact.C_Id_Moneda = asignacion.Id_Moneda_Asignacion;
                    fact.Tipo_Factura = "AS";
                    fact.No_Factura = NoInicialFactura;
                    fact.Monto_Factura = asignacion.Costo_Pactado_Asignacion;
                    fact.Id_Estado_Factura = 1;

                    fact.Fecha_Inicio_Factura = fecha;
                    if (periodo == 1)
                    {

                        fact.Fecha_fin_Factura = fecha.AddDays(7); ;

                    }
                    else if (periodo == 2)
                    {

                        fact.Fecha_fin_Factura = fecha.AddMonths(1); ;
                    }
                    else
                    {
                        fact.Fecha_fin_Factura = fecha.AddYears(1); ;
                    }

                    fecha = fact.Fecha_fin_Factura.Value;

                    facturasLst.Add(fact);

                    NoInicialFactura++;
                }

                var gdFacturas = _FacturasData.GuardarFacturas(facturasLst);

                response.Code = gdFacturas.Code;
                response.Message = gdFacturas.Message;



                return response;
            }

            catch (Exception ex)
            {
                return new MethodResponseDTO<int> { Code = -100, Result = 0, Message = "Generar Facturas Asignación: " + ex.Message };
            }
        }

        /// <summary>
        /// Obtener Asignaciones
        /// Con idAsignacion = 0 regresara todas las asignaciones
        /// Con estatusAsignacion =  0 regresara las asignaciones de cualquier estado
        /// </summary>
        /// <param name="idAsignacion"></param>
        /// <param name="estatusAsignacion"></param>
        /// <returns></returns>
        public MethodResponseDTO<List<AsignacionDTO>> ObtenerAsignaciones(int idAsignacion, int estatusAsignacion)
        {
            using (var context = new DB_9F97CF_CatarsysSGCEntities())
            {
                try
                {
                    var response = new MethodResponseDTO<List<AsignacionDTO>>() { Code = 0 };


                    var asignaciones = context.Asignacion.Where(x => (x.Id_Asignacion == idAsignacion || idAsignacion == 0) && (x.Id_Estatus_Asignacion == estatusAsignacion || estatusAsignacion == 0)).ToList();
                    response.Result = Mapper.Map<List<AsignacionDTO>>(asignaciones);

                    return response;
                }
                catch (Exception ex)
                {
                    return new MethodResponseDTO<List<AsignacionDTO>> { Code = -100, Message = "ObtenerAsignaciones: " + ex.Message };
                }
            }
        }

        public MethodResponseDTO<PaginacionDTO<List<AsignacionPaginadorDTO>>> ObtenerAsignacionesPaginacion(int page, int size, int sort, string sortDireccion, string filter, int Id_Empresa)
        {
            using (var context = new DB_9F97CF_CatarsysSGCEntities())
            {
                try
                {
                    var response = new MethodResponseDTO<PaginacionDTO<List<AsignacionPaginadorDTO>>>() { Code = 0 };

                    var responsePag = new PaginacionDTO<List<AsignacionPaginadorDTO>>();


                    ObjectParameter totalrow = new ObjectParameter("totalrow", typeof(int));
                    var asignacion = context.sp_GetAsigPaginacion(page, size, sort, Id_Empresa, sortDireccion, filter, totalrow).ToList();


                    responsePag.data = Mapper.Map<List<AsignacionPaginadorDTO>>(asignacion);
                    responsePag.recordsTotal = Convert.ToInt32(totalrow.Value);
                    responsePag.draw = page;

                    response.Result = responsePag;
                    return response;
                }
                catch (Exception ex)
                {
                    return new MethodResponseDTO<PaginacionDTO<List<AsignacionPaginadorDTO>>> { Code = -100, Message = "ObtenerAsignaciones: " + ex.Message };
                }
            }
        }
    }
}
