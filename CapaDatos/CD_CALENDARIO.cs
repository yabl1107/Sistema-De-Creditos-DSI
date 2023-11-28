using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public  class CD_CALENDARIO
    {

        public List<Calendario> ListarCalendario(string regId)
        {
            List<Calendario> lista = new List<Calendario>();
            try
            {
                //string query = "SELECT * FROM calendario";
                string query = "select * from REGISTROCALENDARIO r join calendario c on r.REGISTROID = c.REGISTROID where estado=1 and IDCREDITO="+regId;
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        string fechaDB = null;

                        while (rdr.Read())
                        {

                            //Formatear fechas para los empleados y manejar nulos inicialmente creadosf
                            if (rdr["fecha"] != DBNull.Value)
                            {
                                fechaDB = ((DateTime)rdr["fecha"]).ToString("yyyy-MM-dd");
                            }

                            lista.Add(
                                new Calendario()
                                {
                                    idCalendario = Convert.ToInt32(rdr["IDCALENDARIO"]),
                                    Registroid = Convert.ToInt32(rdr["REGISTROID"]),
                                    NroCuota = Convert.ToInt32(rdr["NROCUOTA"]),
                                    Fecha = fechaDB,
                                    MontoCuota = Convert.ToInt32(rdr["MONTOCUOTA"]),
                                    Interes = Convert.ToSingle(rdr["INTERES"]),
                                    Capital = Convert.ToSingle(rdr["CAPITAL"])
                        }
                             );
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                lista = new List<Calendario>();
            }

            return lista;
        }




    }
}
