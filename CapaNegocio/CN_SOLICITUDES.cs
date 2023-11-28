using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_SOLICITUDES
    {
        public CD_SOLICITUDES objCapaDato = new CD_SOLICITUDES();
        public List<Solicitud> ListarSolicitudes()
        {
                return objCapaDato.ListarSolicitudes();
        }

        public void RegistrarSolicitud(Solicitud obj, out string Mensaje)
        {
            int idCLiente = obj.idCliente;
            Cliente oCliente = null;
            oCliente = new CD_CLIENTES().ListarClientesConSolicitud().Where(item => item.idCliente == idCLiente).FirstOrDefault();
            if (oCliente == null)
            {
                objCapaDato.RegistrarSolicitud(obj, out Mensaje);
            }
            else
            {
                Mensaje = "ERROR";
            }

        }


        public List<Solicitud> ListarSolicitudesPendientes()
        {
            return objCapaDato.ListarSolicitudesPendientes();
        }
        public List<Solicitud> ListarSolicitudesAtendidas()
        {
            return objCapaDato.ListarSolicitudesAtendidas();
        }

        public List<Solicitud> ListarSolicitudesDeUnTrabajador(string id)
        {
            return objCapaDato.ListarSolicitudesDeUnTrabajador(id);
        }

        public List<Solicitud> ListarSolicitudesDeUnCliente(string id)
        {
            //string query = "SELECT * FROM solicitud LEFT JOIN empleado ON solicitud.IDEMPLEADO = empleado.IDEMPLEADO;";
            return objCapaDato.ListarSolicitudesDeUnCliente(id);
        }

        public void ModificarEstadoSolicitud(int idSolicitud, int idEmpleado, string NuevoEstado, out string Mensaje)
        {
           objCapaDato.ModificarEstadoSolicitud(idSolicitud,idEmpleado, NuevoEstado, out Mensaje);
        }

        //oEmpleado = new CN_EMPLEADOS().ListarEmpleados().Where(item => item.Email == correo && item.Clave == contraEncriptada ).FirstOrDefault();
    }
}
