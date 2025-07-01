using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControleCampeonato.Data;
using ControleCampeonato.Models;
using ControleCampeonato.DTOs;

namespace ControleCampeonato.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PontuacoesEquipesController : ControllerBase
{
    // Campo para acessar o banco via Entity Framework
    private readonly ControleCampeonatoContext _context;

    // Construtor que injeta o DbContext via injeção de dependência
    public PontuacoesEquipesController(ControleCampeonatoContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<ActionResult<PontuacaoEquipe>> PostPontuacaoEquipe([FromBody] PontuacaoEquipeCreateDto input)
    {
        // Verifica se já existe pontuação cadastrada para essa equipe nessa partida
        var existente = await _context.PontuacoesEquipes.FindAsync(input.IdPartida, input.IdEquipe);
        if (existente != null)
        {
            return BadRequest("Pontuação para essa equipe nessa partida já existe.");
        }

        // Soma as kills de todos os jogadores dessa equipe na partida informada
        var killsTotais = await _context.PontuacoesJogadores
            .Where(p => p.IdEquipe == input.IdEquipe && p.IdPartida == input.IdPartida)
            .SumAsync(p => p.Kills);

        // Define os pontos pela colocação da equipe (ex: 1º lugar = 10 pontos)
        int pontosColocacao = input.Colocacao switch
        {
            1 => 10,
            2 => 8,
            3 => 6,
            4 => 4,
            5 => 2,
            _ => 1
        };

        // Monta o objeto de pontuação da equipe com os dados calculados
        var pontuacao = new PontuacaoEquipe
        {
            IdPartida = input.IdPartida,
            IdEquipe = input.IdEquipe,
            TotalKills = killsTotais,
            Colocacao = input.Colocacao,
            PontosColocacao = pontosColocacao
        };

        // Adiciona no banco e salva
        _context.PontuacoesEquipes.Add(pontuacao);
        await _context.SaveChangesAsync();

        // Retorna resposta de sucesso com a rota de consulta
        return CreatedAtAction(nameof(GetPontuacaoEquipe), new { idPartida = pontuacao.IdPartida, idEquipe = pontuacao.IdEquipe }, pontuacao);
    }

    // Retorna lista de pontuações
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PontuacaoEquipe>>> GetPontuacoes()
    {
        return await _context.PontuacoesEquipes.ToListAsync();
    }

    // GET por chave composta
    [HttpGet("{idPartida}/{idEquipe}")]
    public async Task<ActionResult<PontuacaoEquipe>> GetPontuacaoEquipe(int idPartida, int idEquipe)
    {
        var pontuacao = await _context.PontuacoesEquipes.FindAsync(idPartida, idEquipe);

        if (pontuacao == null)
            return NotFound(); // Retorna 404 se não existir

        return pontuacao;
    }

    // Apaga pontuação
    [HttpDelete("{idPartida}/{idEquipe}")]
    public async Task<IActionResult> DeletePontuacaoEquipe(int idPartida, int idEquipe)
    {
        var pontuacao = await _context.PontuacoesEquipes.FindAsync(idPartida, idEquipe);
        if (pontuacao == null)
            return NotFound(); // Retorna 404 se não existir

        _context.PontuacoesEquipes.Remove(pontuacao);
        await _context.SaveChangesAsync();

        return NoContent(); // Retorna 204 (sem conteúdo)
    }
}
