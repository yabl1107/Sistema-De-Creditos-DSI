using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Reflection;


namespace CapaDatos
{
    public class CD_EMPLEADOS
    {
        public List<Empleado> ListarEmpleados()
        {
            List<Empleado> lista = new List<Empleado>();
            try
            {

                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {

                    //string query = "select IDempleado, Sexo, Nombre, Apellidos, DNI, Email, Telf, FechaNac from empleado";
                    string query = "select * from empleado";
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        string fechaN=null, fechaC=null;
                        
                        while (rdr.Read())
                        {
                            //Formatear fechas para los empleados y manejar nulos inicialmente creadosf
                            if(rdr["FechaContratacion"] != DBNull.Value)
                            {
                                fechaC = ((DateTime)rdr["FechaContratacion"]).ToString("yyyy-MM-dd");
                            }
                            if (rdr["FechaNac"] != DBNull.Value)
                            {
                                fechaN = ((DateTime)rdr["FechaNac"]).ToString("yyyy-MM-dd");
                            }

                            //DateTime fechaPruebis = (rdr["FechaContratacion"]!=DBNull.Value)? (DateTime) rdr["FechaContratacion"]: new DateTime(1900, 1, 1);
                            //string FechaReal = fechaPruebis.ToString("dd/MM/yyyy");
                            //FechaSolicitud = (rdr["fechasolicitud"] != DBNull.Value) ? rdr["fechasolicitud"].ToString() : null,

                            lista.Add(
                                new Empleado()
                                {
                                    IDempleado = Convert.ToInt32(rdr["IDempleado"]),
                                    Sexo = rdr["Sexo"].ToString(),
                                    Nombre = rdr["Nombre"].ToString(),
                                    Apellidos = rdr["Apellidos"].ToString(),
                                    DNI = Convert.ToInt32(rdr["DNI"]),
                                    Email = rdr["Email"].ToString(),
                                    Telefono = Convert.ToInt32(rdr["Telf"]),
                                    Clave = rdr["Clave"].ToString(),
                                    //Salario = Convert.ToInt32(rdr["Salario"]),
                                    Salario = (rdr["Salario"] != DBNull.Value) ? Convert.ToInt32(rdr["Salario"]) : (int?)null,
                                    Ubigeo = rdr["DirUbigeo"].ToString(),
                                    FechaContratacion = fechaC,
                                    //FechaContratacion = ((DateTime)rdr["FechaContratacion"]).ToString("dd/MM/yyyy"),
                                    //FechaNacimiento = ((DateTime)rdr["FechaNac"]).ToString("dd/MM/yyyy"),
                                    FechaNacimiento = fechaN,
                                    Gerencia = rdr["Gerencia"].ToString(),
                                    //Gerencia = (rdr["Gerencia"] != DBNull.Value) ? Convert.ToBoolean(rdr["Gerencia"]) : (bool?)null,
                                    NombrePuesto = rdr["NombrePuesto"].ToString(),

                                    //FechaNacimiento = rdr.GetDateTime(rdr.GetOrdinal("FechaNac"))
                                    //Reestablecer = Convert.ToBoolean(rdr["Atribute"])
                                }

                                ) ;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                lista = new List<Empleado>();
            }

            return lista;
        }



        public int RegistrarEmpleado(Empleado obj, out string Mensaje)
        {
            int id = 0;
            Mensaje = string.Empty;

            try
            {

                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("RegistrarEmpleado", oconexion);
                    cmd.Parameters.AddWithValue("idEmpleado", obj.IDempleado);
                    cmd.Parameters.AddWithValue("Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("Apellidos", obj.Apellidos);
                    cmd.Parameters.AddWithValue("DNI", obj.DNI);
                    cmd.Parameters.AddWithValue("Email", obj.Email);
                    cmd.Parameters.AddWithValue("Telefono", obj.Telefono);
                    cmd.Parameters.AddWithValue("Sexo", obj.Sexo);
                    cmd.Parameters.AddWithValue("FechaNacimiento", obj.FechaNacimiento);
                    cmd.Parameters.AddWithValue("Clave", obj.Clave);
                    cmd.Parameters.AddWithValue("Salario", obj.Salario);
                    cmd.Parameters.AddWithValue("FechaContratacion", obj.FechaContratacion);
                    cmd.Parameters.AddWithValue("NombrePuesto", obj.NombrePuesto);
                    cmd.Parameters.AddWithValue("Gerencia", obj.Gerencia);
                    cmd.Parameters.AddWithValue("DireccionUbigeo", obj.Ubigeo);
                    
                    cmd.Parameters.Add("Resultado",SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje",SqlDbType.VarChar,200).Direction = ParameterDirection.Output;

                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    id = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                id = 0;
                Mensaje = ex.Message;
            }
            return id;
        }


        public bool EliminarEmpleado(int id, out string Mensaje)
        {
            bool resultado = false;

            Mensaje = string.Empty;

            try
            {

                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("delete top (1) from empleado where IdEmpleado = @id", oconexion);
                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    resultado = cmd.ExecuteNonQuery() > 0? true:false;
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }
            return resultado;
        }


        public bool EditarEmpleado(Empleado obj, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {

                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("EditarEmpleado", oconexion);
                    
                    cmd.Parameters.AddWithValue("idEmpleado", obj.IDempleado);
                    cmd.Parameters.AddWithValue("Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("Apellidos", obj.Apellidos);
                    cmd.Parameters.AddWithValue("DNI", obj.DNI);
                    cmd.Parameters.AddWithValue("Email", obj.Email);
                    cmd.Parameters.AddWithValue("Telefono", obj.Telefono);
                    cmd.Parameters.AddWithValue("Sexo", obj.Sexo);
                    //cmd.Parameters.AddWithValue("FechaNacimiento", obj.FechaNacimiento);
                    cmd.Parameters.AddWithValue("Salario", obj.Salario);
                    cmd.Parameters.AddWithValue("FechaContratacion", obj.FechaContratacion);
                    cmd.Parameters.AddWithValue("NombrePuesto", obj.NombrePuesto);
                    cmd.Parameters.AddWithValue("Gerencia", obj.Gerencia);
                    cmd.Parameters.AddWithValue("DireccionUbigeo", obj.Ubigeo);


                    //cmd.Parameters.AddWithValue("FechaNacimiento", DBNull.Value);
                    if (obj.FechaNacimiento == null){ cmd.Parameters.AddWithValue("FechaNacimiento",  DBNull.Value); }
                    else { cmd.Parameters.AddWithValue("FechaNacimiento", obj.FechaNacimiento); }


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


        public Empleado EditarEmpleadoRenovarSesion(Empleado obj, out string Mensaje)
        {
            Empleado resultado = null;
            Mensaje = string.Empty;
            try
            {

                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("EditarEmpleado", oconexion);

                    cmd.Parameters.AddWithValue("idEmpleado", obj.IDempleado);
                    cmd.Parameters.AddWithValue("Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("Apellidos", obj.Apellidos);
                    cmd.Parameters.AddWithValue("DNI", obj.DNI);
                    cmd.Parameters.AddWithValue("Email", obj.Email);
                    cmd.Parameters.AddWithValue("Telefono", obj.Telefono);
                    cmd.Parameters.AddWithValue("Sexo", obj.Sexo);
                    //cmd.Parameters.AddWithValue("FechaNacimiento", obj.FechaNacimiento);
                    cmd.Parameters.AddWithValue("Salario", obj.Salario);
                    cmd.Parameters.AddWithValue("FechaContratacion", obj.FechaContratacion);
                    cmd.Parameters.AddWithValue("NombrePuesto", obj.NombrePuesto);
                    cmd.Parameters.AddWithValue("Gerencia", obj.Gerencia);
                    cmd.Parameters.AddWithValue("DireccionUbigeo", obj.Ubigeo);


                    //cmd.Parameters.AddWithValue("FechaNacimiento", DBNull.Value);
                    if (obj.FechaNacimiento == null) { cmd.Parameters.AddWithValue("FechaNacimiento", DBNull.Value); }
                    else { cmd.Parameters.AddWithValue("FechaNacimiento", obj.FechaNacimiento); }


                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 200).Direction = ParameterDirection.Output;

                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    resultado = obj;
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                resultado = null;
                Mensaje = ex.Message;
            }
            return resultado;
        }



    }

}
