using System;
using System.Collections.Generic;

namespace DL
{
    public partial class Libro
    {
        public int IdLibro { get; set; }
        public string Titulo { get; set; } = null!;
        public int IdAutor { get; set; }
        public int IdGenero { get; set; }
        public bool Disponible { get; set; }

        //
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }
        public string Tipo { get; set; }

        public virtual Autor IdAutorNavigation { get; set; } = null!;
        public virtual Genero IdGeneroNavigation { get; set; } = null!;
    }
}
