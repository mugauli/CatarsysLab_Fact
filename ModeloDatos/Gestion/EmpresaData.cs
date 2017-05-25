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
    public class EmpresaData
    {
        /// <summary>
        /// Obtener Empresas -> 
        /// Con IdEmpresa = 0 regresara todas las Empresas
        /// Con Estado = 2 regresara todas las Empresas de todos los estados
        /// </summary>
        /// <param name="IdEmpresa"></param>
        /// <param name="Estado"></param>
        /// <returns></returns>
        public MethodResponseDTO<List<EmpresaDTO>> ObtenerEmpresas(int Id_Empresa, int Estado)
        {
            using (var context = new DB_9F97CF_CatarsysSGCEntities())
            {
                try
                {
                    var response = new MethodResponseDTO<List<EmpresaDTO>>() { Code = 0 };


                    var obj = context.Empresa.Where(x => (x.Id_Empresa == Id_Empresa || Id_Empresa == 0)
                                                           && (x.Estado_Empresa == (Estado == 1) || Estado == 2)).ToList();

                    response.Result = Mapper.Map<List<EmpresaDTO>>(obj);

                    return response;
                }
                catch (Exception ex)
                {
                    return new MethodResponseDTO<List<EmpresaDTO>> { Code = -100, Message = "ObtenerEmpleados: " + ex.Message };
                }
            }
        }

        /// <summary>
        /// Guardar Empresas
        /// </summary>
        /// <returns>Lista de tipo asignacion</returns>
        public MethodResponseDTO<int> GuardarEmpresas(EmpresaDTO empresa)
        {
            using (var context = new DB_9F97CF_CatarsysSGCEntities())
            {
                try
                {
                    var response = new MethodResponseDTO<int>() { Code = 0, Result = 1 };


                    if (empresa.Id_Empresa == 0)
                    {
                        var objDB = Mapper.Map<Empresa>(empresa);
                        context.Empresa.Add(objDB);
                    }
                    else
                    {
                        var objDB = context.Empresa.SingleOrDefault(x => x.Id_Empresa == empresa.Id_Empresa);

                        objDB.Id_Empresa = empresa.Id_Empresa;
                        objDB.Nombre_Empresa = empresa.Nombre_Empresa;
                        objDB.Razon_Social_Empresa = empresa.Razon_Social_Empresa;
                        objDB.RFC_Empresa = empresa.RFC_Empresa;
                        objDB.Calle_Empresa = empresa.Calle_Empresa;
                        objDB.No_Ext_Empresa = empresa.No_Ext_Empresa;
                        objDB.No_Int_Empresa = empresa.No_Int_Empresa;
                        objDB.Colonia_Empresa = empresa.Colonia_Empresa;
                        objDB.CP_Empresa = empresa.CP_Empresa;
                        objDB.Del_Mun_Empresa = empresa.Del_Mun_Empresa;
                        objDB.Estado_Dom_Empresa = empresa.Estado_Dom_Empresa;
                        objDB.Email_Empresa = empresa.Email_Empresa;
                        objDB.Fecha_Creacion_Empresa = empresa.Fecha_Creacion_Empresa;
                        objDB.Estado_Empresa = empresa.Estado_Empresa;

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
                    return new MethodResponseDTO<int> { Code = -100, Result = 0, Message = Result };
                }
                catch (Exception ex)
                {
                    return new MethodResponseDTO<int> { Code = -100, Result = 0, Message = "GuardarAsignacion: " + ex.Message };
                }
            }
        }

        public MethodResponseDTO<PaginacionDTO<List<EmpresasPaginadorDTO>>> ObtenerEmpresasPaginacion(int page, int size, int sort, string sortDireccion, string filter, int Id_Empresa)
        {
            using (var context = new DB_9F97CF_CatarsysSGCEntities())
            {
                try
                {
                    var response = new MethodResponseDTO<PaginacionDTO<List<EmpresasPaginadorDTO>>>() { Code = 0 };

                    var responsePag = new PaginacionDTO<List<EmpresasPaginadorDTO>>();


                    ObjectParameter totalrow = new ObjectParameter("totalrow", typeof(int));
                    var obj = context.sp_GetEmpresaPaginacion(page, size, sort, Id_Empresa, sortDireccion, filter, totalrow).ToList();


                    responsePag.data = Mapper.Map<List<EmpresasPaginadorDTO>>(obj);
                    responsePag.recordsTotal = Convert.ToInt32(totalrow.Value);
                    responsePag.draw = page;

                    response.Result = responsePag;
                    return response;
                }
                catch (Exception ex)
                {
                    return new MethodResponseDTO<PaginacionDTO<List<EmpresasPaginadorDTO>>> { Code = -100, Message = "ObtenerEmpresasPaginador: " + ex.Message };
                }
            }
        }
    }
}
