using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public  class Solicitud
    {
        public  int idSolicitud { get; set; }
        public  int idCliente { get; set; }
        public Cliente Cliente { get; set; }
        public  int? Seq { get; set; }
        public  int? idEmpleado { get; set; }
        public Empleado Empleado { get; set; }
        public  string TipoMoneda { get; set; }
        public  string Estado { get; set; }
        public  string Situacion { get; set; }
        public  string FechaSolicitud { get; set; }
        public  string FechaDesembolso { get; set; }
        public  float Monto { get; set; }
        public  int CantidadCuotas { get; set; }
        public  int? IngresosMensuales { get; set; }
        public  int? GastosMensuales  { get; set; }
        public  string EstadoCivil { get; set; }
        public  string Educacion { get; set; }
        public  int? PersonasDependientes { get; set; }
        public  string Empleador { get; set; }
        public  string PuestoTrabajo { get; set; }
        public  int?  Antiguedad { get; set; }
    }
}
