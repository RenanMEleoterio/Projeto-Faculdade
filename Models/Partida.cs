namespace ControleCampeonato.Models;

public class Partida
{
    public int Id { get; set; }
    public DateTime Data {get; set; }
    public string? Mapa { get; set; }  

    public ICollection<PontuacaoEquipe> PontuacoesEquipes { get; set; } = new List<PontuacaoEquipe>();
}