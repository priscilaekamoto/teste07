using Microsoft.AspNetCore.Mvc;
using api.Application.Pessoas.Queries;
using api.Application.Pessoas.Commands;
using api.Shared.Dtos;
using api.Shared.Mediator.Dispatcher;

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

        // GET: api/pessoas/totalReceitasDespesasSaldo
        [HttpGet("TotalReceitasDespesasSaldo")]
        public async Task<IActionResult> GetByName()
        {
            var result = await _dispatcher.QueryAsync<List<PessoaTotalReceitaDespesasSaldoDto>>(new GetAllTotalReceitasDespesasSaldoPessoasQuery(), HttpContext.RequestAborted);
            return Ok(result);
        }

        // POST: api/pessoas
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePessoaCommand createPessoaCommand)
        {
            if (createPessoaCommand is null)
                return BadRequest();

            var created = await _dispatcher.SendAsync<PessoaDto>(createPessoaCommand, HttpContext.RequestAborted);
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