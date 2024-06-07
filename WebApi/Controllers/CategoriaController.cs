using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class CategoriaController : ApiController
    {
        // GET api/<controller>
        public List<Categoria> Get()
        {
            return CategoriaData.Listar();
        }

        // GET api/<controller>/5
        public Categoria Get(int id)
        {
            return CategoriaData.Obtener(id);
        }

        public bool Post([FromBody] Categoria oCategoria)
        {
            return CategoriaData.Registrar(oCategoria);
        }

        // PUT api/<controller>/5
        public bool Put(int id, [FromBody] Categoria oCategoria)
        {
            oCategoria.IdCategoria = id; // Asignar el id a la categoría recibida

            return CategoriaData.Modificar(oCategoria);
        }

        // DELETE api/<controller>/5
        public bool Delete(int id)
        {
            return CategoriaData.Eliminar(id);
        }
    }
}