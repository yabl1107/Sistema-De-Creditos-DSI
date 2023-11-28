using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_EMPLEADOS
    {
        public CD_EMPLEADOS objCapaDato = new CD_EMPLEADOS();

        public List<Empleado> ListarEmpleados()
        {
            return objCapaDato.ListarEmpleados();
        }


        public int RegistrarEmpleado(Empleado obj, out string Mensaje)
        {
            //Se aplica las reglas del negocio
            Mensaje = string.Empty;
            if(string.IsNullOrEmpty(obj.Nombre) || string.IsNullOrWhiteSpace(obj.Nombre))
            {
                Mensaje = "El nombre del empleado no puede ser vacío";
            }
            else if (string.IsNullOrEmpty(obj.Apellidos) || string.IsNullOrWhiteSpace(obj.Apellidos))
            {
                Mensaje = "El apellido del empleado no puede ser vacío";
            }
            else if (string.IsNullOrEmpty(obj.Email) || string.IsNullOrWhiteSpace(obj.Email))
            {
                Mensaje = "El email del empleado no puede ser vacío";
            }

            if (string.IsNullOrEmpty(Mensaje)){
                //Enviar el correo al usuario

                //Clave por defecto test123
                string clave = "test123";
                //string clave = CN_RECURSOS.GenerarClave();

                string asunto = "Creacion de Cuenta Sistema de Creditos";
                string mensaje_correo = "<h3>Su cuenta fue creada correctamente</h3></br><p>Su contraseña para acceder es:  !clave!</p>";
                mensaje_correo = mensaje_correo.Replace("!clave!", clave);

                bool respuesta = CN_RECURSOS.EnviarCorreo(obj.Email, asunto,mensaje_correo);
                if (respuesta)
                {
                    obj.Clave = CN_RECURSOS.ConvertirSha256(clave);
                    return objCapaDato.RegistrarEmpleado(obj, out Mensaje);
                }
                else
                {
                    Mensaje = "No se puede enviar el correo";
                    return 0;
                }
                
            }
            else
            {
                return 0;
            }
        }

        public bool EditarEmpleados(Empleado obj, out string Mensaje)
        {
            //Se aplica las reglas del negocio
            Mensaje = string.Empty;
            if (string.IsNullOrEmpty(obj.Nombre) || string.IsNullOrWhiteSpace(obj.Nombre))
            {
                Mensaje = "El nombre del empleado no puede ser vacío";
            }
            else if (string.IsNullOrEmpty(obj.Apellidos) || string.IsNullOrWhiteSpace(obj.Apellidos))
            {
                Mensaje = "El apellido del empleado no puede ser vacío";
            }
            else if (string.IsNullOrEmpty(obj.Email) || string.IsNullOrWhiteSpace(obj.Email))
            {
                Mensaje = "El email del empleado no puede ser vacío";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {
                return objCapaDato.EditarEmpleado(obj, out Mensaje);
            }
            else
            {
                return false;
            }
        }

        public Empleado EditarEmpleadoRenovarSesion(Empleado obj, out string Mensaje)
        {
            //Se aplica las reglas del negocio
            Mensaje = string.Empty;
            if (string.IsNullOrEmpty(obj.Nombre) || string.IsNullOrWhiteSpace(obj.Nombre))
            {
                Mensaje = "El nombre del empleado no puede ser vacío";
            }
            else if (string.IsNullOrEmpty(obj.Apellidos) || string.IsNullOrWhiteSpace(obj.Apellidos))
            {
                Mensaje = "El apellido del empleado no puede ser vacío";
            }
            else if (string.IsNullOrEmpty(obj.Email) || string.IsNullOrWhiteSpace(obj.Email))
            {
                Mensaje = "El email del empleado no puede ser vacío";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {
                return objCapaDato.EditarEmpleadoRenovarSesion(obj, out Mensaje);
            }
            else
            {
                return null;
            }
        }
        


        public bool EliminarEmpleado(int id, out string Mensaje)
        {
            return objCapaDato.EliminarEmpleado(id, out Mensaje);
        }

    }
}
