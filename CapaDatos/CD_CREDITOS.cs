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
    public class CD_CREDITOS
    {

        public List<Credito> ListarCreditos(string query)
        {
            List<Credito> lista = new List<Credito>();
            try
            {

                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {

                    //string query = "select idempleado,idcredito, idsolicitud,tipomoneda,fechaaprobacion,fechadesembolso,tasainteres, pGracia_Meses from creditos";
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        string FechaA = null, FechaD = null;
                        while (rdr.Read())
                        {
                            if (rdr["fechadesembolso"] != DBNull.Value)
                            {
                                FechaD = ((DateTime)rdr["fechadesembolso"]).ToString("yyyy-MM-dd");
                            }
                            //Formatear fechas para los empleados y manejar nulos inicialmente creadosf
                            if (rdr["Fechaaprobacion"] != DBNull.Value)
                            {
                                FechaA = ((DateTime)rdr["Fechaaprobacion"]).ToString("yyyy-MM-dd");
                            }

                            lista.Add(
                                new Credito()
                                {
                                    IdEmpleado = Convert.ToInt32(rdr["IDempleado"]),
                                    IdCredito = Convert.ToInt32(rdr["idcredito"]),
                                    IdSolicitud = Convert.ToInt32(rdr["idsolicitud"]),
                                    TipoMoneda = rdr["tipomoneda"].ToString(),
                                    FechaAprobacion = FechaA,
                                    FechaDesembolso = FechaD,
                                    TasaInteres = rdr["tasainteres"].ToString(),
                                    pGracia_Meses = (rdr["pGracia_Meses"] != DBNull.Value) ? Convert.ToInt32(rdr["pGracia_Meses"]) : (int?)null,
                                    //FechaNacimiento = rdr.GetDateTime(rdr.GetOrdinal("FechaNac"))
                                    //Reestablecer = Convert.ToBoolean(rdr["Atribute"])
                                }

                                );
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                lista = new List<Credito>();
            }

            return lista;
        }

        public List<Credito> ListarCreditos()
        {
            string query = "select * from creditos";
            return ListarCreditos(query);
        }


        public List<Credito> ListarCreditosDeCliente(string id)
        {
            string query = "select * from CREDITOS c join SOLICITUD s on c.IDSOLICITUD=s.IDSOLICITUD where s.IDCLIENTE="+id;
            return ListarCreditos(query);
        }




        public void RegistrarCredito(Credito obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            try
            {

                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("CrearCredito", oconexion);
                    cmd.Parameters.AddWithValue("IdEmpleado", obj.IdEmpleado);
                    cmd.Parameters.AddWithValue("IdSolicitud", obj.IdSolicitud);
                    cmd.Parameters.AddWithValue("TipoMoneda", obj.TipoMoneda);
                    cmd.Parameters.AddWithValue("FechaAprobacion", obj.FechaAprobacion);
                    cmd.Parameters.AddWithValue("FechaDesembolso", obj.FechaDesembolso);
                    cmd.Parameters.AddWithValue("TasaInteres", obj.TasaInteres);
                    cmd.Parameters.AddWithValue("PlazoGraciaMeses", obj.pGracia_Meses);

                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    //id = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    Mensaje = "No hubo problemas en el registro.";
                }
            }
            catch (Exception ex)
            {
                Mensaje = ex.Message;
            }
            //return id;
        }




    }
}
