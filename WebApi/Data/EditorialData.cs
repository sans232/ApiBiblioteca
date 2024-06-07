using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApi.Models;

namespace WebApi.Data
{
    public class EditorialData
    {
        public static bool Registrar(Editorial oEditorial)
        {
            //{"IdCategoria":0,"Descripcion":"Ciencia","Estado":true}
            using (SqlConnection oConexion = new SqlConnection(Conexion.rutaConexion))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarEditorial", oConexion);
                    cmd.Parameters.AddWithValue("@Descripcion", oEditorial.Descripcion);
                    cmd.Parameters.Add("@Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    return Convert.ToBoolean(cmd.Parameters["@Resultado"].Value);

                }
                catch (Exception ex)
                {
                    // Log the exception
                    Console.WriteLine($"Error al registrar la editorial: {ex.Message}");
                    return false;
                }
            }
        }

        public static bool Modificar(Editorial oEditorial)
        {
            using (SqlConnection oConexion = new SqlConnection(Conexion.rutaConexion))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_ModificarEditorial", oConexion);
                    cmd.Parameters.AddWithValue("@IdEditorial", oEditorial.IdEditorial);
                    cmd.Parameters.AddWithValue("@Descripcion", oEditorial.Descripcion);
                    cmd.Parameters.AddWithValue("@Estado", oEditorial.Estado);
                    cmd.Parameters.Add("@Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;

                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    return Convert.ToBoolean(cmd.Parameters["@Resultado"].Value);
                }
                catch (Exception ex)
                {
                    // Log the exception
                    Console.WriteLine($"Error al modificar la editorial: {ex.Message}");
                    return false;
                }
            }
        }

        public static List<Editorial> Listar()
        {
            List<Editorial> oListaEditorial = new List<Editorial>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.rutaConexion))
            {
                SqlCommand cmd = new SqlCommand("select IdEditorial, Descripcion, Estado from editorial", oConexion);
                cmd.CommandType = CommandType.Text;
                try
                {
                    oConexion.Open();
                    cmd.ExecuteNonQuery();


                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oListaEditorial.Add(new Editorial()
                            {
                                IdEditorial = Convert.ToInt32(dr["IdEditorial"]),
                                Descripcion = dr["Descripcion"].ToString(),
                                Estado = Convert.ToBoolean(dr["Estado"])
                            });
                        }
                    }

                    return oListaEditorial;
                }
                catch (Exception ex)
                {
                    return oListaEditorial;
                }
            }
        }


        public static Editorial Obtener(int id)
        {
            Editorial oEditorial = new Editorial();
            using (SqlConnection oConexion = new SqlConnection(Conexion.rutaConexion))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("select IdEditorial, Descripcion, Estado from editorial where IdEditorial = @id", oConexion);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oEditorial = new Editorial()
                            {
                                IdEditorial = Convert.ToInt32(dr["IdEditorial"]),
                                Descripcion = dr["Descripcion"].ToString(),
                                Estado = Convert.ToBoolean(dr["Estado"])
                            };
                        }
                    }

                    return oEditorial;
                }
                catch (Exception ex)
                {
                    // Log the exception
                    Console.WriteLine($"Error al obtener la categoría: {ex.Message}");
                    return oEditorial;
                }
            }
        }

        public static bool Eliminar(int id)
        {
            using (SqlConnection oConexion = new SqlConnection(Conexion.rutaConexion))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("delete from EDITORIAL where IdEditorial = @id", oConexion);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    return true;
                }
                catch (Exception ex)
                {
                    // Log the exception
                    Console.WriteLine($"Error al eliminar la editorial: {ex.Message}");
                    return false;
                }
            }
        }
    }

}