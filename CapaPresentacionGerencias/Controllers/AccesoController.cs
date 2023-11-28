using CapaEntidad;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CapaPresentacionGerencias.Controllers
{
    public class AccesoController : Controller
    {
        // GET: Acceso
        [HttpPost]
        public ActionResult IniciarSesion(string correo, string clave)
        {
            Empleado oEmpleado = null;
            //Obtener el trabajador con credenciales ingresadas
            string contraEncriptada = CN_RECURSOS.ConvertirSha256(clave);

            oEmpleado = new CN_EMPLEADOS().ListarEmpleados().Where(item => item.Email == correo && item.Clave == contraEncriptada ).FirstOrDefault();
            
            if(oEmpleado == null)
            {
                ViewBag.Error = "Correo o contraseña no son correctas";
                return View();

            }
            else
            {
                Session["Empleado"] = oEmpleado;
                ViewBag.Error = null;

                return RedirectToAction("VerPerfil", "Home");
            }

        }

        public ActionResult IniciarSesion()
        {
            return View();
        }

        public ActionResult CerrarSesion()
        {
            Session["Empleado"] = null;
            return RedirectToAction("IniciarSesion", "Acceso");
        }

        /*
        // GET: Acceso/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Acceso/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Acceso/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Acceso/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Acceso/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Acceso/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Acceso/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        */
    }
}
