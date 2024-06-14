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
    public class PersonaController : ApiController
    {
        // GET api/persona
        public List<Persona> Get()
        {
            return PersonaData.Listar();
        }

        // GET api/persona/5
        public Persona Get(int id)
        {
            return PersonaData.Obtener(id);
        }

        // POST api/persona
        public bool Post([FromBody] Persona oPersona)
        {
            return PersonaData.Registrar(oPersona);
        }

        // PUT api/persona/5
        public bool Put(int id, [FromBody] Persona oPersona)
        {
            oPersona.IdPersona = id; // Asignar el id a la persona recibida
            return PersonaData.Modificar(oPersona);
        }

        // DELETE api/persona/5
        public bool Delete(int id)
        {
            return PersonaData.Eliminar(id);
        }
    }
}