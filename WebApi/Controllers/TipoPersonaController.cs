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
    public class TipoPersonaController : ApiController
    {
        // GET api/tipopersona
        public List<TipoPersona> Get()
        {
            return TipoPersonaData.Listar();
        }

        // GET api/tipopersona/5
        public TipoPersona Get(int id)
        {
            return TipoPersonaData.Obtener(id);
        }

        // POST api/tipopersona
        public bool Post([FromBody] TipoPersona oTipoPersona)
        {
            return TipoPersonaData.Registrar(oTipoPersona);
        }

        // PUT api/tipopersona/5
        public bool Put(int id, [FromBody] TipoPersona oTipoPersona)
        {
            oTipoPersona.IdTipoPersona = id; // Asignar el id al tipo de persona recibida
            return TipoPersonaData.Modificar(oTipoPersona);
        }

        // DELETE api/tipopersona/5
        public bool Delete(int id)
        {
            return TipoPersonaData.Eliminar(id);
        }
    }
}