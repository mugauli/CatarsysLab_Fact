using CatarsysLab_Fact.Models;
using CatarsysLab_Fact.Utilerias;
using DTO.Gestion;
using ModeloDatos.Catalogos;
using ModeloDatos.Gestion;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CatarsysLab_Fact.Controllers
{
    public class ConfiguracionController : Controller
    {
        #region Empresas
        // GET: Configuracion
        public ActionResult Empresas()
        {
            return View();
        }

        [HttpPost]
        public JsonResult TablePaginacionEmpresas(int draw, int start, int length, int company, string search, string order)
        {
            Session[Constantes.Session.Empresa] = company;

            int sortColumn = 1;
            string sortDirection = "asc";

            if (length == -1)
            {
                length = 100;
            }

            if (Request["order[0][]"] != null)
            {
                string datos = Request["order[0][]"];
                string[] datos2 = datos.Split(',');

                sortColumn = int.Parse(datos2[0]);
                sortDirection = datos2[1];

            }

            sortColumn = sortColumn == 0 ? 1 : sortColumn;
            var data = new EmpresaData().ObtenerEmpresasPaginacion(start, length, sortColumn, sortDirection, search, company);

            data.Result.draw = draw;
            data.Result.recordsFiltered = data.Result.recordsTotal;



            return Json(data.Result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarEmpresa(int idEmpresa, string Nombre, string RazonSocial, string RFC, string calle, string Exterior, string Interior, string Colonia, short CP, string DelMpio, string EstadoDomicilio, string Email, string fecha_creacion, int estado)
        {

            var empresa = new EmpresaDTO();
            empresa.Id_Empresa = idEmpresa;
            empresa.Nombre_Empresa = Nombre;
            empresa.Razon_Social_Empresa = RazonSocial;
            empresa.RFC_Empresa = RFC;
            empresa.Calle_Empresa = calle;
            empresa.No_Ext_Empresa = Exterior;
            empresa.No_Int_Empresa = Interior;
            empresa.Colonia_Empresa = Colonia;
            empresa.CP_Empresa = CP;
            empresa.Del_Mun_Empresa = DelMpio;
            empresa.Estado_Dom_Empresa = EstadoDomicilio;
            empresa.Email_Empresa = Email;
            if (fecha_creacion != string.Empty) empresa.Fecha_Creacion_Empresa = Convert.ToDateTime(fecha_creacion);
            empresa.Estado_Empresa = estado == 1;


            var obj = new EmpresaData().GuardarEmpresas(empresa);

            if (obj.Code != 0)
                return Json(new { success = false, message = obj.Message }, JsonRequestBehavior.AllowGet);

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ObtenerEmpresa(int IdEmpresa)
        {

            var obj = new EmpresaData().ObtenerEmpresas(IdEmpresa, 2);

            if (obj.Code != 0)
                return Json(new { success = false, message = obj.Message }, JsonRequestBehavior.AllowGet);

            return Json(new { success = true, info = obj.Result }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Clientes
        public ActionResult Clientes()
        {
            return View();
        }

        [HttpPost]
        public JsonResult TablePaginacionClientes(int draw, int start, int length, int company, string search, string order)
        {
            Session[Constantes.Session.Empresa] = company;

            int sortColumn = 1;
            string sortDirection = "asc";

            if (length == -1)
            {
                length = 100;
            }

            if (Request["order[0][]"] != null)
            {
                string datos = Request["order[0][]"];
                string[] datos2 = datos.Split(',');

                sortColumn = int.Parse(datos2[0]);
                sortDirection = datos2[1];

            }

            sortColumn = sortColumn == 0 ? 1 : sortColumn;
            var data = new ClienteData().ObtenerClientesPaginacion(start, length, sortColumn, sortDirection, search, company);

            data.Result.draw = draw;
            data.Result.recordsFiltered = data.Result.recordsTotal;



            return Json(data.Result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarCliente(int idCliente, int frIdEmpresa, string Nombre, string RazonSocial, string RFC, string calle, string Exterior, string Interior, string Colonia, short CP, string DelMpio, string EstadoDomicilio, string diasPago, int estado)
        {

            var cliente = new ClientesDTO();

            cliente.Id_Cliente = idCliente;
            cliente.Id_Empresa = frIdEmpresa;
            cliente.Nombre_Cliente = Nombre;
            cliente.Razon_Social_Cliente = RazonSocial;
            cliente.RFC_Cliente = RFC;
            cliente.Calle_Cliente = calle;
            cliente.Exterior_Cliente = Exterior;
            cliente.Interior_Cliente = Interior;
            cliente.Colonia_Cliente = Colonia;
            cliente.DelMun_Cliente = DelMpio;
            cliente.CP_Cliente = CP;
            cliente.Estado_Dom_Cliente = EstadoDomicilio;
            cliente.Dias_de_Pago_Cliente = diasPago;
            cliente.Estado = estado == 1;

            //public virtual ICollection<ContactosDTO> Contactos =
            var obj = new ClienteData().GuardarCliente(cliente);

            if (obj.Code != 0)
                return Json(new { success = false, message = obj.Message }, JsonRequestBehavior.AllowGet);

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarContacto(int idContacto, int idClienteContacto, string Nombre_Contacto, string Puesto_Contacto, string Email_Contacto, string Skype_Contacto, string Telefono_Contacto, string Movil_Contacto, string Comentario_Contacto, int estado, int enviaFactura)
        {

            var contacto = new ContactosDTO();

            contacto.Id_Contacto = idContacto;
            contacto.Id_Cliente = idClienteContacto;
            contacto.Nombre_Contacto = Nombre_Contacto;
            contacto.Puesto_Contacto = Puesto_Contacto;
            contacto.Email_Contacto = Email_Contacto;
            contacto.Telefono_Contacto = Telefono_Contacto;
            contacto.Movil__Contacto = Movil_Contacto;
            contacto.Skype_Contacto = Skype_Contacto;
            contacto.EnviaFactura_Contacto = enviaFactura == 1;
            contacto.Comentario_Contacto = Comentario_Contacto;
            contacto.Estado = estado == 1;

            var obj = new ClienteData().GuardarContactos(contacto);

            if (obj.Code != 0)
                return Json(new { success = false, message = obj.Message }, JsonRequestBehavior.AllowGet);

            var objContactos = new ClienteData().GetContactosCliente(contacto.Id_Cliente);

            if (objContactos.Code != 0)
                return Json(new { success = false, message = objContactos.Message }, JsonRequestBehavior.AllowGet);

            return Json(new { success = true , contactos = objContactos.Result }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ObtenerCliente(int IdCliente)
        {

            var obj = new ClienteData().ObtenerClientes(IdCliente, 0, 2);

            if (obj.Code != 0)
                return Json(new { success = false, message = obj.Message }, JsonRequestBehavior.AllowGet);

            return Json(new { success = true, info = obj.Result }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Empleados
        public ActionResult Empleados()
        {
            if (Session[Constantes.Session.Empresa] == null)
            {
                ViewBag.Titulo = "Info";
                ViewBag.Mensaje = "Ocurrio un error al obtener la empresa seleccionada.";
                return View("_InfoMensaje");
            }

            int IdEmpresa = Convert.ToInt32(Session[Constantes.Session.Empresa]);

            var response = new EmpleadoModel();

            #region Empleados
            var empleados = new EmpreadosData().ObtenerEmpleados(0, IdEmpresa, 1);

            if (empleados.Code != 0)
            {
                ViewBag.Titulo = "Info";
                ViewBag.Mensaje = "Ocurrio un error al obtener los empleados activos." + empleados.Message;
                return View("_InfoMensaje");
            }
            response.ctEmpleados = empleados.Result;
            #endregion

            #region Perfiles
            var ctPermisos = new CatalogosData().ObtenerPermisos();

            if (ctPermisos.Code != 0)
            {
                ViewBag.Titulo = "Info";
                ViewBag.Mensaje = "Ocurrio un error al obtener los empleados activos." + ctPermisos.Message;
                return View("_InfoMensaje");
            }
            response.ctPermisos = ctPermisos.Result;
            #endregion



            return View(response);
        }

        [HttpPost]
        public JsonResult TablePaginacionEmpleados(int draw, int start, int length, int company, string search, string order)
        {
            Session[Constantes.Session.Empresa] = company;

            int sortColumn = 1;
            string sortDirection = "asc";

            if (length == -1)
            {
                length = 100;
            }

            if (Request["order[0][]"] != null)
            {
                string datos = Request["order[0][]"];
                string[] datos2 = datos.Split(',');

                sortColumn = int.Parse(datos2[0]);
                sortDirection = datos2[1];

            }

            sortColumn = sortColumn == 0 ? 1 : sortColumn;
            var data = new EmpreadosData().ObtenerEmpleadosPaginacion(start, length, sortColumn, sortDirection, search, company);

            data.Result.draw = draw;
            data.Result.recordsFiltered = data.Result.recordsTotal;



            return Json(data.Result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarEmpleado(int idEmpleado, int frIdEmpresa, string Nombre, string Puesto, int? JefeInmediato, string fecha_nacimiento, string fecha_ingreso, string Email, string Skype, string Movil, string Casa,
            string Domicilio, int IsLogin, string Usuario, string Password, string Password2, int estado, string hdPermisos)
        {

            var empleados = new EmpleadosDTO();

            empleados.Id_Empleado = idEmpleado;
            empleados.Id_Empresa = frIdEmpresa;
            empleados.Nombre_Empleado = Nombre;
            empleados.Email_Empleado = Email;
            empleados.Puesto_Empleado = Puesto;
            if (fecha_nacimiento != string.Empty) empleados.Fecha_Nacimiento_Empleado = Convert.ToDateTime(fecha_nacimiento);
            if (fecha_ingreso != string.Empty) empleados.Antiguedad_Empleado = Convert.ToDateTime(fecha_ingreso);
            empleados.Skype_Empleado = Skype;
            empleados.Domicilio_Empleado = Domicilio;
            empleados.Telefono_L_Empleado = Casa;
            empleados.Telefono_M_Empleado = Movil;
            if (JefeInmediato != 0) empleados.Id_JefeInmediato_Empleado = JefeInmediato;
            else empleados.Id_JefeInmediato_Empleado = null;
            empleados.IsLogIn = IsLogin;
            empleados.Usuario_Empleado = Usuario;
            empleados.Password_Empleado = Password;
            empleados.Estado = estado == 1;

            
            var jArray = JArray.Parse(hdPermisos);
            JObject a = JObject.Parse(jArray.First().ToString());

            #region productos
            var permisos = new List<EmpleadoPermisoDTO>();
            foreach (var prod in ((JArray)a.SelectToken("ltsPermisos")))
            {
                EmpleadoPermisoDTO permiso = new EmpleadoPermisoDTO();
                permiso.Id_Empleado = empleados.Id_Empleado;
                permiso.Id_Permiso = Convert.ToInt16(((JValue)prod.SelectToken("Id_Permiso")).Value);
                permiso.Tipo_Permiso = Convert.ToInt16(((JValue)prod.SelectToken("Tipo_Permiso")).Value);
                permisos.Add(permiso);
            }
            #endregion

            empleados.EmpleadoPermiso = permisos;
                        

            var gdAsignacion = new EmpreadosData().GuardarEmpleado(empleados);

            if (gdAsignacion.Code != 0)
                return Json(new { success = false, message = gdAsignacion.Message }, JsonRequestBehavior.AllowGet);

            

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ObtenerEmpleado(int IdEmpleado)
        {

            var gdEmpleado = new EmpreadosData().ObtenerEmpleados(IdEmpleado, 0, 2);

            if (gdEmpleado.Code != 0)
                return Json(new { success = false, message = gdEmpleado.Message }, JsonRequestBehavior.AllowGet);

            return Json(new { success = true, info = gdEmpleado.Result }, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}