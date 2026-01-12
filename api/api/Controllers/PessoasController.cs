using Microsoft.AspNetCore.Mvc;
using api.Application.Pessoas.Queries;
using api.Application.Pessoas.Commands;
using api.Shared.Dtos;
using api.Shared.Mediator.Dispatcher;

namespace api.Controllers
{
    /// <summary>
    /// Endpoints for managing people.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class PessoasController : ControllerBase
    {
        private readonly IDispatcher _dispatcher;

        public PessoasController(IDispatcher dispatcher) 
        {
            _dispatcher = dispatcher;
        }

        /// <summary>
        /// Get all person.
        /// </summary>
        /// <response code="200">List of person.</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<PessoaDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _dispatcher.QueryAsync<List<PessoaDto>>(new GetAllPessoasQuery(), HttpContext.RequestAborted);
            return Ok(result);
        }

        // GET: api/pessoas/totalReceitasDespesasSaldo
        /// <summary>
        /// Get person with totals (revenue/expenses/balance).
        /// </summary>
        /// <response code="200">List of person with totals.</response>
        [HttpGet("TotalReceitasDespesasSaldo")]
        [ProducesResponseType(typeof(List<PessoaTotalReceitaDespesasSaldoDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByTotal()
        {
            var result = await _dispatcher.QueryAsync<List<PessoaTotalReceitaDespesasSaldoDto>>(new GetAllTotalReceitasDespesasSaldoPessoasQuery(), HttpContext.RequestAborted);
            return Ok(result);
        }

        // POST: api/pessoas
        /// <summary>
        /// Create a new person.
        /// </summary>
        /// <response code="201">person created.</response>
        /// <response code="400">Bad request.</response>
        [HttpPost]
        [ProducesResponseType(typeof(PessoaDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreatePessoaCommand createPessoaCommand)
        {
            if (createPessoaCommand is null)
                return BadRequest();

            var created = await _dispatcher.SendAsync<PessoaDto>(createPessoaCommand, HttpContext.RequestAborted);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // GET: api/pessoas/{id}
        /// <summary>
        /// Get a person by id.
        /// </summary>
        /// <response code="200">person found.</response>
        /// <response code="404">person not found.</response>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(PessoaDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _dispatcher.QueryAsync<PessoaDto?>(new GetPessoaByIdQuery(id), HttpContext.RequestAborted);
            if (result == null) return NotFound();
            return Ok(result);
        }

        // DELETE: api/pessoas/{id}
        /// <summary>
        /// Delete a person by id.
        /// </summary>
        /// <response code="204">Deleted successfully.</response>
        /// <response code="404">person not found.</response>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _dispatcher.SendAsync<bool>(new DeletePessoaCommand(id), HttpContext.RequestAborted);
            if (!ok) return NotFound();
            return NoContent();
        }
    }
}