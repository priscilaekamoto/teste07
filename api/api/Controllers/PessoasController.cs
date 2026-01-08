using Microsoft.AspNetCore.Mvc;
using api.Application.Mediator.Dispatcher;
using api.Application.Pessoas.Dtos;
using api.Application.Pessoas.Queries;
using api.Application.Pessoas.Commands;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PessoasController : ControllerBase
    {
        private readonly IDispatcher _dispatcher;

        public PessoasController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        // GET: api/pessoas
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _dispatcher.QueryAsync<List<PessoaDto>>(new GetAllPessoasQuery(), HttpContext.RequestAborted);
            return Ok(result);
        }

        // POST: api/pessoas
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePessoaCommand input)
        {
            if (!ModelState.IsValid || input is null)
                return BadRequest(ModelState);

            var created = await _dispatcher.SendAsync<PessoaDto>(input, HttpContext.RequestAborted);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // GET: api/pessoas/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _dispatcher.QueryAsync<PessoaDto?>(new GetPessoaByIdQuery(id), HttpContext.RequestAborted);
            if (result == null) return NotFound();
            return Ok(result);
        }

        // DELETE: api/pessoas/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _dispatcher.SendAsync<bool>(new DeletePessoaCommand(id), HttpContext.RequestAborted);
            if (!ok) return NotFound();
            return NoContent();
        }
    }
}