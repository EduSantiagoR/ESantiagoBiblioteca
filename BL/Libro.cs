using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Libro
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.ESantiagoBibliotecaContext context = new DL.ESantiagoBibliotecaContext())
                {
                    var query = context.Libros.FromSqlRaw("LibroGetAll").ToList();
                    if(query != null)
                    {
                        result.Objects = new List<object>();
                        foreach(var item in query)
                        {
                            ML.Libro libro = new ML.Libro();
                            libro.Autor = new ML.Autor();
                            libro.Genero = new ML.Genero();

                            libro.IdLibro = item.IdLibro;
                            libro.Titulo = item.Titulo;
                            libro.Disponible = item.Disponible;
                            libro.Autor.IdAutor = item.IdAutor;
                            libro.Autor.Nombre = item.Nombre;
                            libro.Autor.ApellidoPaterno = item.ApellidoPaterno;
                            libro.Autor.ApellidoMaterno = item.ApellidoMaterno;
                            libro.Genero.IdGenero = item.IdGenero;
                            libro.Genero.Tipo = item.Tipo;

                            result.Objects.Add(libro);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "No se han posido recuperar los libros.";
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
        public static ML.Result GetById(int idLibro)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.ESantiagoBibliotecaContext context = new DL.ESantiagoBibliotecaContext())
                {
                    var query = context.Libros.FromSqlRaw($"LibroGetById {idLibro}").AsEnumerable().FirstOrDefault();
                    if(query != null)
                    {
                        result.Object = new object();

                        ML.Libro libro = new ML.Libro();
                        libro.Autor = new ML.Autor();
                        libro.Genero = new ML.Genero();

                        libro.IdLibro = query.IdLibro;
                        libro.Titulo = query.Titulo;
                        libro.Disponible = query.Disponible;
                        libro.Autor.IdAutor = query.IdAutor;
                        libro.Autor.Nombre = query.Nombre;
                        libro.Autor.ApellidoPaterno = query.ApellidoPaterno;
                        libro.Autor.ApellidoMaterno = query.ApellidoMaterno;
                        libro.Genero.IdGenero = query.IdGenero;
                        libro.Genero.Tipo = query.Tipo;

                        result.Object = libro;
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "Error al obtener el registro.";
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
        public static ML.Result Delete(int idLibro)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.ESantiagoBibliotecaContext context = new DL.ESantiagoBibliotecaContext())
                {
                    int rowsAffected = context.Database.ExecuteSqlRaw($"LibroDelete {idLibro}");
                    if (rowsAffected > 0)
                    {
                        result.Correct = true;
                        result.Message = "Eliminado correctamente.";
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "Error al eliminar el libro.";
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
        public static ML.Result Add(ML.Libro libro)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.ESantiagoBibliotecaContext context = new DL.ESantiagoBibliotecaContext())
                {
                    int rowsAffected = context.Database.ExecuteSqlRaw($"LibroAdd '{libro.Titulo}',{libro.Autor.IdAutor},{libro.Genero.IdGenero},{libro.Disponible}");
                    if(rowsAffected > 0)
                    {
                        result.Correct = true;
                        result.Message = "Agregado correctamente.";
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "Error al agregar el libro";
                    }
                }
            }
            catch(Exception ex )
            {
                result.Correct = false;
                result.Message = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result Update(ML.Libro libro)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.ESantiagoBibliotecaContext context = new DL.ESantiagoBibliotecaContext())
                {
                    int rowsAffected = context.Database.ExecuteSqlRaw($"LibroUpdate {libro.IdLibro},'{libro.Titulo}',{libro.Autor.IdAutor},{libro.Genero.IdGenero},{libro.Disponible}");
                    if (rowsAffected > 0)
                    {
                        result.Correct = true;
                        result.Message = "Actualización éxitosa.";
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "Error al actualizar.";
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