﻿using CatarsysLab_Fact.Models;
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
    }
}