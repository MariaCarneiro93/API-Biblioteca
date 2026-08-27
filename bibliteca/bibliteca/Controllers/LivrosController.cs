using biblioteca.Models;
using biblioteca.Services;
using Microsoft.AspNetCore.Mvc;

namespace biblioteca.Controllers
{
    [ApiController]
    [Route("api/livros")]
    public class LivrosController : ControllerBase
    {
        private readonly LivroService _service;

        public LivrosController(LivroService service)
        {
            _service = service;
        }

        // GET: api/livros
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var livros = await _service.GetAllAsync();

            return Ok(livros);
        }

        // GET: api/livros/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var livro = await _service.GetByIdAsync(id);

            if (livro == null)
            {
                return NotFound(new
                {
                    mensagem = "Livro não encontrado."
                });
            }

            return Ok(livro);
        }

        // POST: api/livros
        [HttpPost]
        public async Task<IActionResult> Create(Livro livro)
        {
            var resultado = await _service.CreateAsync(livro);

            if (!resultado.Sucesso)
            {
                return Conflict(new
                {
                    mensagem = resultado.Mensagem
                });
            }

            return CreatedAtAction(
                nameof(GetById),
                new { id = resultado.Livro!.Id },
                resultado.Livro
            );
        }

        // PUT: api/livros/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, Livro livro)
        {
            var resultado = await _service.UpdateAsync(id, livro);

            if (!resultado.Sucesso)
            {
                return BadRequest(new
                {
                    mensagem = resultado.Mensagem
                });
            }

            return Ok(new
            {
                mensagem = resultado.Mensagem
            });
        }

        // DELETE: api/livros/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var resultado = await _service.DeleteAsync(id);

            if (!resultado.Sucesso)
            {
                return BadRequest(new
                {
                    mensagem = resultado.Mensagem
                });
            }

            return Ok(new
            {
                mensagem = resultado.Mensagem
            });
        }
    }
}