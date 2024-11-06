using Entityframework.Context;
using Entityframework.Model;
using Entityframework.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Entityframework.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AutoController : ControllerBase
    {
        private readonly AutorService _service;

        public AutoController(AutorService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Autor>>> GetAll()
        {
            var autor = await _service.GetAll();
            return Ok(autor);
        }
        [HttpGet("GetIdAutor")]
        public async Task<ActionResult<Autor>> GetById(int id)
        {
            if (id == 0) { return BadRequest("Não foi passado id"); }

            var autor = await _service.GetById(id);

            if (autor == null) { return BadRequest("Não foi encontrado id"); }

            return autor;
        }
        [HttpPost]
        public async Task<ActionResult<Autor>> AddAutor(Autor autor)
        {
            if (autor == null)
            {
                return BadRequest("Deve passar as informações do autor");
            }
            try
            {
               _service.AddAutor(autor);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
        [HttpPut]
        public async Task<ActionResult<Autor>> UpdateLivro(Autor autor)
        {
            if (autor == null) { return BadRequest("Deve passar as informações do autor"); }

            try
            {
               _service.UpdateLivro(autor);


            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(autor);
        }
        [HttpDelete]
        public async Task<IActionResult> RemoveLivro(int id)
        {
            if (id == 0) return BadRequest("Não foi passado o ID do autor");
            try
            {

                var result = await _service.RemoveLivro(id);
                if (!result) return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

    }
}
