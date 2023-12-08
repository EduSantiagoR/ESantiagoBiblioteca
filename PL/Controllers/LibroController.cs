using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class LibroController : Controller
    {
        public IActionResult GetAll()
        {
            ML.Libro libro = new ML.Libro();
            libro.Libros = new List<object>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:12797/api/");
                var taskResponse = client.GetAsync("Libro");
                taskResponse.Wait();

                var resultService = taskResponse.Result;
                if (resultService.IsSuccessStatusCode)
                {
                    var readTask = resultService.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();
                    foreach(var item in readTask.Result.Objects)
                    {
                        ML.Libro libroResult = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Libro>(item.ToString());
                        libro.Libros.Add(libroResult);
                    }
                }
            }
            return View(libro);
        }
        public IActionResult Form(int? idLibro)
        {
            ML.Result resultGeneros = BL.Genero.GetAll();
            ML.Result resultAutores = BL.Autor.GetAll();
            ML.Libro libro = new ML.Libro();
            if (idLibro != null)
            {
                using(var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:12797/api/");
                    var taskResponse = client.GetAsync($"Libro/{idLibro}");
                    taskResponse.Wait();

                    var resultService = taskResponse.Result;
                    if (resultService.IsSuccessStatusCode)
                    {
                        var readTask = resultService.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();

                        libro = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Libro>(readTask.Result.Object.ToString());
                    }
                }
                libro.Genero.Generos = resultGeneros.Objects;
                libro.Autor.Autores = resultAutores.Objects;
            }
            else
            {
                libro.Genero = new ML.Genero();
                libro.Autor = new ML.Autor();
                libro.Genero.Generos = resultGeneros.Objects;
                libro.Autor.Autores = resultAutores.Objects;
            }
            return View(libro);
        }
        [HttpPost]
        public IActionResult Form(ML.Libro libro)
        {
            if(libro.IdLibro == 0)
            {
                ML.Result result = new ML.Result();
                using(var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:12797/api/");
                    var postTask = client.PostAsJsonAsync("Libro", libro);
                    postTask.Wait();

                    var resultPost = postTask.Result;
                    if (resultPost.IsSuccessStatusCode)
                    {
                        var readTask = resultPost.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();

                        result = readTask.Result;
                    }
                }
                ViewBag.Mensaje = result.Message;
            }
            else
            {
                ML.Result result = new ML.Result();
                using(var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:12797/api/");
                    var taskResponse = client.PutAsJsonAsync($"Libro/{libro.IdLibro}", libro);
                    taskResponse.Wait();

                    var resultService = taskResponse.Result;
                    if (resultService.IsSuccessStatusCode)
                    {
                        var readTask = resultService.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();

                        result = readTask.Result;
                    }
                }
                ViewBag.Mensaje = result.Message;
            }
            return PartialView("Modal");
        }
        public IActionResult Delete(int idLibro)
        {
            ML.Result result = new ML.Result();
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:12797/api/");
                var taskResponse = client.DeleteAsync($"Libro/{idLibro}");
                taskResponse.Wait();

                var resultService = taskResponse.Result;
                if (resultService.IsSuccessStatusCode)
                {
                    var readTask = resultService.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();

                    result = readTask.Result;
                }
            }
            ViewBag.Mensaje = result.Message;
            return PartialView("Modal");
        }
    }
}
