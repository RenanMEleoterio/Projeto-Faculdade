using Microsoft.EntityFrameworkCore;
using ControleCampeonato.Models;

namespace ControleCampeonato.Data
{
    public class ControleCampeonatoContext : DbContext
    {
        public ControleCampeonatoContext(DbContextOptions<ControleCampeonatoContext> options) : base(options)
        {
        }

        public DbSet<Jogador> Jogadores { get; set; }
        public DbSet<Equipe> Equipes { get; set; }
        public DbSet<Partida> Partidas { get; set; }
        public DbSet<PontuacaoEquipe> PontuacoesEquipes { get; set; }
        public DbSet<PontuacaoJogador> PontuacoesJogadores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração da chave composta para PontuacaoEquipe (PartidaId + EquipeId)
            modelBuilder.Entity<PontuacaoEquipe>()
                .HasKey(pe => new { pe.IdPartida, pe.IdEquipe });

            modelBuilder.Entity<PontuacaoEquipe>()
                .HasOne(pe => pe.Partida)
                .WithMany(p => p.PontuacoesEquipes)
                .HasForeignKey(pe => pe.IdPartida);

            modelBuilder.Entity<PontuacaoEquipe>()
                .HasOne(pe => pe.Equipe)
                .WithMany(e => e.Pontuacoes)
                .HasForeignKey(pe => pe.IdEquipe);

            // Configuração da chave composta para PontuacaoJogador (IdPartida + IdJogador)
            modelBuilder.Entity<PontuacaoJogador>()
                .HasKey(pj => new { pj.IdPartida, pj.IdJogador });

            modelBuilder.Entity<PontuacaoJogador>()
                .HasOne(pj => pj.Partida)
                .WithMany()
                .HasForeignKey(pj => pj.IdPartida);

            modelBuilder.Entity<PontuacaoJogador>()
                .HasOne(pj => pj.Jogador)
                .WithMany(j => j.Pontuacoes)
                .HasForeignKey(pj => pj.IdJogador);

            modelBuilder.Entity<PontuacaoJogador>()
                .HasOne(pj => pj.Equipe)
                .WithMany()
                .HasForeignKey(pj => pj.IdEquipe);

            // Relação Jogador-Equipe
            modelBuilder.Entity<Jogador>()
                .HasOne(j => j.Equipe)
                .WithMany()
                .HasForeignKey(j => j.EquipeId);
        }
    }
}
