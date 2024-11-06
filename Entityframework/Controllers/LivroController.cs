using Entityframework.Context;
using Entityframework.Model;
using Entityframework.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Entityframework.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LivroController : ControllerBase
    {
        private readonly LivroService _service;

        public LivroController(LivroService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Livro>>> GetAll()
        {
            var livros = await _service.GetAll();
            return Ok(livros);
        }
        [HttpGet("GetId")]
        public async Task<ActionResult<Livro>> GetById(int id)
        {
            if (id == 0) { return BadRequest("Não foi passado id"); }

            var livro = await _service.GetById(id);

            if (livro == null) { return BadRequest("Não foi encontrado id"); }

            return livro;
        }
        [HttpPost]
        public async Task<ActionResult<Livro>> AddLivro(Livro livro)
        {
            if (livro == null)
            {
                return BadRequest("Deve passar as informações do livro");
            }
            try
            {
                await _service.AddLivro(livro);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
        [HttpPut]
        public async Task<ActionResult<Livro>> UpdateLivro(Livro livro)
        {
            if (livro == null) { return BadRequest("Deve passar as informações do livro"); }

            try
            {
                await _service.UpdateLivro(livro);


            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(livro);
        }
        [HttpDelete]
        public async Task<IActionResult> RemoveLivro(int id)
        {
            if (id == 0) return BadRequest("Não foi passado o ID do livro");
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
