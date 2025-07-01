using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControleCampeonato.Data;
using ControleCampeonato.Models;
using ControleCampeonato.Dtos;

namespace ControleCampeonato.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PontuacoesJogadoresController : ControllerBase
    {
        // Campo para acessar o banco via Entity Framework
        private readonly ControleCampeonatoContext _context;

        // Construtor que injeta o DbContext via injeção de dependência
        public PontuacoesJogadoresController(ControleCampeonatoContext context)
        {
            _context = context;
        }

        // Lista todas as pontuações dos jogadores nas partidas
       [HttpGet]
        public async Task<ActionResult<IEnumerable<PontuacaoJogadorReadDto>>> GetPontuacoesJogadores()
        {
            var pontuacoes = await _context.PontuacoesJogadores
                .Include(p => p.Partida)
                .Include(p => p.Equipe)
                .Include(p => p.Jogador)
                .Select(p => new PontuacaoJogadorReadDto
                {
                    NomePartida = p.Partida != null && !string.IsNullOrEmpty(p.Partida.Mapa) ? p.Partida.Mapa : "Desconhecido",
                    NomeEquipe = p.Equipe != null && !string.IsNullOrEmpty(p.Equipe.Nome) ? p.Equipe.Nome : "Desconhecido",
                    NomeJogador = p.Jogador != null && !string.IsNullOrEmpty(p.Jogador.Nome) ? p.Jogador.Nome : "Desconhecido",
                    Kills = p.Kills,
                    Mortes = p.Mortes,
                    Dano = p.Dano,
                    Assistencias = p.Assistencias
                })
                .ToListAsync();

            return Ok(pontuacoes); // Retorna 200
}


        // Busca pontuação específica por Partida e Jogador (chave composta)
        [HttpGet("{idPartida}/{idJogador}")]
        public async Task<ActionResult<PontuacaoJogador>> GetPontuacao(int idPartida, int idJogador)
        {
            var pontuacao = await _context.PontuacoesJogadores
                .Include(p => p.Jogador)
                .Include(p => p.Equipe)
                .Include(p => p.Partida)
                .FirstOrDefaultAsync(p => p.IdPartida == idPartida && p.IdJogador == idJogador);

            if (pontuacao == null)
                return NotFound(); // Retorna 404 se não achar

            return pontuacao;
        }

        // Cadastra nova pontuação para um jogador em uma partida
        [HttpPost]
        public async Task<ActionResult<PontuacaoJogador>> PostPontuacao(PontuacaoJogadorCreateDto dto)
        {
            var pontuacao = new PontuacaoJogador
            {
                IdPartida = dto.IdPartida,
                IdEquipe = dto.IdEquipe,
                IdJogador = dto.IdJogador,
                Kills = dto.Kills,
                Mortes = dto.Mortes,
                Dano = dto.Dano,
                Assistencias = dto.Assistencias
            };

            _context.PontuacoesJogadores.Add(pontuacao);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPontuacao), new { idPartida = pontuacao.IdPartida, idJogador = pontuacao.IdJogador }, pontuacao);
        }


        // Atualiza a pontuação de um jogador em uma partida
        [HttpPut("{idPartida}/{idJogador}")]
        public async Task<IActionResult> PutPontuacao(int idPartida, int idJogador, PontuacaoJogador pontuacao)
        {
            if (idPartida != pontuacao.IdPartida || idJogador != pontuacao.IdJogador)
                return BadRequest(); // Retorna 400

            _context.Entry(pontuacao).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                var exists = await _context.PontuacoesJogadores.AnyAsync(p => p.IdPartida == idPartida && p.IdJogador == idJogador);
                if (!exists)
                    return NotFound(); // Retorna 404 se não achar
                else
                    throw;
            }

            return NoContent(); // Retorna 204
        }

        // Deleta pontuação de um jogador em uma partida
        [HttpDelete("{idPartida}/{idJogador}")]
        public async Task<IActionResult> DeletePontuacao(int idPartida, int idJogador)
        {
            var pontuacao = await _context.PontuacoesJogadores
                .FirstOrDefaultAsync(p => p.IdPartida == idPartida && p.IdJogador == idJogador);

            if (pontuacao == null)
                return NotFound(); // Retorna 404 se não achar

            _context.PontuacoesJogadores.Remove(pontuacao);
            await _context.SaveChangesAsync();

            return NoContent(); // Retorna 204
        }
    }
}
