using AutoMapper;
using DTO.Catalogos;
using DTO.General;
using DTO.Gestion;
using DTO.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModeloDatos.Catalogos
{
    public class CatalogosData
    {
        /// <summary>
        /// Obtener Tipo Asignación
        /// </summary>
        /// <returns>Lista de tipo asignacion</returns>
        public MethodResponseDTO<List<C_Tipo_AsignacionDTO>> ObtenerTipoAsignacion()
        {
            using (var context = new DB_9F97CF_CatarsysSGCEntities())
            {
                try
                {
                    var response = new MethodResponseDTO<List<C_Tipo_AsignacionDTO>>() { Code = 0 };


                    var tipoAsignacion = context.C_Tipo_Asignacion.ToList();

                    response.Result = Mapper.Map<List<C_Tipo_AsignacionDTO>>(tipoAsignacion);

                    return response;
                }
                catch (Exception ex)
                {
                    return new MethodResponseDTO<List<C_Tipo_AsignacionDTO>> { Code = -100, Message = "ObtenerTipoAsignacion: " + ex.Message };
                }
            }
        }

        /// <summary>
        /// Obtener Corte Facturación
        /// </summary>
        /// <returns>Lista de Corte Facturación</returns>
        public MethodResponseDTO<List<C_CorteDTO>> ObtenerCorteFactura()
        {
            using (var context = new DB_9F97CF_CatarsysSGCEntities())
            {
                try
                {
                    var response = new MethodResponseDTO<List<C_CorteDTO>>() { Code = 0 };


                    var obj = context.C_Corte.ToList();

                    response.Result = Mapper.Map<List<C_CorteDTO>>(obj);

                    return response;
                }
                catch (Exception ex)
                {
                    return new MethodResponseDTO<List<C_CorteDTO>> { Code = -100, Message = "ObtenerCorteFactura: " + ex.Message };
                }
            }
        }

        /// <summary>
        /// Obtener Periodo Facturación
        /// </summary>
        /// <returns>Lista de Periodo Facturación</returns>
        public MethodResponseDTO<List<C_PeriodosDTO>> ObtenerPeriodo()
        {
            using (var context = new DB_9F97CF_CatarsysSGCEntities())
            {
                try
                {
                    var response = new MethodResponseDTO<List<C_PeriodosDTO>>() { Code = 0 };


                    var obj = context.C_Periodos.ToList();

                    response.Result = Mapper.Map<List<C_PeriodosDTO>>(obj);

                    return response;
                }
                catch (Exception ex)
                {
                    return new MethodResponseDTO<List<C_PeriodosDTO>> { Code = -100, Message = "ObtenerPeriodo: " + ex.Message };
                }
            }
        }

        /// <summary>
        /// Obtener Moneda
        /// </summary>
        /// <returns>Lista de Periodo Moneda</returns>
        public MethodResponseDTO<List<C_MonedaDTO>> ObtenerMoneda()
        {
            using (var context = new DB_9F97CF_CatarsysSGCEntities())
            {
                try
                {
                    var response = new MethodResponseDTO<List<C_MonedaDTO>>() { Code = 0 };


                    var obj = context.C_Moneda.ToList();

                    response.Result = Mapper.Map<List<C_MonedaDTO>>(obj);

                    return response;
                }
                catch (Exception ex)
                {
                    return new MethodResponseDTO<List<C_MonedaDTO>> { Code = -100, Message = "ObtenerMoneda: " + ex.Message };
                }
            }
        }

        /// <summary>
        /// Obtener IVA
        /// </summary>
        /// <returns>Lista de IVA</returns>
        public MethodResponseDTO<List<C_IVADTO>> ObtenerIVA()
        {
            using (var context = new DB_9F97CF_CatarsysSGCEntities())
            {
                try
                {
                    var response = new MethodResponseDTO<List<C_IVADTO>>() { Code = 0 };


                    var obj = context.C_IVA.ToList();

                    response.Result = Mapper.Map<List<C_IVADTO>>(obj);

                    return response;
                }
                catch (Exception ex)
                {
                    return new MethodResponseDTO<List<C_IVADTO>> { Code = -100, Message = "ObtenerIVA: " + ex.Message };
                }
            }
        }

        /// <summary>
        /// Obtener Tipo Cambio
        /// </summary>
        /// <returns>Lista de tipo asignacion</returns>
        public MethodResponseDTO<List<C_Tipo_CambioDTO>> ObtenerTipoCambio()
        {
            using (var context = new DB_9F97CF_CatarsysSGCEntities())
            {
                try
                {
                    var response = new MethodResponseDTO<List<C_Tipo_CambioDTO>>() { Code = 0 };


                    var tipoAsignacion = context.C_Tipo_Cambio.ToList();

                    response.Result = Mapper.Map<List<C_Tipo_CambioDTO>>(tipoAsignacion);

                    return response;
                }
                catch (Exception ex)
                {
                    return new MethodResponseDTO<List<C_Tipo_CambioDTO>> { Code = -100, Message = "ObtenerTipoCambio: " + ex.Message };
                }
            }
        }

        /// <summary>
        /// Obtener Permisos
        /// </summary>
        /// <returns>Lista de tipo asignacion</returns>
        public MethodResponseDTO<List<PermisosDTO>> ObtenerPermisos()
        {
            using (var context = new DB_9F97CF_CatarsysSGCEntities())
            {
                try
                {
                    var response = new MethodResponseDTO<List<PermisosDTO>>() { Code = 0 };


                    var tipoAsignacion = context.ctPermisos.Where(x=>x.Estado_Permiso == true).ToList();

                    response.Result = Mapper.Map<List<PermisosDTO>>(tipoAsignacion);

                    return response;
                }
                catch (Exception ex)
                {
                    return new MethodResponseDTO<List<PermisosDTO>> { Code = -100, Message = "Obtener Permisos DTO: " + ex.Message };
                }
            }
        }
    }
}
