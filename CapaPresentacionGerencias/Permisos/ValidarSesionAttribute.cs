using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CapaPresentacionGerencias.Permisos
{
    public class ValidarSesionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session["Empleado"] == null)
            {
                filterContext.Result = new RedirectResult("~/Acceso/IniciarSesion");
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
