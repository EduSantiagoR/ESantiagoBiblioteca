using System;
using System.Collections.Generic;

namespace DL
{
    public partial class Autor
    {
        public Autor()
        {
            Libros = new HashSet<Libro>();
        }

        public int IdAutor { get; set; }
        public string Nombre { get; set; } = null!;
        public string ApellidoPaterno { get; set; } = null!;
        public string? ApellidoMaterno { get; set; }

        public virtual ICollection<Libro> Libros { get; set; }
    }
}
