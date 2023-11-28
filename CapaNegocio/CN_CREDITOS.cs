using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_CREDITOS
    {
        public CD_CREDITOS objCapaDato = new CD_CREDITOS();

        public List<Credito> ListarCreditos()
        {
            return objCapaDato.ListarCreditos();
        }



        public void RegistrarCredito(Credito obj, out string Mensaje)
        {
            //Se aplica las reglas del negocio
            Mensaje = string.Empty;
            if (obj.TasaInteres == null)
            {
                Mensaje = "Debe indicar tasa interes";
                return;
            }
            else if(obj.pGracia_Meses==null){
                Mensaje = "Debe indicar periodo de gracia ";
                return;
            }
            objCapaDato.RegistrarCredito(obj, out Mensaje);
        }


        public List<Credito> ListarCreditosDeCliente(string id)
        {
            return objCapaDato.ListarCreditosDeCliente(id);
        }

    }
}
