namespace ControleCampeonato.Models;

public class PontuacaoJogador
{
    public int IdPartida { get; set; }
    public Partida Partida { get; set; } = null!;

    
    public int IdEquipe { get; set; }
    public Equipe Equipe { get; set; } = null!;
    
    public int IdJogador { get; set; }
    public Jogador Jogador { get; set; } = null!;

    public int Kills { get; set; }
    public int Mortes { get; set; }
    public float Dano { get; set; }
    public int Assistencias { get; set; }
}