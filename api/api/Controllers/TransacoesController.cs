using api.Application.Transacoes.Commands;
using api.Application.Transacoes.Queries;
using api.Shared.Dtos;
using api.Shared.Mediator.Dispatcher;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransacoesController : ControllerBase
    {
        private readonly IDispatcher _dispatcher;
        public TransacoesController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        // POST: api/transacoes 
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTransacaoCommand createTransacaoCommand)
        {
            if (createTransacaoCommand is null)
                return BadRequest();
            var created = await _dispatcher.SendAsync<TransacaoDto>(createTransacaoCommand, HttpContext.RequestAborted);
            return CreatedAtAction(nameof(Create), new { id = created.Id }, created);
        }

        // GET: api/transacoes
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _dispatcher.QueryAsync<List<TransacaoDto>>(new GetAllTransacoesQuery(), HttpContext.RequestAborted);
            return Ok(result);

        }

        // GET: api/transacoes/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _dispatcher.QueryAsync<TransacaoDto?>(new GetTransacaoByIdQuery(id), HttpContext.RequestAborted);
            if (result == null) return NotFound();
            return Ok(result);
        }
    }
}
