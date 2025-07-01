using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControleCampeonato.Models;
using ControleCampeonato.Data;

namespace ControleCampeonato.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EquipesController : ControllerBase
    {
        // Campo para acessar o banco via Entity Framework
        private readonly ControleCampeonatoContext _context;

        // Construtor que injeta o DbContext via injeção de dependência
        public EquipesController(ControleCampeonatoContext context)
        {
            _context = context;
        }

        // Lista todas as equipes, incluindo seus jogadores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Equipe>>> GetEquipes()
        {
            return await _context.Equipes
                .ToListAsync();
        }

        // Retorna uma equipe pelo ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Equipe>> GetEquipe(int id)
        {
            var equipe = await _context.Equipes
                .FirstOrDefaultAsync(e => e.Id == id);

            if (equipe == null)
                return NotFound(); // Retorna 404 se não encontrar

            return equipe;
        }

        // Cadastra uma nova equipe
        [HttpPost]
        public async Task<ActionResult<Equipe>> PostEquipe(Equipe equipe)
        {
            _context.Equipes.Add(equipe);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEquipe), new { id = equipe.Id }, equipe);
        }

        // Atualiza uma equipe existente
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEquipe(int id, Equipe equipe)
        {
            if (id != equipe.Id)
                return BadRequest(); // Retorna 400

            _context.Entry(equipe).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Equipes.Any(e => e.Id == id))
                    return NotFound(); // Retorna 404 se não encontrar
                else
                    throw;
            }

            return NoContent();  // Retorna 204 (sem conteúdo)
        }

        // Remove uma equipe
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEquipe(int id)
        {
            var equipe = await _context.Equipes.FindAsync(id);

            if (equipe == null)
                return NotFound(); // Retorna 404 se não encontrar

            _context.Equipes.Remove(equipe);
            await _context.SaveChangesAsync();

            return NoContent();  // Retorna 204 (sem conteúdo)
        }
    }
}
