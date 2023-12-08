using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibroController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public IActionResult GetAll()
        {
            ML.Result result = BL.Libro.GetAll();
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        [HttpGet]
        [Route("{idLibro}")]
        public IActionResult GetById(int idLibro)
        {
            ML.Result result = BL.Libro.GetById(idLibro);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        [HttpPost]
        [Route("")]
        public IActionResult Add([FromBody] ML.Libro libro)
        {
            ML.Result result = BL.Libro.Add(libro);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        [HttpPut]
        [Route("{idLibro}")]
        public IActionResult Update(int idLibro, [FromBody]ML.Libro libro)
        {
            libro.IdLibro = idLibro;
            ML.Result result = BL.Libro.Update(libro);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        [HttpDelete]
        [Route("{idLibro}")]
        public IActionResult Delete(int idLibro)
        {
            ML.Result result = BL.Libro.Delete(idLibro);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
    }
}
