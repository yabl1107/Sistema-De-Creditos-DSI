using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public  class Empleado
    {

        public int IDempleado{ get; set; }
        public string Sexo { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public int DNI{ get; set; }
        public string Email{ get; set; }
        public int Telefono{ get; set; }
        public string FechaNacimiento { get; set; }
        public string FechaContratacion { get; set; }
        public string Clave {  get; set; }  
        public int? Salario {  get; set; }  
        public string Ubigeo {  get; set; }  
        public string NombrePuesto {  get; set; }  
        public string Gerencia {  get; set; }  

    }
}
