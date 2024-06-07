using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApi.Models;

namespace WebApi.Data
{
    public class AutorData
    {
        public static bool Registrar(Autor oAutor)
        {
            //{"IdCategoria":0,"Descripcion":"Ciencia","Estado":true}
            using (SqlConnection oConexion = new SqlConnection(Conexion.rutaConexion))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarAutor", oConexion);
                    cmd.Parameters.AddWithValue("@Descripcion", oAutor.Descripcion);
                    cmd.Parameters.Add("@Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    return Convert.ToBoolean(cmd.Parameters["@Resultado"].Value);

                }
                catch (Exception ex)
                {
                    // Log the exception
                    Console.WriteLine($"Error al registrar la categoría: {ex.Message}");
                    return false;
                }
            }
        }

        public static bool Modificar(Autor oAutor)
        {
            using (SqlConnection oConexion = new SqlConnection(Conexion.rutaConexion))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_ModificarAutor", oConexion);
                    cmd.Parameters.AddWithValue("@IdAutor", oAutor.IdAutor);
                    cmd.Parameters.AddWithValue("@Descripcion", oAutor.Descripcion);
                    cmd.Parameters.AddWithValue("@Estado", oAutor.Estado);
                    cmd.Parameters.Add("@Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;

                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    return Convert.ToBoolean(cmd.Parameters["@Resultado"].Value);
                }
                catch (Exception ex)
                {
                    // Log the exception
                    Console.WriteLine($"Error al modificar la categoría: {ex.Message}");
                    return false;
                }
            }
        }

        public static List<Autor> Listar()
        {
            List<Autor> oListaAutor = new List<Autor>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.rutaConexion))
            {
                SqlCommand cmd = new SqlCommand("select IdAutor, Descripcion, Estado from autor", oConexion);
                cmd.CommandType = CommandType.Text;
                try
                {
                    oConexion.Open();
                    cmd.ExecuteNonQuery();


                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oListaAutor.Add(new Autor()
                            {
                                IdAutor = Convert.ToInt32(dr["IdAutor"]),
                                Descripcion = dr["Descripcion"].ToString(),
                                Estado = Convert.ToBoolean(dr["Estado"])
                            });
                        }
                    }

                    return oListaAutor;
                }
                catch (Exception ex)
                {
                    return oListaAutor;
                }
            }
        }


        public static Autor Obtener(int id)
        {
            Autor oAutor = new Autor();
            using (SqlConnection oConexion = new SqlConnection(Conexion.rutaConexion))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("select IdAutor, Descripcion, Estado from autor where IdAutor = @id", oConexion);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oAutor = new Autor()
                            {
                                IdAutor = Convert.ToInt32(dr["IdAutor"]),
                                Descripcion = dr["Autor"].ToString(),
                                Estado = Convert.ToBoolean(dr["Estado"])
                            };
                        }
                    }

                    return oAutor;
                }
                catch (Exception ex)
                {
                    // Log the exception
                    Console.WriteLine($"Error al obtener la categoría: {ex.Message}");
                    return oAutor;
                }
            }
        }

        public static bool Eliminar(int id)
        {
            using (SqlConnection oConexion = new SqlConnection(Conexion.rutaConexion))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("delete from autor where IdAutor = @id", oConexion);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    return true;
                }
                catch (Exception ex)
                {
                    // Log the exception
                    Console.WriteLine($"Error al eliminar el autor: {ex.Message}");
                    return false;
                }
            }
        }
    }

}