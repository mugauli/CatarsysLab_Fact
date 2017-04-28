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
