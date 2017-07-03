using CatarsysLab_Fact.Models;
using CatarsysLab_Fact.Utilerias;
using DTO.Catalogos;
using DTO.Gestion;
using ModeloDatos.Catalogos;
using ModeloDatos.Gestion;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CatarsysLab_Fact.Controllers
{
    public class GestionController : Controller
    {
        Helpers _helpers = new Helpers();

        #region Dashboard
        public ActionResult Dashboard()
        {

            return View();
        }
        #endregion

        #region Asignaciones
        // GET: Gestion
        [AuthorizeCustom(IdObjetos = "2", IdTipoPermiso = "1")]
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

        [AuthorizeCustom(IdObjetos = "2", IdTipoPermiso = "1")]
        [HttpPost]
        public JsonResult TablePaginacion(int draw, int start, int length, int company, string search, string order)
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


            var data = new AsignacionData().ObtenerAsignacionesPaginacion(start, length, sortColumn, sortDirection, search, company);

            data.Result.draw = draw;
            data.Result.recordsFiltered = data.Result.recordsTotal;



            return Json(data.Result, JsonRequestBehavior.AllowGet);
        }

        [AuthorizeCustom(IdObjetos = "2", IdTipoPermiso = "2")]
        [HttpPost]
        public JsonResult GuardarAsignacion(int idAsignacion, int frIdEmpresa, int Cliente, int Consultor, int Tipo, string fecha_inicio, string fecha_fin, int Corte, decimal Costo, int Periodo, int Moneda, int IVA, int estado)
        {

            var asignacion = new AsignacionDTO();

            if (idAsignacion != 0) asignacion.Id_Asignacion = idAsignacion;
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

            if (gdAsignacion.Code != 0)
                return Json(new { success = false, message = gdAsignacion.Message }, JsonRequestBehavior.AllowGet);

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        [AuthorizeCustom(IdObjetos = "2", IdTipoPermiso = "2")]
        [HttpPost]
        public JsonResult ObtenerAsignacion(int IdAsignacion)
        {

            var gdAsignacion = new AsignacionData().ObtenerAsignaciones(IdAsignacion, 0);

            if (gdAsignacion.Code != 0)
                return Json(new { success = false, message = gdAsignacion.Message }, JsonRequestBehavior.AllowGet);

            return Json(new { success = true, info = gdAsignacion.Result }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Proyectos

        [AuthorizeCustom(IdObjetos = "3", IdTipoPermiso = "1")]
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

        [AuthorizeCustom(IdObjetos = "3", IdTipoPermiso = "1")]
        [HttpPost]
        public JsonResult TablePaginacionProy(int draw, int start, int length, int company, int filter2, string search, string order)
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

                sortColumn = int.Parse(datos2[0]) > 0 ? int.Parse(datos2[0]) : 1;
                sortDirection = datos2[1];

            }


            var data = new ProyectosData().ObtenerProyectosPaginacion(start, length, sortColumn, sortDirection, search, filter2, company);

            data.Result.draw = draw;
            data.Result.recordsFiltered = data.Result.recordsTotal;



            return Json(data.Result, JsonRequestBehavior.AllowGet);
        }

        [AuthorizeCustom(IdObjetos = "3", IdTipoPermiso = "2")]
        [HttpPost]
        public JsonResult GuardarProyecto(int idProyecto, int frIdEmpresa, int Cliente, string NombreProyecto, string fecha_inicio, string fecha_fin, decimal Costo, int Moneda, int CantidadFacturas, int TipoCambio, int IVA, int estado, string Comentarios, string tablaJSON)
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

            var factLts = new List<FacturasDTO>();

            #region JSN Facturas

            var jArray = JArray.Parse(tablaJSON);
            JObject a = JObject.Parse(jArray.First().ToString());

            foreach (var prod in ((JArray)a.SelectToken("ltsFacturas")))
            {
                var facturaJ = new FacturasDTO();
                facturaJ.IdFactura = Convert.ToInt16(((JValue)prod.SelectToken("IdFactura")).Value);
                facturaJ.Monto_Factura = Convert.ToDecimal(((JValue)prod.SelectToken("Monto")).Value);
                facturaJ.Fecha_fin_Factura = Convert.ToDateTime(((JValue)prod.SelectToken("Fecha")).Value);
                facturaJ.IdCliente = Cliente;
                facturaJ.IdEmpresa = frIdEmpresa;
                if (idProyecto != 0) facturaJ.IdProyecto = idProyecto;
                facturaJ.C_Id_IVA = IVA;
                facturaJ.C_Id_Moneda = Moneda;
                facturaJ.C_Id_Tipo_Cambio = TipoCambio;
                facturaJ.Tipo_Factura = "PR";
                var EstadoFact = ((JValue)prod.SelectToken("Estado")).Value.ToString();
                facturaJ.Id_Estado_Factura = Convert.ToInt16(EstadoFact.Equals("SI") ? 2 : (EstadoFact.Equals("NO") ? 1 : 3));
                if (((JValue)prod.SelectToken("NoFactura")).Value.ToString() != "0") facturaJ.No_Factura = Convert.ToInt16(((JValue)prod.SelectToken("NoFactura")).Value);

                factLts.Add(facturaJ);
            }

            #endregion

            proyecto.facturas = factLts;

            var gdProyecto = new ProyectosData().GuardarProyecto(proyecto);

            if (gdProyecto.Code != 0)
                return Json(new { success = false, message = gdProyecto.Message }, JsonRequestBehavior.AllowGet);

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        [AuthorizeCustom(IdObjetos = "3", IdTipoPermiso = "1")]
        [HttpPost]
        public JsonResult ObtenerProyecto(int IdProyecto)
        {

            var gdAsignacion = new ProyectosData().ObtenerProyecto(IdProyecto, 0, 2);

            if (gdAsignacion.Code != 0)
                return Json(new { success = false, message = gdAsignacion.Message }, JsonRequestBehavior.AllowGet);

            return Json(new { success = true, info = gdAsignacion.Result }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Facturas

        [AuthorizeCustom(IdObjetos = "4", IdTipoPermiso = "1")]
        public ActionResult Facturacion()
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

            var response = new FacturacionModel();

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

            #region ctMetodoPago
            var ctMetodoPago = _CatalogosData.ObtenerMetodoPago();

            if (ctMetodoPago.Code != 0)
            {
                ViewBag.Titulo = "Info";
                ViewBag.Mensaje = "Ocurrio un error al obtener ctMetodoPago. Error: " + ctMetodoPago.Message;
                return View("_InfoMensaje");
            }
            response.ctMetodoPago = ctMetodoPago.Result;
            #endregion

            #region tipoCambio
            var tipoCambio = _CatalogosData.ObtenerTipoCambio();

            if (tipoCambio.Code != 0)
            {
                ViewBag.Titulo = "Info";
                ViewBag.Mensaje = "Ocurrio un error al obtener Tipo de Cambio. Error: " + tipoCambio.Message;
                return View("_InfoMensaje");
            }
            response.ctTipoCambio = tipoCambio.Result;
            #endregion

            #region Empleados
            var Empleados = new EmpreadosData().ObtenerEmpleados(0, IdEmpresa, 1);

            if (Empleados.Code != 0)
            {
                ViewBag.Titulo = "Info";
                ViewBag.Mensaje = "Ocurrio un error al obtener  los Empleados. Error: " + tipoCambio.Message;
                return View("_InfoMensaje");
            }
            response.ctEmpleado = Empleados.Result;
            #endregion

            #region Proyectos
            var Proyectos = new ProyectosData().ObtenerProyecto(0, IdEmpresa, 1);

            if (Proyectos.Code != 0)
            {
                ViewBag.Titulo = "Info";
                ViewBag.Mensaje = "Ocurrio un error al obtener  los Empleados. Error: " + tipoCambio.Message;
                return View("_InfoMensaje");
            }
            response.ctProyectos = Proyectos.Result;
            #endregion

            return View(response);
        }

        [AuthorizeCustom(IdObjetos = "4", IdTipoPermiso = "1")]
        [HttpPost]
        public JsonResult TablePaginacionFacturas(int draw, int start, int length, int company, int filter2, string search, string order)
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

                sortColumn = int.Parse(datos2[0]) > 0 ? int.Parse(datos2[0]) : 1;
                sortDirection = datos2[1];

            }

            sortColumn = sortColumn == 0 ? 1 : sortColumn;

            var data = new FacturasData().ObtenerFacturasPaginacion(start, length, sortColumn, sortDirection, search, 0, 0, DateTime.Now.AddYears(-10), company);

            data.Result.draw = draw;
            data.Result.recordsFiltered = data.Result.recordsTotal;

            return Json(data.Result, JsonRequestBehavior.AllowGet);
        }

        [AuthorizeCustom(IdObjetos = "4", IdTipoPermiso = "2")]
        [HttpPost]
        public JsonResult GuardarFacturas(int IdFactura, int frIdEmpresa, int Cliente, int? Proyecto, int? Asignacion, string fecha_inicio, string fecha_fin, decimal Monto, decimal? Descontar, int Moneda, short NoFactura, int? TipoCambio, int MetodoPago, int IVA, int asigProy, string Comentarios, short Digitos)
        {

            var objFactura = new FacturasDTO();

            objFactura.IdFactura = IdFactura;
            objFactura.IdCliente = Cliente;
            objFactura.IdEmpresa = frIdEmpresa;
            if (asigProy == 1) objFactura.IdAsignacion = Asignacion.Value;
            else objFactura.IdProyecto = Proyecto.Value;
            objFactura.C_Id_IVA = IVA;
            objFactura.C_Id_Moneda = Moneda;
            objFactura.C_Id_Tipo_Cambio = TipoCambio;
            objFactura.C_Id_Metodo_Pago = MetodoPago;
            objFactura.Tipo_Factura = asigProy == 1 ? "AS" : "PR";

            DateTime date;
            if (DateTime.TryParse(fecha_inicio, out date)) objFactura.Fecha_Inicio_Factura = date;
            if (DateTime.TryParse(fecha_fin, out date)) objFactura.Fecha_fin_Factura = date;

            objFactura.No_Factura = NoFactura;
            objFactura.Descuento_Factura = Descontar;
            objFactura.Monto_Factura = Monto;
            objFactura.Ultimos_4_digitos_Factura = Digitos;
            if (IdFactura == 0) objFactura.Id_Estado_Factura = 1;

            objFactura.Comentarios = Comentarios;

            var gdFactura = new FacturasData().GuardarFactura(objFactura);

            if (gdFactura.Code != 0)
                return Json(new { success = false, message = gdFactura.Message }, JsonRequestBehavior.AllowGet);

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        [AuthorizeCustom(IdObjetos = "4", IdTipoPermiso = "2")]
        [HttpPost]
        public JsonResult ObtenerFacturas(int IdFacturas)
        {

            var gdAsignacion = new FacturasData().ObtenerFacturas(IdFacturas);

            if (gdAsignacion.Code != 0)
                return Json(new { success = false, message = gdAsignacion.Message }, JsonRequestBehavior.AllowGet);

            var cliente = new ClienteData().ObtenerClientes(gdAsignacion.Result.First().IdCliente, 0, 2);
            if (cliente.Code != 0)
                return Json(new { success = false, message = cliente.Message }, JsonRequestBehavior.AllowGet);


            return Json(new { success = true, info = gdAsignacion.Result, cliente = cliente.Result }, JsonRequestBehavior.AllowGet);
        }

        [AuthorizeCustom(IdObjetos = "4", IdTipoPermiso = "2")]
        [HttpPost]
        public ActionResult UploadFiles()
        {
            // Checking no of files injected in Request object  
            if (Request.Files.Count > 0)
            {
                try
                {
                    var pago = new PagosFacturasDTO();
                    var doctosLts = new List<DocumentosFacturasDTO>();


                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    pago.IdFactura = Convert.ToInt32(Request.Form["IdFactura"]);
                    pago.Monto = Convert.ToDecimal(Request.Form["Monto"]);
                    pago.Concepto = Request.Form["Concepto"];
                    pago.Descripcion = Request.Form["Descripcion"];

                    for (int i = 0; i < files.Count; i++)
                    {
                        var docto = new DocumentosFacturasDTO();


                        HttpPostedFileBase file = files[i];
                        string fname, fname2;

                        // Checking for Internet Explorer  
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                            fname2 = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName.ToLowerInvariant();
                            fname2 = file.FileName.ToLowerInvariant();
                        }

                        // Get the complete folder path and store the file inside it.  
                        fname = Path.Combine(Server.MapPath("~/Facturas/"), fname);
                        if (System.IO.File.Exists(fname))
                        {

                            string[] nameExt = fname.Split('.');
                            fname2 = Path.Combine(Server.MapPath("~/Facturas/"), nameExt[0] + "_" + DateTime.Now.ToString("ss") + "." + nameExt[1]);
                            file.SaveAs(fname2);
                            docto.Url = fname2;
                        }
                        else
                        {
                            file.SaveAs(fname);
                            docto.Url = fname;
                        }
                        doctosLts.Add(docto);
                    }



                    var pagos = new FacturasData().GuardarPagosFacturas(pago);
                    if (pagos.Code != 0)
                        return Json("Erroredc: " + pagos.Message);

                    var doctos = new FacturasData().GuardarDocumentosFacturas(pagos.Result, doctosLts);
                    if (doctos.Code != 0)
                        return Json("Error: " + pagos.Message);
                    // Returns message that successfully uploaded  
                    return Json("Pago realizado con exito!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No hay archivos seleccionados.");
            }
        }

        [AuthorizeCustom(IdObjetos = "4", IdTipoPermiso = "2")]
        [HttpPost]
        public JsonResult ObtenerNumeroFactura(int Id, int tipo)
        {
            var numberObj = new FacturasData().ObtenerNumeroMaxFactura(Id, tipo);

            return Json(new { success = numberObj.Code == 0, message = numberObj.Message, data = numberObj.Result }, JsonRequestBehavior.AllowGet);
        }

        [AuthorizeCustom(IdObjetos = "4", IdTipoPermiso = "2")]
        [HttpPost]
        public JsonResult ObtenerProyectosCliente(int IdCliente, int tipo)
        {
            var numberObj = new ProyectosData().ObtenerProyectoAsignacionCliente(IdCliente, tipo);

            return Json(new { success = numberObj.Code == 0, data = numberObj.Result, message = numberObj.Message }, JsonRequestBehavior.AllowGet);
        }

        [AuthorizeCustom(IdObjetos = "4", IdTipoPermiso = "2")]
        [HttpPost]
        public JsonResult ObtenerDatosFiscales(int IdCliente)
        {
            var cliente = new ClienteData().ObtenerClientes(IdCliente, 0, 2);


            return Json(new { success = cliente.Code == 0, data = cliente.Result, message = cliente.Message }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Shared

        [AuthorizeCustom(IdObjetos = "4", IdTipoPermiso = "2")]
        [HttpPost]
        public JsonResult AjustarFacturas(int tipo, int id, DateTime inicio, DateTime fin)
        {
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }



        #endregion


    }
}