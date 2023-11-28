using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class RegistroCalendario
    {
       public int RegistroId { get; set; }
        public int IDcredito { get; set; } //Comentar despues
        public int SeqHistorial { get; set; }
        public int Estado { get; set; }

        public Credito Credito { get; set; }
    }
}
