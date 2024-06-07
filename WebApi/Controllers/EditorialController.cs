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
    public class EditorialController : ApiController
    {
        // GET api/<controller>
        public List<Editorial> Get()
        {
            return EditorialData.Listar();
        }

        // GET api/<controller>/5
        public Editorial Get(int id)
        {
            return EditorialData.Obtener(id);
        }

        public bool Post([FromBody] Editorial oEditorial)
        {
            return EditorialData.Registrar(oEditorial);
        }

        // PUT api/<controller>/5
        public bool Put(int id, [FromBody] Editorial oEditorial)
        {
            oEditorial.IdEditorial = id; // Asignar el id a la categoría recibida

            return EditorialData.Modificar(oEditorial);
        }

        // DELETE api/<controller>/5
        public bool Delete(int id)
        {
            return EditorialData.Eliminar(id);
        }
    }
}