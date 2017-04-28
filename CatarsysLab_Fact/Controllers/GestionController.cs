﻿using CatarsysLab_Fact.Models;
using CatarsysLab_Fact.Utilerias;
using DTO.Catalogos;
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
    public class GestionController : Controller
    {
        #region Asignaciones
        // GET: Gestion
        public ActionResult Asignaciones()
        {
            Session[Constantes.Session.Empresa] = 1;
            var _CatalogosData = new CatalogosData();
            if (Session[Constantes.Session.Empresa] == null)
            {
                ViewBag.Titulo = "Info";
                ViewBag.Mensaje = "Ocurrio un error al obtener la empresa seleccionada.";
                return View("_InfoMensaje");
            }

            int IdEmpresa = Convert.ToInt32(Session[Constantes.Session.Empresa]);

            ViewBag.Empresa = Convert.ToInt32(Session[Constantes.Session.Empresa]);

            var response = new AsignacionModel();

            #region Clientes
            var clientes = new ClienteData().ObtenerClientes(0, IdEmpresa, 1);

            if (clientes.Code != 0)
            {
                ViewBag.Titulo = "Info";
                ViewBag.Mensaje = "Ocurrio un error al obtener los clientes activos. Error: " + clientes.Message;
                return View("_InfoMensaje");
            }
            response.ctClientes = clientes.Result;
            #endregion

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

            #region Tipo asignacion
            var tipoAsignacion = _CatalogosData.ObtenerTipoAsignacion();

            if (tipoAsignacion.Code != 0)
            {
                ViewBag.Titulo = "Info";
                ViewBag.Mensaje = "Ocurrio un error al obtener los Tipo asignacion. Error: " + tipoAsignacion.Message;
                return View("_InfoMensaje");
            }
            response.ctTipoAsignacion = tipoAsignacion.Result;
            #endregion

            #region Corte
            var Corte = _CatalogosData.ObtenerCorteFactura();

            if (Corte.Code != 0)
            {
                ViewBag.Titulo = "Info";
                ViewBag.Mensaje = "Ocurrio un error al obtener Corte. Error: " + Corte.Message;
                return View("_InfoMensaje");
            }
            response.ctCorte = Corte.Result;
            #endregion

            #region ctPeriodo
            var Periodo = _CatalogosData.ObtenerPeriodo();

            if (Periodo.Code != 0)
            {
                ViewBag.Titulo = "Info";
                ViewBag.Mensaje = "Ocurrio un error al obtener Periodo. Error: " + Periodo.Message;
                return View("_InfoMensaje");
            }
            response.ctPeriodo = Periodo.Result;
            #endregion

            #region ctMoneda
            var Moneda = _CatalogosData.ObtenerMoneda();

            if (Moneda.Code != 0)
            {
                ViewBag.Titulo = "Info";
                ViewBag.Mensaje = "Ocurrio un error al obtener Moneda. Error: " + Moneda.Message;
                return View("_InfoMensaje");
            }
            response.ctMoneda = Moneda.Result;
            #endregion

            #region ctIva
            var Iva = _CatalogosData.ObtenerIVA();

            if (Iva.Code != 0)
            {
                ViewBag.Titulo = "Info";
                ViewBag.Mensaje = "Ocurrio un error al obtener IVA. Error: " + Iva.Message;
                return View("_InfoMensaje");
            }
            response.ctIva = Iva.Result;
            #endregion


            return View(response);
        }

        [HttpPost]
        public JsonResult TablePaginacion(int draw, int start, int length, int company, string search,string order)
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


            var data = new AsignacionData().ObtenerAsignacionesPaginacion(start, length, sortColumn, sortDirection, search, company);

            data.Result.draw = draw;
            data.Result.recordsFiltered = data.Result.recordsTotal;



            return Json(data.Result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarAsignacion(int idAsignacion, int frIdEmpresa, int Cliente, int Consultor, int Tipo, string fecha_inicio, string fecha_fin, int Corte, decimal Costo, int Periodo, int Moneda, int IVA, int estado)
        {

            var asignacion = new AsignacionDTO();

            if(idAsignacion != 0) asignacion.Id_Asignacion = idAsignacion;
            asignacion.Id_Cliente_Asignacion = Cliente;
            asignacion.Id_Empresa = frIdEmpresa;
            asignacion.Id_Empleado_Asignacion = Consultor;
            asignacion.Id_Tipo_Asignacion = Tipo;
            asignacion.Fecha_Ini_Asignacion = Convert.ToDateTime(fecha_inicio);
            if (fecha_fin != string.Empty) asignacion.Fecha_Fin_Asignacion = Convert.ToDateTime(fecha_fin);
            asignacion.Id_Corte_Asignacion = Corte;
            asignacion.Costo_Pactado_Asignacion = Costo;
            asignacion.Id_Periodo_Asignacion = Periodo;
            asignacion.Id_Moneda_Asignacion = Moneda;
            asignacion.Id_IVA_Asignacion = IVA;
            asignacion.Id_Estatus_Asignacion = estado == 0 ? 2 : 1;

            var gdAsignacion = new AsignacionData().GuardarAsignacion(asignacion);

            if(gdAsignacion.Code != 0)
                return Json(new { success = false, message = gdAsignacion.Message }, JsonRequestBehavior.AllowGet);

            return Json(new {success = true}, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ObtenerAsignacion(int IdAsignacion)
        {

            var gdAsignacion = new AsignacionData().ObtenerAsignaciones(IdAsignacion, 0);

            if (gdAsignacion.Code != 0)
                return Json(new { success = false, message = gdAsignacion.Message }, JsonRequestBehavior.AllowGet);

            return Json(new { success = true, info = gdAsignacion.Result}, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Proyectos
        public ActionResult Proyectos()
        {
            Session[Constantes.Session.Empresa] = 1;
            var _CatalogosData = new CatalogosData();
            if (Session[Constantes.Session.Empresa] == null)
            {
                ViewBag.Titulo = "Info";
                ViewBag.Mensaje = "Ocurrio un error al obtener la empresa seleccionada.";
                return View("_InfoMensaje");
            }

            int IdEmpresa = Convert.ToInt32(Session[Constantes.Session.Empresa]);

            ViewBag.Empresa = Convert.ToInt32(Session[Constantes.Session.Empresa]);

            var response = new ProyectoModel();

            #region Clientes
            var clientes = new ClienteData().ObtenerClientes(0, IdEmpresa, 1);

            if (clientes.Code != 0)
            {
                ViewBag.Titulo = "Info";
                ViewBag.Mensaje = "Ocurrio un error al obtener los clientes activos. Error: " + clientes.Message;
                return View("_InfoMensaje");
            }
            response.ctClientes = clientes.Result;
            #endregion

            #region ctMoneda
            var Moneda = _CatalogosData.ObtenerMoneda();

            if (Moneda.Code != 0)
            {
                ViewBag.Titulo = "Info";
                ViewBag.Mensaje = "Ocurrio un error al obtener Moneda. Error: " + Moneda.Message;
                return View("_InfoMensaje");
            }
            response.ctMoneda = Moneda.Result;
            #endregion

            #region ctIva
            var Iva = _CatalogosData.ObtenerIVA();

            if (Iva.Code != 0)
            {
                ViewBag.Titulo = "Info";
                ViewBag.Mensaje = "Ocurrio un error al obtener IVA. Error: " + Iva.Message;
                return View("_InfoMensaje");
            }
            response.ctIva = Iva.Result;
            #endregion

            #region ctIva
            var tipoCambio = _CatalogosData.ObtenerTipoCambio();

            if (tipoCambio.Code != 0)
            {
                ViewBag.Titulo = "Info";
                ViewBag.Mensaje = "Ocurrio un error al obtener Tipo de Cambio. Error: " + tipoCambio.Message;
                return View("_InfoMensaje");
            }
            response.ctTipoCambio = tipoCambio.Result;
            #endregion

            #region Facturas
            var facturaFake = new List<FacturasDTO>();
            //facturaFake.Add(new FacturasDTO { Id_Factura = 1, Monto = 35000, FechaFactura = "16/Mayo/2017" });
            //facturaFake.Add(new FacturasDTO { Id_Factura = 2, Monto = 45000, FechaFactura = "16/Junio/2017" });
            //facturaFake.Add(new FacturasDTO { Id_Factura = 3, Monto = 55000, FechaFactura = "16/Agosto/2017" });
            response.Facturas = facturaFake;
            #endregion

            return View(response);
           
        }
        [HttpPost]
        public JsonResult TablePaginacionProy(int draw, int start, int length, int company,int filter2, string search, string order)
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

                sortColumn = int.Parse(datos2[0])>0? int.Parse(datos2[0]): 1;
                sortDirection = datos2[1];

            }


            var data = new ProyectosData().ObtenerProyectosPaginacion(start, length, sortColumn, sortDirection, search,filter2, company);

            data.Result.draw = draw;
            data.Result.recordsFiltered = data.Result.recordsTotal;



            return Json(data.Result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost] 
        public JsonResult GuardarProyecto(int idProyecto, int frIdEmpresa, int Cliente, string NombreProyecto, string fecha_inicio, string fecha_fin, decimal Costo, int Moneda, int CantidadFacturas, int TipoCambio, int IVA, int estado, string Comentarios)
        {

            var proyecto = new ProyectosDTO();

            if (idProyecto != 0) proyecto.Id_Proyectos = idProyecto;            
            proyecto.Id_Empresa = frIdEmpresa;
            proyecto.Id_Clientes_Proyectos = Cliente;
            proyecto.Nombre_Proyectos = NombreProyecto;
            proyecto.Numero_Facturas_Proyectos = CantidadFacturas;
            proyecto.Fecha_Ini_Proyectos = Convert.ToDateTime(fecha_inicio);
            if (fecha_fin != string.Empty) proyecto.Fecha_Fin_Proyectos = Convert.ToDateTime(fecha_fin);
            proyecto.Costo_Proyectos = Costo;
            proyecto.Id_Tipo_Cambio_Proyectos = TipoCambio;
            proyecto.Id_Moneda_Proyectos = Moneda;
            proyecto.Id_IVA_Proyectos = IVA;
            proyecto.Comentarios_Proyecto = Comentarios;
            proyecto.Estado = estado == 1;

            var gdProyecto = new ProyectosData().GuardarProyecto(proyecto);

            if (gdProyecto.Code != 0)
                return Json(new { success = false, message = gdProyecto.Message }, JsonRequestBehavior.AllowGet);

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ObtenerProyecto(int IdProyecto)
        {

            var gdAsignacion = new ProyectosData().ObtenerProyecto(IdProyecto, 2);

            if (gdAsignacion.Code != 0)
                return Json(new { success = false, message = gdAsignacion.Message }, JsonRequestBehavior.AllowGet);

            return Json(new { success = true, info = gdAsignacion.Result }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Facturas
        public ActionResult Facturacion()
        {
            return View();
        }
        #endregion

    }
}