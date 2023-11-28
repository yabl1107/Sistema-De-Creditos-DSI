using CapaPresentacionGerencias.Permisos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CapaPresentacionGerencias.Controllers
{
    [ValidarSesion] //Antes de que ejecuten cualquiera de las vistas, se ejecuta validarSesion 'onactinoexcecuting'
    public class GestionController : Controller
    {
        // GET: Creditos
        public ActionResult Trabajadores()
        {
            return View();
        }

        public ActionResult VerCalendario(string dato)
        {
            ViewBag.Dato = dato;

            return View();
        }

        public ActionResult VerTrabajador()
        {
            return View();
        }
        public ActionResult Clientes()
        {
            return View();
        }
        public ActionResult Creditos()
        {
            return View();
        }
        public ActionResult Solicitudes()
        {
            return View();
        }
    }
}