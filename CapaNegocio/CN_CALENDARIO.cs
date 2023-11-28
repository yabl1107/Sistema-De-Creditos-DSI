using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_CALENDARIO
    {
        public CD_CALENDARIO objCapaDato = new CD_CALENDARIO();

        public List<Calendario> ListarCalendario(string regId)
        {
            return objCapaDato.ListarCalendario(regId);
        }


    }
}
