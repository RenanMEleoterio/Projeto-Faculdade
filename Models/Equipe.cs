namespace ControleCampeonato.Models;

public class Equipe
{
    public int Id { get; set; }
    public required string Nome { get; set; }
    public ICollection<PontuacaoEquipe> Pontuacoes { get; set; } = new List<PontuacaoEquipe>();

}