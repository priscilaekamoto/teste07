using api.Application.Categorias.Commands;
using api.Application.Categorias.Queries;
using api.Shared.Dtos;
using api.Shared.Mediator.Dispatcher;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    /// <summary>
    /// Category management endpoints.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class CategoriasController : ControllerBase
    {
        private readonly IDispatcher _dispatcher;

        public CategoriasController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        // POST: api/categorias 
        /// <summary>
        /// Create a new category.
        /// </summary>
        /// <param name="createCategoriaCommand">Category data.</param>
        /// <returns>Created category DTO.</returns>
        /// <response code="201">Category created.</response>
        /// <response code="400">Bad request.</response>
        [HttpPost]
        [ProducesResponseType(typeof(CategoriaDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateCategoriaCommand createCategoriaCommand)
        {
            if (createCategoriaCommand is null)
                return BadRequest();

            var created = await _dispatcher.SendAsync<CategoriaDto>(createCategoriaCommand, HttpContext.RequestAborted);
            return CreatedAtAction(nameof(Create), new { id = created.Id }, created);
        }

        // GET: api/categorias
        /// <summary>
        /// Get all categories.
        /// </summary>
        /// <returns>List of categories.</returns>
        /// <response code="200">Get the list of categories.</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<CategoriaDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _dispatcher.QueryAsync<List<CategoriaDto>>(new GetAllCategoriasQuery(), HttpContext.RequestAborted);
            return Ok(result);
        }

        // GET: api/categorias/totalReceitasDespesasSaldo
        /// <summary>
        /// Get categories totals (revenue/expenses/balance).
        /// </summary>
        /// <returns>Total by category.</returns>
        /// <response code="200">Get total.</response>
        [HttpGet("TotalReceitasDespesasSaldo")]
        [ProducesResponseType(typeof(List<CategoriaTotalReceitasDespesasSaldoDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTotalReceitasDespesasSaldo()
        {
            var result = await _dispatcher.QueryAsync<List<CategoriaTotalReceitasDespesasSaldoDto>>(new GetAllTotalReceitasdespesasSaldoCategoriaQuery(), HttpContext.RequestAborted);
            return Ok(result);
        }

        // GET: api/categorias/{id}
        /// <summary>
        /// Get category by id.
        /// </summary>
        /// <param name="id">Category id.</param>
        /// <returns>Category DTO or 404.</returns>
        /// <response code="200">Get category.</response>
        /// <response code="404">Category not found.</response>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(CategoriaDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _dispatcher.QueryAsync<CategoriaDto?>(new GetCategoriaByIdQuery(id), HttpContext.RequestAborted);
            if (result == null) return NotFound();
            return Ok(result);
        }
    }
}
