using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace CapaDatos
{


    public class CD_SOLICITUDES
    {
        public List<Solicitud> ListarSolicitudesGeneral(string query)
        {
            List<Solicitud> lista = new List<Solicitud>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {

                    //string query = "select idsolicitud, idcliente, idempleado, tipomoneda, estado, situacion, monto, cantidadcuotas, fechasolicitud from solicitud";
                    //query = "SELECT * FROM solicitud LEFT JOIN empleado ON solicitud.IDEMPLEADO = empleado.IDEMPLEADO;";
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        string fechaSol = null, fechaDes = null;
                        string fechaN = null, fechaC = null;
                        Empleado Empleado1 = new Empleado();
                        while (rdr.Read())
                        {

                            //Formatear fechas para los empleados y manejar nulos inicialmente creadosf
                            if (rdr["fechasolicitud"] != DBNull.Value)
                            {
                                fechaSol = ((DateTime)rdr["fechasolicitud"]).ToString("yyyy-MM-dd");
                            }
                            //Formatear fechas para los empleados y manejar nulos inicialmente creadosf
                            if (rdr["FechaContratacion"] != DBNull.Value)
                            {
                                fechaC = ((DateTime)rdr["FechaContratacion"]).ToString("yyyy-MM-dd");
                            }
                            if (rdr["FechaNac"] != DBNull.Value)
                            {
                                fechaN = ((DateTime)rdr["FechaNac"]).ToString("yyyy-MM-dd");
                            }

                            if (rdr["idempleado"] != DBNull.Value)
                            {
                                Empleado1 = new Empleado()
                                {
                                    IDempleado = Convert.ToInt32(rdr["IDempleado"]),
                                    Sexo = rdr["Sexo"].ToString(),
                                    Nombre = rdr["Nombre"].ToString(),
                                    Apellidos = rdr["Apellidos"].ToString(),
                                    DNI = Convert.ToInt32(rdr["DNI"]),
                                    Email = rdr["Email"].ToString(),
                                    Telefono = Convert.ToInt32(rdr["Telf"]),
                                    Clave = rdr["Clave"].ToString(),
                                    Salario = (rdr["Salario"] != DBNull.Value) ? Convert.ToInt32(rdr["Salario"]) : (int?)null,
                                    Ubigeo = rdr["DirUbigeo"].ToString(),
                                    FechaContratacion = fechaC,
                                    FechaNacimiento = fechaN,
                                    Gerencia = rdr["Gerencia"].ToString(),
                                    NombrePuesto = rdr["NombrePuesto"].ToString(),
                                };
                            }
                            //Crear nuevo objeto empleado y cliente 


                            lista.Add(
                                new Solicitud()
                                {
                                    idSolicitud = Convert.ToInt32(rdr["IDSOLICITUD"]),
                                    idCliente = Convert.ToInt32(rdr["IDCLIENTE"]),
                                    idEmpleado = (rdr["idempleado"] != DBNull.Value) ? Convert.ToInt32(rdr["idempleado"]) : (int?)null,
                                    TipoMoneda = rdr["TIPOMONEDA"].ToString(),
                                    Estado = rdr["ESTADO"].ToString(),
                                    Situacion = rdr["SITUACION"].ToString(),
                                    Monto = Convert.ToSingle(rdr["MONTO"]),
                                    CantidadCuotas = Convert.ToInt32(rdr["CANTIDADCUOTAS"]),
                                    FechaSolicitud = fechaSol,
                                    FechaDesembolso = fechaDes,
                                    Seq = (rdr["seq"] != DBNull.Value) ? Convert.ToInt32(rdr["seq"]) : (int?)null,
                                    IngresosMensuales = (rdr["ingresosmens"] != DBNull.Value) ? Convert.ToInt32(rdr["ingresosmens"]) : (int?)null,
                                    GastosMensuales = (rdr["gastosmens"] != DBNull.Value) ? Convert.ToInt32(rdr["gastosmens"]) : (int?)null,
                                    EstadoCivil = (rdr["estadocivil"] != DBNull.Value) ? Convert.ToString(rdr["estadocivil"]) : null,
                                    Educacion = (rdr["educacion"] != DBNull.Value) ? Convert.ToString(rdr["educacion"]) : null,
                                    PersonasDependientes = (rdr["personasdep"] != DBNull.Value) ? Convert.ToInt32(rdr["personasdep"]) : (int?)null,
                                    Empleador = (rdr["empleador"] != DBNull.Value) ? Convert.ToString(rdr["empleador"]) : null,
                                    PuestoTrabajo = (rdr["puestotrabajo"] != DBNull.Value) ? Convert.ToString(rdr["puestotrabajo"]) : null,
                                    Antiguedad = (rdr["antiguedad"] != DBNull.Value) ? Convert.ToInt32(rdr["antiguedad"]) : (int?)null,
                                    Empleado = Empleado1,
                                }

                                );
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                lista = new List<Solicitud>();
            }

            return lista;
        }





        public void RegistrarSolicitud(Solicitud obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            try
            {

                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("InsertarSolicitud", oconexion);
                    cmd.Parameters.AddWithValue("IDCliente", obj.idCliente);
                    cmd.Parameters.AddWithValue("TipoMoneda", obj.TipoMoneda);
                    cmd.Parameters.AddWithValue("FechaSolicitud", obj.FechaSolicitud);
                    cmd.Parameters.AddWithValue("Monto", obj.Monto);
                    cmd.Parameters.AddWithValue("CantidadCuotas", obj.CantidadCuotas);
                    cmd.Parameters.AddWithValue("IngresosMens", obj.IngresosMensuales);
                    cmd.Parameters.AddWithValue("GastosMens", obj.GastosMensuales);
                    cmd.Parameters.AddWithValue("EstadoCivil", obj.EstadoCivil);
                    cmd.Parameters.AddWithValue("Educacion", obj.Educacion);
                    cmd.Parameters.AddWithValue("PersonasDep", obj.PersonasDependientes);
                    cmd.Parameters.AddWithValue("Empleador", obj.Empleador);
                    cmd.Parameters.AddWithValue("PuestoTrabajo", obj.PuestoTrabajo);
                    cmd.Parameters.AddWithValue("Antiguedad", obj.Antiguedad);


                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    Mensaje = "Registro exitoso";
                }
            }
            catch (Exception ex)
            {
                Mensaje = ex.Message;
            }
        }




        public List<Solicitud> ListarSolicitudes()
        {
            string query = "SELECT * FROM solicitud LEFT JOIN empleado ON solicitud.IDEMPLEADO = empleado.IDEMPLEADO;";
            return ListarSolicitudesGeneral(query);
        }

        public List<Solicitud> ListarSolicitudesPendientes()
        {
            //string query = "select * from SOLICITUD where IDEMPLEADO is null;";
            string query = "SELECT * FROM solicitud LEFT JOIN empleado ON solicitud.IDEMPLEADO = empleado.IDEMPLEADO where SOLICITUD.IDEMPLEADO IS NULL;";
            return ListarSolicitudesGeneral(query);
        }
        public List<Solicitud> ListarSolicitudesAtendidas()
        {
            //string query = "SELECT * FROM solicitud LEFT JOIN empleado ON solicitud.IDEMPLEADO = empleado.IDEMPLEADO;";
            string query = "SELECT * FROM solicitud JOIN empleado ON solicitud.IDEMPLEADO = empleado.IDEMPLEADO;";
            return ListarSolicitudesGeneral(query);
        }

        public List<Solicitud> ListarSolicitudesDeUnTrabajador(string id)
        {
            //string query = "SELECT * FROM solicitud LEFT JOIN empleado ON solicitud.IDEMPLEADO = empleado.IDEMPLEADO;";
            string query = "SELECT * FROM solicitud JOIN empleado ON solicitud.IDEMPLEADO = empleado.IDEMPLEADO where SOLICITUD.IDEMPLEADO=" + id;
            return ListarSolicitudesGeneral(query);
        }

        public List<Solicitud> ListarSolicitudesDeUnCliente(string id)
        {
            //string query = "SELECT * FROM solicitud LEFT JOIN empleado ON solicitud.IDEMPLEADO = empleado.IDEMPLEADO;";
            string query = "SELECT * FROM solicitud LEFT JOIN empleado ON solicitud.IDEMPLEADO = empleado.IDEMPLEADO where IDCLIENTE="+id;
            return ListarSolicitudesGeneral(query);
        }


        public void ModificarEstadoSolicitud(int idSolicitud,int idEmpleado, string NuevoEstado, out string Mensaje)
        {

            Mensaje = string.Empty;
            try
            {

                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("ModificarEstadoSolicitud", oconexion);
                    cmd.Parameters.AddWithValue("idSolicitud", idSolicitud);
                    cmd.Parameters.AddWithValue("idEmpleado", idEmpleado);
                    cmd.Parameters.AddWithValue("nuevoEstado", NuevoEstado);

                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();
                    Mensaje = "No hubo problemas";

                }
            }
            catch (Exception ex)
            {
                Mensaje = ex.Message;
            }
        }

    }
}
