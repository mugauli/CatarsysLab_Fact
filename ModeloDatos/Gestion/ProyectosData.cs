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
    public class ProyectosData
    {
        /// <summary>
        /// Guardar Proyecto
        /// </summary>
        /// <returns>Lista de tipo asignacion</returns>
        public MethodResponseDTO<int> GuardarProyecto(ProyectosDTO proyecto)
        {
            using (var context = new DB_9F97CF_CatarsysSGCEntities())
            {
                try
                {
                    var response = new MethodResponseDTO<int>() { Code = 0, Result = 1 };


                    if (proyecto.Id_Proyectos == 0)
                    {
                        var proyectoDB = Mapper.Map<Proyectos>(proyecto);
                        context.Proyectos.Add(proyectoDB);
                    }
                    else
                    {
                        var asig = context.Proyectos.SingleOrDefault(x => x.Id_Proyectos == proyecto.Id_Proyectos);

                        asig.Id_Proyectos = proyecto.Id_Proyectos;
                        asig.Id_Clientes_Proyectos = proyecto.Id_Clientes_Proyectos;
                        asig.Id_Empresa = proyecto.Id_Empresa;
                        asig.Nombre_Proyectos = proyecto.Nombre_Proyectos;
                        asig.Fecha_Ini_Proyectos = proyecto.Fecha_Ini_Proyectos;
                        asig.Fecha_Fin_Proyectos = proyecto.Fecha_Fin_Proyectos;
                        asig.Costo_Proyectos = proyecto.Costo_Proyectos;
                        asig.Id_Moneda_Proyectos = proyecto.Id_Moneda_Proyectos;
                        asig.Numero_Facturas_Proyectos = proyecto.Numero_Facturas_Proyectos;
                        asig.Id_Tipo_Cambio_Proyectos = proyecto.Id_Tipo_Cambio_Proyectos;                      
                        asig.Id_IVA_Proyectos = proyecto.Id_IVA_Proyectos;                        
                        asig.Estado = proyecto.Estado;
                    }
                    context.SaveChanges();

                    
                    var _FacturasData = new FacturasData();

                    var BrFacturas = _FacturasData.BorrarFacturas(proyecto.Id_Proyectos, 1);

                    if (BrFacturas.Code != 0)
                    {
                        return new MethodResponseDTO<int> { Code = -100, Result = 0, Message = "Borrar Facturas: " + BrFacturas.Message };
                    }

                    var Facturas = _FacturasData.GuardarFacturas(proyecto.facturas);

                    if (Facturas.Code != 0)
                    {
                        return new MethodResponseDTO<int> { Code = -100, Result = 0, Message = "Guardar Facturas: " + Facturas.Message };
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
                    return new MethodResponseDTO<int> { Code = -100, Result = 0, Message = "Guardar Proyecto: " + ex.Message };
                }
            }
        }

        /// <summary>
        /// Obtener Proyectos
        /// Con IdProyecto = 0 regresara todos los proyectos
        /// Con IdEmpresa = 0 regresara todos los proyectos de todas las empresas
        /// Con estado =  0 regresara los proyectos de cualquier estado
        /// </summary>
        /// <param name="idProyecto"></param>
        /// <param name="idEmpresa"></param>
        /// <param name="estado"></param>
        /// <returns>Lista de proyectos</returns>
        public MethodResponseDTO<List<ProyectosDTO>> ObtenerProyecto(int idProyecto,int idEmpresa, int estado)
        {
            using (var context = new DB_9F97CF_CatarsysSGCEntities())
            {
                try
                {
                    var response = new MethodResponseDTO<List<ProyectosDTO>>() { Code = 0 };


                    var asignaciones = context.Proyectos.Where(x => (x.Id_Proyectos == idProyecto || idProyecto == 0) 
                                                                     && (x.Estado == (estado == 1) || estado == 2)
                                                                     && (x.Id_Empresa == idEmpresa|| idEmpresa == 0)).ToList();
                    response.Result = Mapper.Map<List<ProyectosDTO>>(asignaciones);
                    
                    return response;
                }
                catch (Exception ex)
                {
                    return new MethodResponseDTO<List<ProyectosDTO>> { Code = -100, Message = "ObtenerProyecto: " + ex.Message };
                }
            }
        }
        /// <summary>
        /// Obtener proyectos de la base con paginacion
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <param name="sort"></param>
        /// <param name="sortDireccion"></param>
        /// <param name="filter"></param>
        /// <param name="filter2"></param>
        /// <param name="Id_Empresa"></param>
        /// <returns></returns>
        public MethodResponseDTO<PaginacionDTO<List<ProyectosPaginadorDTO>>> ObtenerProyectosPaginacion(int page, int size, int sort, string sortDireccion, string filter, int filter2, int Id_Empresa)
        {
            using (var context = new DB_9F97CF_CatarsysSGCEntities())
            {
                try
                {
                    var response = new MethodResponseDTO<PaginacionDTO<List<ProyectosPaginadorDTO>>>() { Code = 0 };

                    var responsePag = new PaginacionDTO<List<ProyectosPaginadorDTO>>();


                    ObjectParameter totalrow = new ObjectParameter("totalrow", typeof(int));
                    var asignacion = context.sp_GetProyectosPaginacion(page, size, sort, Id_Empresa, sortDireccion, filter,filter2, totalrow).ToList();


                    responsePag.data = Mapper.Map<List<ProyectosPaginadorDTO>>(asignacion);
                    responsePag.recordsTotal = Convert.ToInt32(totalrow.Value);
                    responsePag.draw = page;

                    response.Result = responsePag;
                    return response;
                }
                catch (Exception ex)
                {
                    return new MethodResponseDTO<PaginacionDTO<List<ProyectosPaginadorDTO>>> { Code = -100, Message = "ObtenerAsignaciones: " + ex.Message };
                }
            }
        }
    }
}
