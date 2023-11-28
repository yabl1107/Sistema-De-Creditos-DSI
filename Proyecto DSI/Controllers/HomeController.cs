using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto_DSI.Permisos;

using CapaEntidad;
using CapaNegocio;


namespace Proyecto_DSI.Controllers
{
    [ValidarSesion] //Antes de que ejecuten cualquiera de las vistas, se ejecuta validarSesion 'onactinoexcecuting'
    public class HomeController : Controller
    {

        //Un http get , una url q se ejecuta y te devuelve datos
        [HttpGet]
        public JsonResult ListarCalendario(string regId)
        {
            List<Calendario> oLista = new List<Calendario>();
            oLista = new CN_CALENDARIO().ListarCalendario(regId);
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            return RedirectToAction("VerPerfil", "Cliente");
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult CerrarSesion()
        {
            Session["usuario"] = null;
            return RedirectToAction("Login", "Acceso");
        }



        public ActionResult VerPerfil()
        {
            return View();
        }

        public ActionResult EditarPerfil()
        {
            return View();
        }






        /*
        [HttpGet]
        public JsonResult ListarUsuarios()
        {
            List<Cliente> oLista = new List<Cliente>();
            oLista = new CN_Usuarios().ListarClientes();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }*/


    }
}