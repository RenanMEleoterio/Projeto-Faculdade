namespace ControleCampeonato.Dtos
{
    public class PontuacaoJogadorCreateDto
    {
        public int IdPartida { get; set; }
        public int IdEquipe { get; set; }
        public int IdJogador { get; set; }

        public int Kills { get; set; }
        public int Mortes { get; set; }
        public float Dano { get; set; }
        public int Assistencias { get; set; }
    }
}
