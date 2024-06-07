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
    public class AutorController : ApiController
    {
        // GET api/<controller>
        public List<Autor> Get()
        {
            return AutorData.Listar();
        }

        // GET api/<controller>/5
        public Autor Get(int id)
        {
            return AutorData.Obtener(id);
        }

        public bool Post([FromBody] Autor oAutor)
        {
            return AutorData.Registrar(oAutor);
        }

        // PUT api/<controller>/5
        public bool Put(int id, [FromBody] Autor oAutor)
        {
            oAutor.IdAutor = id; // Asignar el id a la categoría recibida

            return AutorData.Modificar(oAutor);
        }

        // DELETE api/<controller>/5
        public bool Delete(int id)
        {
            return AutorData.Eliminar(id);
        }
    }
}