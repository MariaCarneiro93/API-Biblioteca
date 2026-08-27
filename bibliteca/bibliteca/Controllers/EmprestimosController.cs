using biblioteca.Services;
using Microsoft.AspNetCore.Mvc;

namespace biblioteca.Controllers
{
    [ApiController]
    [Route("api/emprestimos")]
    public class EmprestimosController : ControllerBase
    {
        private readonly EmprestimoService _service;

        public EmprestimosController(EmprestimoService service)
        {
            _service = service;
        }

        // GET: api/emprestimos
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var emprestimos = await _service.GetAllAsync();

            return Ok(emprestimos);
        }

        // GET: api/emprestimos/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var emprestimo = await _service.GetByIdAsync(id);

            if (emprestimo == null)
            {
                return NotFound(new
                {
                    mensagem = "Empréstimo não encontrado."
                });
            }

            return Ok(emprestimo);
        }

        // POST: api/emprestimos
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] EmprestimoRequest request)
        {
            var resultado = await _service.CreateAsync(
                request.LivroId,
                request.NomeUsuario
            );

            if (!resultado.Sucesso)
            {
                return BadRequest(new
                {
                    mensagem = resultado.Mensagem
                });
            }

            return Ok(resultado.Emprestimo);
        }

        // PUT: api/emprestimos/{id}/devolver
        [HttpPut("{id}/devolver")]
        public async Task<IActionResult> Devolver(Guid id)
        {
            var resultado = await _service.DevolverAsync(id);

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

    public class EmprestimoRequest
    {
        public Guid LivroId { get; set; }

        public string NomeUsuario { get; set; } = string.Empty;
    }
}