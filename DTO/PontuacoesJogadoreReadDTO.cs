public class PontuacaoJogadorReadDto
{
    public string NomePartida { get; set; } = "";
    public string NomeEquipe { get; set; } = "";
    public string NomeJogador { get; set; } = "";
    public int Kills { get; set; }
    public int Mortes { get; set; }
    public float Dano { get; set; }
    public int Assistencias { get; set; }
}
