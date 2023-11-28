using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Credito
    {
        public int IdEmpleado { get; set; }
        public int IdCredito { get; set; }
        public int IdSolicitud { get; set; }
        public string TipoMoneda { get; set; }
        public string FechaAprobacion { get; set; }
        public string FechaDesembolso { get; set; }
        public string TasaInteres { get; set; }
        public int? pGracia_Meses { get; set; }

    }
}
