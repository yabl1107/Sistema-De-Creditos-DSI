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
    public  class CD_CLIENTES
    {

        public List<Cliente> ListarClientesConSolicitud()
        {
            string query = "select* from cliente c join SOLICITUD s on s.IDCLIENTE = c.IDCLIENTE where s.SITUACION = 'Activo'";
            return ListarClientesGeneral(query);
        }
        public List<Cliente> ListarClientes()
        {
            string query = "select * from cliente";
            return ListarClientesGeneral(query);
        }


        public List<Cliente> ListarClientesGeneral(string query)
        {
            List<Cliente> lista = new List<Cliente>();
            try
            {

                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {

                    //string query = "select idcliente, nombre, apellidos, dni, fechanac,ubigeonac, telf, email,direccion, ubigeovivienda, sexo from cliente";
                    //string query = "select * from cliente";
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    string fechaN = null;

                    oconexion.Open();


                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            if (rdr["fechanac"] != DBNull.Value)
                            {
                                fechaN = ((DateTime)rdr["FechaNac"]).ToString("yyyy-MM-dd");
                            }

                            lista.Add(
                                new Cliente()
                                {
                                    idCliente = Convert.ToInt32(rdr["idcliente"]),
                                    Nombre = rdr["nombre"].ToString(),
                                    Apellidos = rdr["apellidos"].ToString(),
                                    //DNI = Convert.ToInt32(rdr["dni"]),
                                    //DNI = Convert.ToInt32(rdr["DNI"] ?? 0),
                                    DNI = (rdr["DNI"] != DBNull.Value) ? Convert.ToInt32(rdr["DNI"]) : (int?)null,
                                    FechaNacimiento = fechaN,
                                    UbigeoNacimiento =    rdr["ubigeonac"].ToString(),
                                    Telefono =    rdr["telf"].ToString(),
                                    Correo = rdr["email"].ToString(),
                                    UbigeoVivienda = rdr["ubigeovivienda"].ToString(),
                                    Sexo = rdr["sexo"].ToString(),
                                    Clave = rdr["clave"].ToString(),

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
                lista = new List<Cliente>();
            }

            return lista;
        }



        public bool EditarCliente(Cliente obj, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {

                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("ActualizarCliente", oconexion);

                    cmd.Parameters.AddWithValue("IDCliente", obj.idCliente);
                    cmd.Parameters.AddWithValue("Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("Apellidos", obj.Apellidos);
                    cmd.Parameters.AddWithValue("DNI", obj.DNI);
                    cmd.Parameters.AddWithValue("Email", obj.Correo);
                    cmd.Parameters.AddWithValue("Telf", obj.Telefono);
                    cmd.Parameters.AddWithValue("Sexo", obj.Sexo);
                    cmd.Parameters.AddWithValue("FechaNac", obj.FechaNacimiento);
                    //cmd.Parameters.AddWithValue("FechaNacimiento", obj.FechaNacimiento);

                    cmd.Parameters.AddWithValue("UbigeoVivienda", obj.UbigeoVivienda);
                    cmd.Parameters.AddWithValue("UbigeoNac", obj.UbigeoNacimiento);



                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 200).Direction = ParameterDirection.Output;

                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }
            return resultado;
        }




    }
}
