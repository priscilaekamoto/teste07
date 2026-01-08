using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Data;
using api.Models;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PessoasController : ControllerBase
    {
        private readonly AppDbContext _db;

        public PessoasController(AppDbContext db)
        {
            _db = db;
        }

        // GET: api/pessoas
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var pessoas = await _db.Pessoas
                                   .AsNoTracking()
                                   .ToListAsync();
            return Ok(pessoas);
        }

        // POST: api/pessoas
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Pessoa input)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var pessoa = new Pessoa
            {
                Nome = input.Nome,
                Idade = input.Idade
            };

            _db.Pessoas.Add(pessoa);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = pessoa.Id }, pessoa);
        }

        // GET: api/pessoas/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var pessoa = await _db.Pessoas
                                 .AsNoTracking()
                                 .FirstOrDefaultAsync(p => p.Id == id);

            if (pessoa == null)
                return NotFound();

            return Ok(pessoa);
        }

        // DELETE: api/pessoas/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var pessoa = await _db.Pessoas.FirstOrDefaultAsync(p => p.Id == id);

            if (pessoa == null)
                return NotFound();

            _db.Pessoas.Remove(pessoa);
            await _db.SaveChangesAsync();

            // Como OnDelete cascade está configurado, as transações serão removidas automaticamente.
            return NoContent();
        }
    }
}