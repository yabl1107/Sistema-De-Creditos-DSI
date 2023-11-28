using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.Text;


using CapaEntidad;
using CapaNegocio;


namespace Proyecto_DSI.Controllers
{
    public class AccesoController : Controller
    {
        //Cadena de conexion a base de datos 

        static string cadena = "Data Source=DESKTOP-AJLUJD1\\SQLEXPRESS;Initial Catalog=DB_ACCESO;Integrated Security=true";


        // GET: Acceso

        public ActionResult Registrar()
        {
            return View();
        }
        public ActionResult About()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult CerrarSesion()
        {
            Session["Cliente"] = null;
            return RedirectToAction("Login", "Acceso");
        }



        [HttpGet]
        public JsonResult ListarClientes()
        {
            List<Cliente> oLista = new List<Cliente>();
            oLista = new CN_CLIENTES().ListarClientes();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult Login(string Correo, string Clave)
        {
            Cliente oCliente = null;
            //Obtener el trabajador con credenciales ingresadas
            string contraEncriptada = CN_RECURSOS.ConvertirSha256(Clave);

            oCliente = new CN_CLIENTES().ListarClientes().Where(item => item.Correo == Correo && item.Clave == contraEncriptada).FirstOrDefault();

            if (oCliente == null)
            {
                ViewData["Mensaje"] = "USUARIO NO ENCONTRADO";
                return View();

            }
            else
            {
                Session["Cliente"] = oCliente;
                ViewBag.Error = null;

                return RedirectToAction("Index", "Home");
            }

        }

        [HttpPost]
        public ActionResult Registrar(Cliente oUsuario)
        {
            bool registrado; //Almacenan resultado del query
            string mensaje;

            if (oUsuario.Clave == oUsuario.ConfirmarClave)
            {
                oUsuario.Clave = ConvertirSha256(oUsuario.Clave);
            }
            else
            {
                //ViewData permite enviar mensajes del controlador a la vista
                ViewData["Mensaje"] = "Las contraseñas no coinciden";
                return View();
            }

            using (SqlConnection cn = new SqlConnection(cadena))
            {

                SqlCommand cmd = new SqlCommand("InsertarCliente", cn);
                cmd.Parameters.AddWithValue("FechaNac", oUsuario.FechaNacimiento);
                cmd.Parameters.AddWithValue("DNI", oUsuario.DNI);
                cmd.Parameters.AddWithValue("Nombre", oUsuario.Nombre);
                cmd.Parameters.AddWithValue("Apellidos", oUsuario.Apellidos);
                cmd.Parameters.AddWithValue("UbigeoNac", oUsuario.UbigeoNacimiento);
                cmd.Parameters.AddWithValue("Telf", oUsuario.Telefono);
                cmd.Parameters.AddWithValue("Email", oUsuario.Correo);
                cmd.Parameters.AddWithValue("Clave", oUsuario.Clave);
                cmd.Parameters.AddWithValue("UbigeoVivienda", oUsuario.UbigeoVivienda);
                cmd.Parameters.AddWithValue("Sexo", oUsuario.Sexo);

                cmd.Parameters.Add("Registrado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                cmd.ExecuteNonQuery();

                registrado = Convert.ToBoolean(cmd.Parameters["Registrado"].Value);
                mensaje = cmd.Parameters["Mensaje"].Value.ToString();


            }

            ViewData["Mensaje"] = mensaje;

            if (registrado)
            {
                return RedirectToAction("Login", "Acceso");
            }
            else
            {
                return View();
            }

        }

        public static string ConvertirSha256(string texto)
        {
            //using System.Text;
            //USAR LA REFERENCIA DE "System.Security.Cryptography"

            StringBuilder Sb = new StringBuilder();
            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(texto));

                foreach (byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }
    }
}