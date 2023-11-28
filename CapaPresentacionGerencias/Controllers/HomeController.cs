using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using CapaEntidad;
using CapaNegocio;
using CapaPresentacionGerencias.Permisos;

namespace CapaPresentacionGerencias.Controllers
{
    [ValidarSesion] //Antes de que ejecuten cualquiera de las vistas, se ejecuta validarSesion 'onactinoexcecuting'
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("VerPerfil", "Home");
        }
        public ActionResult Estadisticas()
        {
            return View();
        }
        public ActionResult VerPerfil()
        {
            return View();
        }

        public ActionResult EditarPerfil()
        {
            return View();
        }

        //Un http get , una url q se ejecuta y te devuelve datos
        [HttpGet]
        public JsonResult ListarCalendario(string regId)
        {
            List<Calendario> oLista = new List<Calendario>();
            oLista = new CN_CALENDARIO().ListarCalendario(regId);
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }


        //Un http get , una url q se ejecuta y te devuelve datos
        [HttpGet]
        public JsonResult ListarUsuarios()
        {
            List<Cliente> oLista = new List<Cliente>();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }        
        
        [HttpGet]
        public JsonResult ListarEmpleados()
        {
            List<Empleado> oLista = new List<Empleado>();
            oLista = new CN_EMPLEADOS().ListarEmpleados();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarClientes()
        {
            List<Cliente> oLista = new List<Cliente>();
            oLista = new CN_CLIENTES().ListarClientes();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListarCreditos()
        {
            List<Credito> oLista = new List<Credito>();
            oLista = new CN_CREDITOS().ListarCreditos();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult ListarSolicitudes()
        {
            List<Solicitud> oLista = new List<Solicitud>();
            oLista = new CN_SOLICITUDES().ListarSolicitudes();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListarSolicitudesPendientes()
        {
            List<Solicitud> oLista = new List<Solicitud>();
            oLista = new CN_SOLICITUDES().ListarSolicitudesPendientes();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListarSolicitudesAtendidas()
        {
            List<Solicitud> oLista = new List<Solicitud>();
            oLista = new CN_SOLICITUDES().ListarSolicitudesAtendidas();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarSolicitudesDeTrabajadorEnSesion()
        {
            List<Solicitud> oLista = new List<Solicitud>();
            Empleado empleado = Session["Empleado"] as Empleado;
            string nro = empleado.IDempleado.ToString();
            oLista = new CN_SOLICITUDES().ListarSolicitudesDeUnTrabajador(nro);
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult CrearEmpleado(Empleado obj)
        {
            int  resultado;
            string mensaje = string.Empty;

            resultado = new CN_EMPLEADOS().RegistrarEmpleado(obj, out mensaje);

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet );
        }


        [HttpPost]
        public JsonResult CrearCredito(Credito obj) //Acepta la solicitud y crea un credito con esa solicitud
        {
            string mensaje = string.Empty;
            string mensaje2 = string.Empty;

            new CN_CREDITOS().RegistrarCredito(obj, out mensaje);

            new CN_SOLICITUDES().ModificarEstadoSolicitud(obj.IdSolicitud,obj.IdEmpleado, "Aprobado", out mensaje2);

            Solicitud ns = null;
            ns  = new CN_SOLICITUDES().ListarSolicitudesAtendidas().Where(item => item.idSolicitud == obj.IdSolicitud).FirstOrDefault();

            return Json(new { resultado = ns, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult RechazarSolicitud(Credito obj) //Rechaza la solicitud
        {
            string mensaje = string.Empty;
            string mensaje2 = string.Empty;


            new CN_SOLICITUDES().ModificarEstadoSolicitud(obj.IdSolicitud, obj.IdEmpleado, "Rechazado", out mensaje2);

            Solicitud ns = null;
            ns = new CN_SOLICITUDES().ListarSolicitudesAtendidas().Where(item => item.idSolicitud == obj.IdSolicitud).FirstOrDefault();

            return Json(new { resultado = ns, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult EditarEmpleado(Empleado obj)
        {
            bool resultado;
            string mensaje = string.Empty;


            resultado = new CN_EMPLEADOS().EditarEmpleados(obj, out mensaje);

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EditarEmpleadoRenovarSesion(Empleado obj)
        {

            bool resultado;
            string mensaje = string.Empty;

            Session["Empleado"] = obj;

            resultado = new CN_EMPLEADOS().EditarEmpleados(obj, out mensaje);

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]

        public JsonResult EliminarEmpleado(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;


            respuesta = new CN_EMPLEADOS().EliminarEmpleado(id, out mensaje);

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        } 

    }
}