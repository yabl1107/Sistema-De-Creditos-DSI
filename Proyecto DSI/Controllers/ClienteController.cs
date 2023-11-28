using CapaEntidad;
using CapaNegocio;
using Proyecto_DSI.Permisos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_DSI.Controllers
{
    [ValidarSesion]
    public class ClienteController : Controller
    {
        // GET: Cliente


        [HttpPost]
        public JsonResult EditarClienteRenovarSesion(Cliente obj)
        {

            bool resultado;
            string mensaje = string.Empty;

            

            resultado = new CN_CLIENTES().EditarClientes(obj, out mensaje);
            if (resultado)
            {
                Session["Cliente"] = obj;
            }

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }


 


        [HttpPost]
        public ActionResult RealizarSolicitud(Solicitud obj)
        {

            bool resultado;
            string mensaje = string.Empty;

            new CN_SOLICITUDES().RegistrarSolicitud(obj, out mensaje);
            if(mensaje== "ERROR")
            {
                return RedirectToAction("NuevaSolicitud", "Cliente", new { mensaje = "Cliente ya cuenta con solicitud activa" });
            }
            else
            {
                return RedirectToAction("Solicitudes", "Cliente");
            }


            //resultado = new CN_CLIENTES().EditarClientes(obj, out mensaje);


            //return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult ListarSolicitudesDeClienteEnSesion()
        {
            List<Solicitud> oLista = new List<Solicitud>();
            Cliente cliente = Session["Cliente"] as Cliente;
            string nro = cliente.idCliente.ToString();
            oLista = new CN_SOLICITUDES().ListarSolicitudesDeUnCliente(nro);
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult ListarCreditosDeClienteEnSesion()
        {
            List<Credito> oLista = new List<Credito>();
            Cliente cliente = Session["Cliente"] as Cliente;
            string nro = cliente.idCliente.ToString();
            oLista = new CN_CREDITOS().ListarCreditosDeCliente(nro);
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult VerPerfil()
        {
            return View();
        }

        public ActionResult EditarPerfil()
        {
            return View();
        }



        public ActionResult Creditos()
        {
            return View();
        }


        public ActionResult VerCalendario(string dato)
        {
            ViewBag.Dato = dato;

            return View();
        }

        public ActionResult Pagos()
        {
            return RedirectToAction("VerCalendario", "Cliente");
        }



        public ActionResult Solicitudes()
        {
            return View();
        }
        public ActionResult NuevaSolicitud(string mensaje)
        {
            ViewBag.Mensaje = mensaje;
            return View();
        }

    }
}