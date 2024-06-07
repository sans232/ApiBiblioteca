
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WebApi.Models
{
    public class Libro
    {
        public int IdLibro { get; set; }
        public string Titulo { get; set; }
        public string RutaPortada { get; set; }
        public string NombrePortada { get; set; }

        public Autor oAutor { get; set; }

        public Categoria oCategoria { get; set; }

        public Editorial oEditorial { get; set; }
        public string Ubicacion { get; set; }
        public int Ejemplares { get; set; }
        public bool Estado { get; set; }

    }
}