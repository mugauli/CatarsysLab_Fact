using CatarsysLab_Fact.Models;
using CatarsysLab_Fact.Utilerias;
using DTO.Gestion;
using ModeloDatos.Catalogos;
using ModeloDatos.Gestion;
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
        #endregion

        #region Clientes
        public ActionResult Clientes()
        {
            return View();
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
            var perfiles = new CatalogosData().ObtenerPerfiles();

            if (perfiles.Code != 0)
            {
                ViewBag.Titulo = "Info";
                ViewBag.Mensaje = "Ocurrio un error al obtener los empleados activos." + perfiles.Message;
                return View("_InfoMensaje");
            }
            response.ctPerfil = perfiles.Result;
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
        public JsonResult GuardarEmpleado(int idEmpleado, int frIdEmpresa, string Nombre, string Puesto,int JefeInmediato, string fecha_nacimiento, string fecha_ingreso, string Email, string Skype, string Movil,string Casa, 
            string Domicilio,int IsLogin, string Usuario, string Password, string Password2, int estado, int Perfil)
        {

            var empleados = new EmpleadosDTO();

            empleados.Id_Empleado = idEmpleado;
            empleados.Id_Empresa = frIdEmpresa;
            empleados.Nombre_Empleado = Nombre;
            empleados.Email_Empleado = Email;
            empleados.Puesto_Empleado = Puesto;
            if(fecha_nacimiento != string.Empty) empleados.Fecha_Nacimiento_Empleado = Convert.ToDateTime(fecha_nacimiento);
            if (fecha_ingreso != string.Empty)  empleados.Antiguedad_Empleado = Convert.ToDateTime(fecha_ingreso);
            empleados.Skype_Empleado = Skype;
            empleados.Domicilio_Empleado = Domicilio;
            empleados.Telefono_L_Empleado = Casa;
            empleados.Telefono_M_Empleado = Movil;
            if(JefeInmediato != 0) empleados.Id_JefeInmediato_Empleado = JefeInmediato;
            empleados.IsLogIn = IsLogin;
            empleados.Usuario_Empleado = Usuario;
            empleados.Password_Empleado = Password;
            empleados.Estado = estado == 1;
            if (Perfil != 0) empleados.Id_Perfil = Perfil;

            var gdAsignacion = new EmpreadosData().GuardarEmpleado(empleados);

            if (gdAsignacion.Code != 0)
                return Json(new { success = false, message = gdAsignacion.Message }, JsonRequestBehavior.AllowGet);

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ObtenerEmpleado(int IdEmpleado)
        {

            var gdEmpleado = new EmpreadosData().ObtenerEmpleados(IdEmpleado,0,2);

            if (gdEmpleado.Code != 0)
                return Json(new { success = false, message = gdEmpleado.Message }, JsonRequestBehavior.AllowGet);

            return Json(new { success = true, info = gdEmpleado.Result }, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}