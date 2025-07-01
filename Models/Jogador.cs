namespace ControleCampeonato.Models;

public class Jogador
{
    public int Id { get; set; }
    public required string Nome { get; set; }
    public int EquipeId { get; set; }
    public Equipe Equipe { get; set; } = null!;

    public ICollection<PontuacaoJogador> Pontuacoes { get; set; } = new List<PontuacaoJogador>();
}