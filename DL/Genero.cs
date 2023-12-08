using System;
using System.Collections.Generic;

namespace DL
{
    public partial class Genero
    {
        public Genero()
        {
            Libros = new HashSet<Libro>();
        }

        public int IdGenero { get; set; }
        public string Tipo { get; set; } = null!;

        public virtual ICollection<Libro> Libros { get; set; }
    }
}
