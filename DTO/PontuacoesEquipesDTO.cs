namespace ControleCampeonato.DTOs
{
    public class PontuacaoEquipeCreateDto
    {
        public int IdPartida { get; set; }
        public int IdEquipe { get; set; }
        public int TotalKills { get; set; }
        public int Colocacao { get; set; }
        public int PontosColocacao { get; set; }
    }
}