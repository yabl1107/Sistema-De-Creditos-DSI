using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Cliente
    {
        public int idCliente { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public int? DNI {  get; set; }
        public string FechaNacimiento {  get; set; }
        public string UbigeoNacimiento{  get; set; }
        public string Telefono{  get; set; }
        public string Correo {  get; set; }
        public string UbigeoVivienda {  get; set; }
        public string Sexo {  get; set; }
        public Solicitud solicitudes { get; set; }
        public string Clave {  get; set; }
        public string ConfirmarClave {  get; set; }
    }
}
