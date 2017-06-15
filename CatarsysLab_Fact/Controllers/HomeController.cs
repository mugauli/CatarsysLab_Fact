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
    
    public class HomeController : Controller
    {
        [AuthorizeCustom(IdObjetos = "0", IdTipoPermiso = "1")]
        public ActionResult Index()
        {
            Session[Constantes.Session.Empresa] = 1;
            return View();
        }
        [AuthorizeCustom(IdObjetos = "0", IdTipoPermiso = "1")]
        public ActionResult BannerEmpresa()
        {

            var response = new BannerModel();

            var empresas = new EmpresaData().ObtenerEmpresas(0, 1);

            if (empresas.Code != 0)
            {
                ViewBag.Titulo = "Info";
                ViewBag.Mensaje = "Ocurrio un error al obtener las empresas. Error: " + empresas.Message;
                return View("_InfoMensaje");
            }

            ViewBag.DateNow = DateTime.Now.ToString("dd / MMMMMM / yyyy");

            response.ctEmpresas = empresas.Result;
           // response.ctEmpresas = new List<EmpresaDTO>();

            return View(response);
        }
        [AuthorizeCustom(IdObjetos = "0", IdTipoPermiso = "1")]
        [HttpPost]
        public JsonResult ActualizaEmpresa(int Empresa)
        {
            Session[Constantes.Session.Empresa] = Empresa;

            return Json(new { succes = true }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult TablePendientesPorFacturarPaginacion(int draw, int start, int length, int company, string search, string order)
        {
            Session[Constantes.Session.Empresa] = company;

            //string search = Request["search[value]"] == null ? string.Empty : Request["search[value]"];

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


            var data = new FacturasData().ObtenerPendientesPorFacturarPaginacion(start, length, sortColumn, sortDirection, search, 0, 0, DateTime.Now.AddYears(-10), company);
            

            data.Result.draw = draw;
            data.Result.recordsFiltered = data.Result.recordsTotal;



            return Json(data.Result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult TableProximosIngresosPaginacion(int draw, int start, int length, int company, string search, string order)
        {
            Session[Constantes.Session.Empresa] = company;

            //string search = Request["search[value]"] == null ? string.Empty : Request["search[value]"];

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


            var data = new FacturasData().ObtenerProximosIngresosPaginacion(start, length, sortColumn, sortDirection, search, 0, 0, DateTime.Now.AddYears(-10), company);


            data.Result.draw = draw;
            data.Result.recordsFiltered = data.Result.recordsTotal;



            return Json(data.Result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult TableServiciosActualesPaginacion_Proyectos(int draw, int start, int length, int company, string search, string order)
        {
            Session[Constantes.Session.Empresa] = company;

            //string search = Request["search[value]"] == null ? string.Empty : Request["search[value]"];

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


            var data = new FacturasData().ObtenerServiciosActualesPaginacion_Proyecto(start, length, sortColumn, sortDirection, search, 0, 0, DateTime.Now.AddYears(-10), company);


            data.Result.draw = draw;
            data.Result.recordsFiltered = data.Result.recordsTotal;



            return Json(data.Result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult TableServiciosActualesPaginacion_Asignaciones(int draw, int start, int length, int company, string search, string order)
        {
            Session[Constantes.Session.Empresa] = company;

            //string search = Request["search[value]"] == null ? string.Empty : Request["search[value]"];

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


            var data = new FacturasData().ObtenerServiciosActualesPaginacion_Asignacion(start, length, sortColumn, sortDirection, search, 0, 0, DateTime.Now.AddYears(-10), company);


            data.Result.draw = draw;
            data.Result.recordsFiltered = data.Result.recordsTotal;



            return Json(data.Result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult TableCarteraVencidaPaginacion(int draw, int start, int length, int company, string search, string order)
        {
            Session[Constantes.Session.Empresa] = company;

            //string search = Request["search[value]"] == null ? string.Empty : Request["search[value]"];

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


            var data = new FacturasData().ObtenerCarteraVencidaPaginacion(start, length, sortColumn, sortDirection, search, 0, 0, DateTime.Now.AddYears(-10), company);


            data.Result.draw = draw;
            data.Result.recordsFiltered = data.Result.recordsTotal;



            return Json(data.Result, JsonRequestBehavior.AllowGet);
        }
    }
}