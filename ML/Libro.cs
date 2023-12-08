using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Libro
    {
        public int IdLibro { get; set; }
        public string Titulo { get; set; }
        public Autor Autor { get; set; }
        public Genero Genero { get; set; }
        public bool Disponible { get; set; }
        public List<object>? Libros { get; set; }
    }
}
