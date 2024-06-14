using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApi.Models;

namespace WebApi.Data
{
    public class TipoPersonaData
    {
        public static List<TipoPersona> Listar()
        {
            List<TipoPersona> Lista = new List<TipoPersona>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.rutaConexion))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("select IdTipoPersona, Descripcion from TIPO_PERSONA", oConexion);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Lista.Add(new TipoPersona()
                            {
                                IdTipoPersona = Convert.ToInt32(dr["IdTipoPersona"]),
                                Descripcion = dr["Descripcion"].ToString()
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log the exception
                    Console.WriteLine($"Error al listar los tipos de persona: {ex.Message}");
                    Lista = new List<TipoPersona>();
                }
            }
            return Lista;
        }

        public static TipoPersona Obtener(int id)
        {
            TipoPersona oTipoPersona = new TipoPersona();
            using (SqlConnection oConexion = new SqlConnection(Conexion.rutaConexion))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("select IdTipoPersona, Descripcion from TIPO_PERSONA where IdTipoPersona = @id", oConexion);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oTipoPersona = new TipoPersona()
                            {
                                IdTipoPersona = Convert.ToInt32(dr["IdTipoPersona"]),
                                Descripcion = dr["Descripcion"].ToString()
                            };
                        }
                    }
                    return oTipoPersona;
                }
                catch (Exception ex)
                {
                    // Log the exception
                    Console.WriteLine($"Error al obtener el tipo de persona: {ex.Message}");
                    return oTipoPersona;
                }
            }
        }

        public static bool Registrar(TipoPersona oTipoPersona)
        {
            using (SqlConnection oConexion = new SqlConnection(Conexion.rutaConexion))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarTipoPersona", oConexion);
                    cmd.Parameters.AddWithValue("@Descripcion", oTipoPersona.Descripcion);
                    cmd.Parameters.Add("@Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();
                    cmd.ExecuteNonQuery();

                    return Convert.ToBoolean(cmd.Parameters["@Resultado"].Value);
                }
                catch (Exception ex)
                {
                    // Log the exception
                    Console.WriteLine($"Error al registrar el tipo de persona: {ex.Message}");
                    return false;
                }
            }
        }

        public static bool Modificar(TipoPersona oTipoPersona)
        {
            using (SqlConnection oConexion = new SqlConnection(Conexion.rutaConexion))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_ModificarTipoPersona", oConexion);
                    cmd.Parameters.AddWithValue("@IdTipoPersona", oTipoPersona.IdTipoPersona);
                    cmd.Parameters.AddWithValue("@Descripcion", oTipoPersona.Descripcion);
                    cmd.Parameters.Add("@Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();
                    cmd.ExecuteNonQuery();

                    return Convert.ToBoolean(cmd.Parameters["@Resultado"].Value);
                }
                catch (Exception ex)
                {
                    // Log the exception
                    Console.WriteLine($"Error al modificar el tipo de persona: {ex.Message}");
                    return false;
                }
            }
        }

        public static bool Eliminar(int id)
        {
            using (SqlConnection oConexion = new SqlConnection(Conexion.rutaConexion))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("delete from TIPO_PERSONA where IdTipoPersona = @id", oConexion);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();
                    cmd.ExecuteNonQuery();

                    return true;
                }
                catch (Exception ex)
                {
                    // Log the exception
                    Console.WriteLine($"Error al eliminar el tipo de persona: {ex.Message}");
                    return false;
                }
            }
        }
    }

}