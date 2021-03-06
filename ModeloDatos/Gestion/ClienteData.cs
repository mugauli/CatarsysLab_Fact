﻿using AutoMapper;
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
    public class ClienteData
    {
        /// <summary>
        /// Obtener Clientes
        /// Con idcliente = 0 regresara todos los clientes
        /// Con idEmpresa =  0 regresara todos clientes de todas las empresas
        /// Con Estado = 2 regresara todos los clientes de todos los estados
        /// </summary>
        /// <param name="IdCliente"></param>
        /// <param name="IdEmpresa"></param>
        /// <param name="Estado"></param>
        /// <returns></returns>
        public MethodResponseDTO<List<ClientesDTO>> ObtenerClientes(int IdCliente, int IdEmpresa, int Estado)
        {
            using (var context = new DB_9F97CF_CatarsysSGCEntities())
            {
                try
                {
                    var response = new MethodResponseDTO<List<ClientesDTO>>() { Code = 0 };


                    var clientes = context.Clientes.Where(x => (x.Id_Cliente == IdCliente || IdCliente == 0)
                                                           && (x.Id_Empresa == IdEmpresa || IdEmpresa == 0)
                                                           && (x.Estado == (Estado == 1) || Estado == 2)).ToList();

                    response.Result = Mapper.Map<List<ClientesDTO>>(clientes);

                    return response;
                }
                catch (Exception ex)
                {
                    return new MethodResponseDTO<List<ClientesDTO>> { Code = -100, Message = "ObtenerClientes: " + ex.Message };
                }
            }
        }

        /// <summary>
        /// Guardar Clientes
        /// </summary>
        /// <returns>Lista de tipo asignacion</returns>
        public MethodResponseDTO<int> GuardarCliente(ClientesDTO clientes)
        {
            using (var context = new DB_9F97CF_CatarsysSGCEntities())
            {
                try
                {
                    var response = new MethodResponseDTO<int>() { Code = 0, Result = 1 };


                    if (clientes.Id_Empresa == 0)
                    {
                        var objDB = Mapper.Map<Clientes>(clientes);
                        context.Clientes.Add(objDB);
                    }
                    else
                    {
                        var objDB = context.Clientes.SingleOrDefault(x => x.Id_Cliente == clientes.Id_Cliente);

                        objDB.Id_Cliente = clientes.Id_Cliente;
                        objDB.Id_Empresa = clientes.Id_Empresa;
                        objDB.Nombre_Cliente = clientes.Nombre_Cliente;
                        objDB.Razon_Social_Cliente = clientes.Razon_Social_Cliente;
                        objDB.RFC_Cliente = clientes.RFC_Cliente;
                        objDB.Calle_Cliente = clientes.Calle_Cliente;
                        objDB.Exterior_Cliente = clientes.Exterior_Cliente;
                        objDB.Interior_Cliente = clientes.Interior_Cliente;
                        objDB.Colonia_Cliente = clientes.Colonia_Cliente;
                        objDB.DelMun_Cliente = clientes.DelMun_Cliente;
                        objDB.CP_Cliente = clientes.CP_Cliente;
                        objDB.Estado_Dom_Cliente = clientes.Estado_Dom_Cliente;
                        objDB.Dias_de_Pago_Cliente = clientes.Dias_de_Pago_Cliente;
                        objDB.Estado = clientes.Estado;
                        objDB.Contactos = Mapper.Map<List<Contactos>>(clientes.Contactos);
                    };
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

        public MethodResponseDTO<PaginacionDTO<List<ClientesPaginadorDTO>>> ObtenerClientesPaginacion(int page, int size, int sort, string sortDireccion, string filter, int Id_Empresa)
        {
            using (var context = new DB_9F97CF_CatarsysSGCEntities())
            {
                try
                {
                    var response = new MethodResponseDTO<PaginacionDTO<List<ClientesPaginadorDTO>>>() { Code = 0 };

                    var responsePag = new PaginacionDTO<List<ClientesPaginadorDTO>>();


                    ObjectParameter totalrow = new ObjectParameter("totalrow", typeof(int));
                    var obj = context.sp_GetClientesPaginacion(page, size, sort, Id_Empresa, sortDireccion, filter, totalrow).ToList();


                    responsePag.data = Mapper.Map<List<ClientesPaginadorDTO>>(obj);
                    responsePag.recordsTotal = Convert.ToInt32(totalrow.Value);
                    responsePag.draw = page;

                    response.Result = responsePag;
                    return response;
                }
                catch (Exception ex)
                {
                    return new MethodResponseDTO<PaginacionDTO<List<ClientesPaginadorDTO>>> { Code = -100, Message = "ObtenerClientesPaginador: " + ex.Message };
                }
            }
        }

        public MethodResponseDTO<List<ContactosDTO>> GetContactosCliente(int IdCliente)
        {
            using (var context = new DB_9F97CF_CatarsysSGCEntities())
            {
                try
                {
                    var response = new MethodResponseDTO<List<ContactosDTO>>() { Code = 0 };


                    var clientes = context.Contactos.Where(x => x.Id_Cliente == IdCliente && x.Estado == true).ToList();

                    response.Result = Mapper.Map<List<ContactosDTO>>(clientes);

                    return response;
                }
                catch (Exception ex)
                {
                    return new MethodResponseDTO<List<ContactosDTO>> { Code = -100, Message = "ObtenerContactos: " + ex.Message };
                }
            }
        }

        public MethodResponseDTO<int> GuardarContactos(ContactosDTO contactos)
        {
            using (var context = new DB_9F97CF_CatarsysSGCEntities())
            {
                try
                {
                    var response = new MethodResponseDTO<int>() { Code = 0, Result = 1 };


                    if (contactos.Id_Contacto == 0)
                    {
                        var objDB = Mapper.Map<Contactos>(contactos);
                        context.Contactos.Add(objDB);
                    }
                    else
                    {
                        var objDB = context.Contactos.SingleOrDefault(x => x.Id_Contacto == contactos.Id_Contacto);

                        objDB.Nombre_Contacto = contactos.Nombre_Contacto;
                        objDB.Puesto_Contacto = contactos.Puesto_Contacto;
                        objDB.Email_Contacto = contactos.Email_Contacto;
                        objDB.Telefono_Contacto = contactos.Telefono_Contacto;
                        objDB.Movil__Contacto = contactos.Movil__Contacto;
                        objDB.Skype_Contacto = contactos.Skype_Contacto;
                        objDB.Comentario_Contacto = contactos.Comentario_Contacto;
                        objDB.EnviaFactura_Contacto = contactos.EnviaFactura_Contacto;
                        objDB.Estado = contactos.Estado;
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
    }
}
