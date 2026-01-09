using api.Application.Categorias.Commands;
using api.Application.Categorias.Dtos;
using api.Application.Categorias.Queries;
using api.Application.Mediator.Dispatcher;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriasController : ControllerBase
    {
        private readonly IDispatcher _dispatcher;

        public CategoriasController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        // POST: api/categorias 
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCategoriaCommand createCategoriaCommand)
        {
            if (createCategoriaCommand is null)
                return BadRequest();

            var created = await _dispatcher.SendAsync<CategoriaDto>(createCategoriaCommand, HttpContext.RequestAborted);
            return CreatedAtAction(nameof(Create), new { id = created.Id }, created);
        }

        // GET: api/categorias
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _dispatcher.QueryAsync<List<CategoriaDto>>(new GetAllCategoriasQuery(), HttpContext.RequestAborted);
            return Ok(result);
        }

        // GET: api/categorias/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _dispatcher.QueryAsync<CategoriaDto?>(new GetCategoriaByIdQuery(id), HttpContext.RequestAborted);
            if (result == null) return NotFound();
            return Ok(result);
        }
    }
}
