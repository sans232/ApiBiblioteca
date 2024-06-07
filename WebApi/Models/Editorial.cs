using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class Editorial
    {
        public int IdEditorial { get; set; }
        public string Descripcion { get; set; }
        public bool Estado { get; set; }
    }
}