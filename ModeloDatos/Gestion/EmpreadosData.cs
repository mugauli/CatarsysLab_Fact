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
    public class EmpreadosData
    {
        /// <summary>
        /// Obtener Empleados
        /// Con IdEmpleado = 0 regresara todos los Empleado
        /// Con idEmpresa =  0 regresara todos Empleado de todas las empresas
        /// Con Estado = 2 regresara todos los Empleado de todos los estados
        /// </summary>
        /// <param name="IdEmpleado"></param>
        /// <param name="IdEmpresa"></param>
        /// <param name="Estado"></param>
        /// <returns></returns>
        public MethodResponseDTO<List<EmpleadosDTO>> ObtenerEmpleados(int IdEmpleado, int IdEmpresa, int Estado)
        {
            using (var context = new DB_9F97CF_CatarsysSGCEntities())
            {
                try
                {
                    var response = new MethodResponseDTO<List<EmpleadosDTO>>() { Code = 0 };


                    var clientes = context.Empleados.Where(x => (x.Id_Empleado == IdEmpleado || IdEmpleado == 0)
                                                           && (x.Id_Empresa == IdEmpresa || IdEmpresa == 0)
                                                           && (x.Estado == (Estado == 1) || Estado == 2)).ToList();

                    response.Result = Mapper.Map<List<EmpleadosDTO>>(clientes);

                    return response;
                }
                catch (Exception ex)
                {
                    return new MethodResponseDTO<List<EmpleadosDTO>> { Code = -100, Message = "ObtenerEmpleados: " + ex.Message };
                }
            }
        }

        /// <summary>
        /// Obtener Empleados por username
        /// </summary>
        /// <param name="Username"></param>
        /// <returns>EmpleadosDTO</returns>
        public MethodResponseDTO<EmpleadosDTO> ObtenerEmpleados(string Username)
        {
            using (var context = new DB_9F97CF_CatarsysSGCEntities())
            {
                try
                {
                    var response = new MethodResponseDTO<EmpleadosDTO>() { Code = 0 };


                    var clientes = context.Empleados.Where(x => x.Usuario_Empleado == Username && x.Estado.Value == true && x.IsLogIn.Value == 1).FirstOrDefault();

                    response.Result = Mapper.Map<EmpleadosDTO>(clientes);

                    return response;
                }
                catch (Exception ex)
                {
                    return new MethodResponseDTO<EmpleadosDTO> { Code = -100, Message = "ObtenerEmpleados: " + ex.Message };
                }
            }
        }

        /// <summary>
        /// Guardar Empleado
        /// </summary>
        /// <returns>Lista de tipo asignacion</returns>
        public MethodResponseDTO<int> GuardarEmpleado(EmpleadosDTO empleado)
        {
            using (var context = new DB_9F97CF_CatarsysSGCEntities())
            {
                try
                {
                    var response = new MethodResponseDTO<int>() { Code = 0, Result = 1 };


                    if (empleado.Id_Empleado == 0)
                    {
                        var objDB = Mapper.Map<Empleados>(empleado);
                        context.Empleados.Add(objDB);
                    }
                    else
                    {
                        var objDB = context.Empleados.SingleOrDefault(x => x.Id_Empleado == empleado.Id_Empleado);

                        objDB.Id_Empleado = empleado.Id_Empleado;
                        objDB.Id_Empresa = empleado.Id_Empresa;
                        objDB.Nombre_Empleado = empleado.Nombre_Empleado;
                        objDB.Email_Empleado = empleado.Email_Empleado;
                        objDB.Puesto_Empleado = empleado.Puesto_Empleado;
                        objDB.Fecha_Nacimiento_Empleado = empleado.Fecha_Nacimiento_Empleado;
                        objDB.Antiguedad_Empleado = empleado.Antiguedad_Empleado;
                        objDB.Skype_Empleado = empleado.Skype_Empleado;
                        objDB.Domicilio_Empleado = empleado.Domicilio_Empleado;
                        objDB.Telefono_L_Empleado = empleado.Telefono_L_Empleado;
                        objDB.Telefono_M_Empleado = empleado.Telefono_M_Empleado;
                        objDB.Id_JefeInmediato_Empleado = empleado.Id_JefeInmediato_Empleado;
                        objDB.IsLogIn = empleado.IsLogIn;
                        objDB.Usuario_Empleado = empleado.Usuario_Empleado;
                        if (!String.IsNullOrEmpty(empleado.Password_Empleado))
                        {
                            objDB.Password_Empleado = empleado.Password_Empleado;
                            objDB.Salt = empleado.Salt;
                        }
                        objDB.Estado = empleado.Estado;

                        //objDB.Id_Perfil = empleado.Id_Perfil;

                    }

                    context.SaveChanges();

                    var permisos = GuardarPermisos(empleado.EmpleadoPermiso);
                    if (permisos.Code != 0)
                    {
                        return new MethodResponseDTO<int> { Code = -100, Result = 0, Message = permisos.Message };
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
                    return new MethodResponseDTO<int> { Code = -100, Result = 0, Message = Result };
                }
                catch (Exception ex)
                {
                    return new MethodResponseDTO<int> { Code = -100, Result = 0, Message = "GuardarAsignacion: " + ex.Message };
                }
            }
        }

        /// <summary>
        /// Guardar Asignacion
        /// </summary>
        /// <returns>Lista de tipo asignacion</returns>
        public MethodResponseDTO<int> GuardarPermisos(List<EmpleadoPermisoDTO> permisos)
        {
            using (var context = new DB_9F97CF_CatarsysSGCEntities())
            {
                try
                {
                    var response = new MethodResponseDTO<int>() { Code = 0, Result = 1 };

                    foreach (var perm in permisos)
                    {
                        var objDB = context.EmpleadoPermiso.Where(x => x.Id_Empleado == perm.Id_Empleado && perm.Id_Permiso == x.Id_Permiso).FirstOrDefault();
                        if (objDB == null)
                        {
                            objDB = Mapper.Map<EmpleadoPermiso>(perm);
                            context.EmpleadoPermiso.Add(objDB);
                        }
                        else
                        {
                            objDB.Id_Empleado = perm.Id_Empleado;
                            objDB.Id_Permiso = perm.Id_Permiso;
                            objDB.Tipo_Permiso = perm.Tipo_Permiso;
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

        public MethodResponseDTO<bool> ObtenerPermisosTipo(int IdEmpleado,int Idpermiso)
        {
            using (var context = new DB_9F97CF_CatarsysSGCEntities())
            {
                try
                {
                    var response = new MethodResponseDTO<bool> { Code = 0 };


                    var objDB = context.EmpleadoPermiso.Where(a => a.Id_Empleado == IdEmpleado && a.Id_Permiso == Idpermiso).FirstOrDefault();

                   

                    response.Result = (objDB.Tipo_Permiso == 2 || objDB.Tipo_Permiso == 3);

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
                    return new MethodResponseDTO<bool> { Code = -100, Message = Result };
                }
                catch (Exception ex)
                {
                    return new MethodResponseDTO<bool> { Code = -100, Message = "Obtener permiso Tipo: " + ex.Message };
                }
            }
        }

        public MethodResponseDTO<List<EmpleadoPermisoDTO>> ObtenerPermisos(int IdEmpleado)
        {
            using (var context = new DB_9F97CF_CatarsysSGCEntities())
            {
                try
                {
                    var response = new MethodResponseDTO<List<EmpleadoPermisoDTO>> { Code = 0 };


                    var objDB = context.EmpleadoPermiso
                            .Join(context.ctPermisos, c => c.Id_Permiso,cm => cm.Id_Permisos, (c, cm) => new { EmpPer = c, Per = cm })
                            .Where(a => a.EmpPer.Id_Empleado == IdEmpleado && a.Per.Estado_Permiso == true && a.EmpPer.Tipo_Permiso > 0).Select(x => x.EmpPer);

                    response.Result = Mapper.Map<List<EmpleadoPermisoDTO>>(objDB);

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
                    return new MethodResponseDTO<List<EmpleadoPermisoDTO>> { Code = -100, Message = Result };
                }
                catch (Exception ex)
                {
                    return new MethodResponseDTO<List<EmpleadoPermisoDTO>> { Code = -100, Message = "GuardarAsignacion: " + ex.Message };
                }
            }
        }

        public MethodResponseDTO<EmpleadoPermisoDTO> ObtenRolActivo(int IdPermiso)
        {
            using (var context = new DB_9F97CF_CatarsysSGCEntities())
            {
                try
                {
                    var response = new MethodResponseDTO<EmpleadoPermisoDTO> { Code = 0 };


                    var objDB = context.ctPermisos.SingleOrDefault(r => r.Id_Permisos == IdPermiso && r.Estado_Permiso == true);

                    if (objDB != null)
                        response.Result = Mapper.Map<EmpleadoPermisoDTO>(objDB);

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
                    return new MethodResponseDTO<EmpleadoPermisoDTO> { Code = -100, Message = Result };
                }
                catch (Exception ex)
                {
                    return new MethodResponseDTO<EmpleadoPermisoDTO> { Code = -100, Message = "GuardarAsignacion: " + ex.Message };
                }
            }
        }

        public MethodResponseDTO<PaginacionDTO<List<EmpleadosPaginadorDTO>>> ObtenerEmpleadosPaginacion(int page, int size, int sort, string sortDireccion, string filter, int Id_Empresa)
        {
            using (var context = new DB_9F97CF_CatarsysSGCEntities())
            {
                try
                {
                    var response = new MethodResponseDTO<PaginacionDTO<List<EmpleadosPaginadorDTO>>>() { Code = 0 };

                    var responsePag = new PaginacionDTO<List<EmpleadosPaginadorDTO>>();


                    ObjectParameter totalrow = new ObjectParameter("totalrow", typeof(int));
                    var obj = context.sp_GetEmpleadosPaginacion(page, size, sort, Id_Empresa, sortDireccion, filter, totalrow).ToList();


                    responsePag.data = Mapper.Map<List<EmpleadosPaginadorDTO>>(obj);
                    responsePag.recordsTotal = Convert.ToInt32(totalrow.Value);
                    responsePag.draw = page;

                    response.Result = responsePag;
                    return response;
                }
                catch (Exception ex)
                {
                    return new MethodResponseDTO<PaginacionDTO<List<EmpleadosPaginadorDTO>>> { Code = -100, Message = "ObtenerEmpleadosPaginador: " + ex.Message };
                }
            }
        }
    }
}
