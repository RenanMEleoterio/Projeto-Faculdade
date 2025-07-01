using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControleCampeonato.Models;
using ControleCampeonato.Data;
using ControleCampeonato.Dtos;

namespace ControleCampeonato.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JogadoresController : ControllerBase
    {
        // Campo para acessar o banco via Entity Framework
        private readonly ControleCampeonatoContext _context;

        // Construtor que injeta o DbContext via injeção de dependência
        public JogadoresController(ControleCampeonatoContext context)
        {
            _context = context;
        }

        // Retorna todos os jogadores, incluindo o nome da equipe de cada um
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Jogador>>> GetJogadores()
        {
            return await _context.Jogadores
                .Include(j => j.Equipe) // Inclui dados da equipe relacionada
                .ToListAsync();         // Retorna como lista assíncrona
        }

        // Busca um jogador por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Jogador>> GetJogador(int id)
        {
            var jogador = await _context.Jogadores
                .Include(j => j.Equipe) // Inclui equipe
                .FirstOrDefaultAsync(j => j.Id == id);

            if (jogador == null)
                return NotFound(); // Retorna 404 se não encontrar

            return jogador; // Retorna o jogador
        }

        // Cria um novo jogador
        [HttpPost]
        public async Task<ActionResult<Jogador>> PostJogador(JogadorCreateDto dto)
        {
            var equipe = await _context.Equipes.FindAsync(dto.EquipeId);
            if (equipe == null)
                return BadRequest("Equipe informada não existe.");

            var jogador = new Jogador
            {
                Nome = dto.Nome,
                EquipeId = dto.EquipeId,
                Equipe = equipe
            };

            _context.Jogadores.Add(jogador);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetJogador), new { id = jogador.Id }, jogador);
        }

        // Atualiza um jogador existente
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJogador(int id, Jogador jogador)
        {
            if (id != jogador.Id)
                return BadRequest(); // Retorna 400 

            _context.Entry(jogador).State = EntityState.Modified; // Marca como modificado

            try
            {
                await _context.SaveChangesAsync(); // Salva mudanças
            }
            catch (DbUpdateConcurrencyException)
            {
                // Verifica se o jogador ainda existe no banco
                if (!_context.Jogadores.Any(e => e.Id == id))
                    return NotFound(); // Retorna 404 se não existir
                else
                    throw; // Se for outro erro, relança
            }

            return NoContent(); // Retorna 204 (sem conteúdo) ao atualizar com sucesso
        }

        // Deleta um jogador
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJogador(int id)
        {
            var jogador = await _context.Jogadores.FindAsync(id); // Busca jogador

            if (jogador == null)
                return NotFound(); // Retorna 404 se não achar

            _context.Jogadores.Remove(jogador);        // Remove do contexto
            await _context.SaveChangesAsync();         // Aplica remoção no banco

            return NoContent(); // Retorna 204 (sem conteúdo)
        }
    }
}
