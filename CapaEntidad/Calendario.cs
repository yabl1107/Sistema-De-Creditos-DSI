using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Calendario
    {
        public int idCalendario {  get; set; }
        public int Registroid { get; set; }
        public int NroCuota { get; set; }
        public string Fecha { get; set; }
        public float SaldoInicial { get; set; }
        public float MontoCuota { get; set; }
        public float InteresCompensatorio { get; set; }
        public float InteresMoratorio { get; set; }
        public float ComisionesAdicionales { get; set; }
        public float GastosAdministrativos { get; set; }
        public float Amortizacion {  get; set; }
        public float Capital {  get; set; }
        public float Interes {  get; set; }
        public float SaldoFinal {  get; set; }
        public string Estado {  get; set; }
        public string FechaEstablecida { get; set; }
        public string FechaCancelacion {  get; set; }
        public RegistroCalendario RegistroCalendario { get; set; }

    }
}
