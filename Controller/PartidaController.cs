using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControleCampeonato.Data;
using ControleCampeonato.Models;

namespace ControleCampeonato.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PartidasController : ControllerBase
    {
        // Campo para acessar o banco via Entity Framework
        private readonly ControleCampeonatoContext _context;

        // Construtor que injeta o DbContext via injeção de dependência
        public PartidasController(ControleCampeonatoContext context)
        {
            _context = context;
        }

        // Retorna dados das partidas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Partida>>> GetPartidas()
        {
            return await _context.Partidas
                .Include(p => p.PontuacoesEquipes) // inclui as pontuações da partida
                .ToListAsync();
        }

        // Retorna partida pelo id
        [HttpGet("{id}")]
        public async Task<ActionResult<Partida>> GetPartida(int id)
        {
            var partida = await _context.Partidas
                .Include(p => p.PontuacoesEquipes)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (partida == null)
                return NotFound(); // Retorna 404 se não encontrar

            return partida;
        }

        // Cria nova partida
        [HttpPost]
        public async Task<ActionResult<Partida>> PostPartida(Partida partida)
        {
            _context.Partidas.Add(partida);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPartida), new { id = partida.Id }, partida);
        }

        // Atualiza partida
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPartida(int id, Partida partida)
        {
            if (id != partida.Id)
                return BadRequest(); // Retorna 400 

            _context.Entry(partida).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Partidas.Any(p => p.Id == id))
                    return NotFound(); // Retorna 404 se não encontrar
                else
                    throw;
            }

            return NoContent();
        }

        // Deleta partida
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePartida(int id)
        {
            var partida = await _context.Partidas.FindAsync(id);

            if (partida == null)
                return NotFound();

            _context.Partidas.Remove(partida);
            await _context.SaveChangesAsync();

            return NoContent(); // Retorna 204 (sem conteúdo)
        }
    }
}
