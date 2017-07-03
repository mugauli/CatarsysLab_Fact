using CatarsysLab_Fact.Models;
using CatarsysLab_Fact.Utilerias;
using ModeloDatos.Gestion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace CatarsysLab_Fact.Controllers
{
    public class AccountController : Controller
    {
        public CustomMembershipProvider MembershipService { get; set; }
        EmpreadosData _Empleados = new EmpreadosData();

        protected override void Initialize(RequestContext requestContext)
        {
            if (MembershipService == null)
                MembershipService = new CustomMembershipProvider();
            //if (AuthorizationService == null)
            //    AuthorizationService = new CustomRoleProvider();

            base.Initialize(requestContext);
        }

        // GET: Account
        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public ActionResult LoginAuth(AccountLoginViewModel login)
        {
            EncriptPasswordSha3 encript = new EncriptPasswordSha3();

            if (string.IsNullOrEmpty(login.Username))
            {
                ModelState.AddModelError("Username", "El Usuario es obligatorio");
            }

            if (string.IsNullOrEmpty(login.Password))
            {
                ModelState.AddModelError("Password", "La contraseña es obligatoria");
            }

            if (ModelState.IsValid)
            {
                //var resUsCheca = _Empleados.ObtenerEmpleados(login.Username);

                if (MembershipService.ValidateUser(login.Username, login.Password))
                {

                    var usuario = _Empleados.ObtenerEmpleados(login.Username);

                    Session[Constantes.Session.Empleados] = usuario.Result;
                    Session[Constantes.Session.Empresa] = usuario.Result.Id_Empresa;

                    FormsAuthentication.SetAuthCookie(usuario.Result.Id_Empleado.ToString(), false);


                    return RedirectToLocal(login.returnUrl);


                }
                else
                {
                    ViewBag.MensajeError = "Usuario o password incorrecto";
                }


            }

            ViewBag.Login = true;

            return View(login);
        }

        [System.Web.Mvc.Authorize]
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [AllowAnonymous]
        public ActionResult Login()
        {


            return View();
        }

        [AllowAnonymous]
        public ActionResult LoginAuth(string returnUrl)
        {
            AccountLoginViewModel login = new AccountLoginViewModel();

            ViewBag.Login = true;
            login.returnUrl = returnUrl;

            return View(login);
        }

        public ActionResult SinPrivilegios()
        {
            return View();
        }

    }
}