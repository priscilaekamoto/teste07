using api.Application.Transacoes.Commands;
using api.Application.Transacoes.Queries;
using api.Shared.Dtos;
using api.Shared.Mediator.Dispatcher;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    /// <summary>
    /// Operations related to financial transactions.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class TransacoesController : ControllerBase
    {
        private readonly IDispatcher _dispatcher;
        public TransacoesController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        // POST: api/transacoes 
        /// <summary>
        /// Create a new transaction.
        /// </summary>
        /// <param name="createTransacaoCommand">Transaction data.</param>
        /// <returns>Created transaction DTO.</returns>
        /// <response code="201">Transaction successfully created.</response>
        /// <response code="400">Bad request (malformed payload).</response>
        /// <response code="404">Related resource not found (e.g. person).</response>
        /// <response code="422">Business rule violation (semantic validation).</response>
        [HttpPost]
        [ProducesResponseType(typeof(TransacaoDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Create([FromBody] CreateTransacaoCommand createTransacaoCommand)
        {
            if (createTransacaoCommand is null)
                return BadRequest();
            var created = await _dispatcher.SendAsync<TransacaoDto>(createTransacaoCommand, HttpContext.RequestAborted);
            return CreatedAtAction(nameof(Create), new { id = created.Id }, created);
        }

        // GET: api/transacoes
        /// <summary>
        /// Get all transactions.
        /// </summary>
        /// <returns>List of transaction DTOs.</returns>
        /// <response code="200">Get the list of transactions.</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<TransacaoDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _dispatcher.QueryAsync<List<TransacaoDto>>(new GetAllTransacoesQuery(), HttpContext.RequestAborted);
            return Ok(result);

        }

        // GET: api/transacoes/{id}
        /// <summary>
        /// Get transaction by id.
        /// </summary>
        /// <param name="id">Transaction id.</param>
        /// <returns>Transaction DTO or 404.</returns>
        /// <response code="200">Get transaction.</response>
        /// <response code="404">Transaction not found.</response>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(TransacaoDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _dispatcher.QueryAsync<TransacaoDto?>(new GetTransacaoByIdQuery(id), HttpContext.RequestAborted);
            if (result == null) return NotFound();
            return Ok(result);
        }
    }
}
