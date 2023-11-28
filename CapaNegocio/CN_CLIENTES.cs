using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_CLIENTES
    {
        public CD_CLIENTES objCapaDato = new CD_CLIENTES();

        public List<Cliente> ListarClientes()
        {
            return objCapaDato.ListarClientes();
        }




        public bool EditarClientes(Cliente obj, out string Mensaje)
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
            else if (string.IsNullOrEmpty(obj.Correo) || string.IsNullOrWhiteSpace(obj.Correo))
            {
                Mensaje = "El email del empleado no puede ser vacío";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {
                return objCapaDato.EditarCliente(obj, out Mensaje);
            }
            else
            {
                return false;
            }
        }

    }
}
