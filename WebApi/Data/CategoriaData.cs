using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApi.Models;

namespace WebApi.Data
{
    public class CategoriaData
    {
        public static bool Registrar(Categoria oCategoria)
        {
            //{"IdCategoria":0,"Descripcion":"Ciencia","Estado":true}
            using (SqlConnection oConexion = new SqlConnection(Conexion.rutaConexion))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarCategoria", oConexion);
                    cmd.Parameters.AddWithValue("@Descripcion", oCategoria.Descripcion);
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

        public static bool Modificar(Categoria oCategoria)
        {
            using (SqlConnection oConexion = new SqlConnection(Conexion.rutaConexion))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_ModificarCategoria", oConexion);
                    cmd.Parameters.AddWithValue("@IdCategoria", oCategoria.IdCategoria);
                    cmd.Parameters.AddWithValue("@Descripcion", oCategoria.Descripcion);
                    cmd.Parameters.AddWithValue("@Estado", oCategoria.Estado);
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

        public static List<Categoria> Listar()
        {
            List<Categoria> oListaCategoria = new List<Categoria>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.rutaConexion))
            {
                SqlCommand cmd = new SqlCommand("select IdCategoria, Descripcion, Estado from categoria", oConexion);
                cmd.CommandType = CommandType.Text;
                try
                {
                    oConexion.Open();
                    cmd.ExecuteNonQuery();

                    
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oListaCategoria.Add(new Categoria()
                            {
                                IdCategoria = Convert.ToInt32(dr["IdCategoria"]),
                                Descripcion = dr["Descripcion"].ToString(),
                                Estado = Convert.ToBoolean(dr["Estado"])
                            });
                        }
                    }

                    return oListaCategoria;
                }
                catch (Exception ex)
                {
                    return oListaCategoria;
                }
            }
        }


        public static Categoria Obtener(int id)
        {
            Categoria oCategoria = new Categoria();
            using (SqlConnection oConexion = new SqlConnection(Conexion.rutaConexion))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("select IdCategoria, Descripcion, Estado from categoria where IdCategoria = @id", oConexion);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oCategoria = new Categoria()
                            {
                                IdCategoria = Convert.ToInt32(dr["IdCategoria"]),
                                Descripcion = dr["Descripcion"].ToString(),
                                Estado = Convert.ToBoolean(dr["Estado"])
                            };
                        }
                    }

                    return oCategoria;
                }
                catch (Exception ex)
                {
                    // Log the exception
                    Console.WriteLine($"Error al obtener la categoría: {ex.Message}");
                    return oCategoria;
                }
            }
        }

        public static bool Eliminar(int id)
        {
            using (SqlConnection oConexion = new SqlConnection(Conexion.rutaConexion))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("delete from CATEGORIA where idcategoria = @id", oConexion);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    return true;
                }
                catch (Exception ex)
                {
                    // Log the exception
                    Console.WriteLine($"Error al eliminar la categoría: {ex.Message}");
                    return false;
                }
            }
        }
    }

}