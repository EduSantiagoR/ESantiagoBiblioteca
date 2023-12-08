using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Autor
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.ESantiagoBibliotecaContext context = new DL.ESantiagoBibliotecaContext())
                {
                    var query = (from autor in context.Autors
                                 select new
                                 {
                                     IdAutor = autor.IdAutor,
                                     Nombre = autor.Nombre,
                                     ApellidoPaterno = autor.ApellidoPaterno,
                                     ApellidoMaterno = autor.ApellidoMaterno
                                 }).ToList();
                    if (query != null)
                    {
                        result.Objects = new List<object>();
                        foreach(var item in query)
                        {
                            ML.Autor autor = new ML.Autor();
                            autor.IdAutor = item.IdAutor;
                            autor.Nombre = item.Nombre + " " + item.ApellidoPaterno + " " + item.ApellidoMaterno;
                            autor.ApellidoPaterno = item.ApellidoPaterno;
                            autor.ApellidoMaterno = item.ApellidoMaterno;
                            result.Objects.Add(autor);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "Error al obtener los autores.";
                    }
                }
            }
            catch(Exception ex)
            {
                result.Correct = false;
                result.Message = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
    }
}
