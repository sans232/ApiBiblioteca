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
    public class LibroController : ApiController
    {
        // GET api/<controller>
        public List<Libro> Get()
        {
            return LibroData.Listar();
        }

        // GET api/<controller>/5
        public Libro Get(int id)
        {
            return LibroData.Obtener(id);
        }

        // POST api/<controller>
        public bool Post([FromBody] Libro libro)
        {
            return LibroData.Registrar(libro) > 0;
        }

        // PUT api/<controller>
        public bool Put([FromBody] Libro libro)
        {
            return LibroData.Modificar(libro);
        }

        // DELETE api/<controller>/5
        public bool Delete(int id)
        {
            return LibroData.Eliminar(id);
        }
    }
}