namespace ControleCampeonato.Models;

public class PontuacaoEquipe
{
    public int IdPartida { get; set; }
    public Partida Partida { get; set; } = null!;
    public Equipe Equipe { get; set; } = null!;
    public int IdEquipe { get; set; }
    public int TotalKills { get; set; }
    public int Colocacao { get; set; }
    public int PontosColocacao { get; set; }
    public int PontuacaoTotal => TotalKills + PontosColocacao;
    

}