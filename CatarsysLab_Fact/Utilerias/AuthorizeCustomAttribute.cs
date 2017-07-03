using ModeloDatos.Gestion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CatarsysLab_Fact.Utilerias
{
    public class AuthorizeCustomAttribute : AuthorizeAttribute
    {
        public string IdObjetos { get; set; }
        public string IdTipoPermiso { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var isAuthorized = base.AuthorizeCore(httpContext);
            if (!isAuthorized)
            {
                return false;
            }

            //TODO: esto quizas podría hacerse mejor obteniendo los objetos tipo vista del usuario
            // y despyes verificar si alguno de los IdObjetos se encuentra en estos objetos del usuario
            // además así podemos guardar los objetos del sistema en cache para que no los busque de nuevo

            IPrincipal user = httpContext.User;
            EmpreadosData _Empleados = new EmpreadosData();



            var rolesDeUsuario = System.Web.Security.Roles.GetRolesForUser(user.Identity.Name);


            if (rolesDeUsuario.Count() > 0)
            {
                foreach (var idObjeto in IdObjetos.Split(','))
                {

                    if (rolesDeUsuario.Contains(idObjeto) || idObjeto.Equals("0"))
                    {
                        if (IdTipoPermiso.Equals("2"))
                        {
                            var tipopermiso = _Empleados.ObtenerPermisosTipo(int.Parse(user.Identity.Name),int.Parse(idObjeto));
                            if (tipopermiso.Code == 0)
                                return tipopermiso.Result;
                            else
                                return false;
                        }
                        else
                            return true;
                    }
                }
            }


            return false;

        }

        //Llamado cuando el acceso es denegado
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            //Empleados no esta logeado
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(new { controller = "Account", action = "LoginAuth", returnUrl = filterContext.RequestContext.HttpContext.Request.Path.Trim() })
                );
            }
            //Empleados esta logueado pero no tiene acceso
            else
            {
                if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.Result = new JsonResult()
                    {
                        Data = new
                        {
                            success = false,
                            mensaje = "Su usuario no tiene privilegios a " + filterContext.RequestContext.HttpContext.Request.Path.Trim() + ". Comuníquese con el administrador del sistema para poder acceder.",
                            type = "default",
                            mensajeintro = "",
                            title = "¡SIN PRIVILEGIOS!"
                        },
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };

                    filterContext.HttpContext.Response.Clear();
                    filterContext.HttpContext.Response.StatusCode = 500;
                }
                else
                {
                    filterContext.Result = new RedirectToRouteResult(
                            new RouteValueDictionary(new { controller = "Account", action = "SinPrivilegios", url = filterContext.RequestContext.HttpContext.Request.Path.Trim(), muestraModal = true })
                    );
                }
            }
        }
    }
}