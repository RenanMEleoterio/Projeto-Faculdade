using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControleCampeonato.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Equipes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Partidas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Data = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partidas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Jogadores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    EquipeId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jogadores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Jogadores_Equipes_EquipeId",
                        column: x => x.EquipeId,
                        principalTable: "Equipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PontuacoesEquipes",
                columns: table => new
                {
                    IdPartida = table.Column<int>(type: "INTEGER", nullable: false),
                    IdEquipe = table.Column<int>(type: "INTEGER", nullable: false),
                    TotalKills = table.Column<int>(type: "INTEGER", nullable: false),
                    Colocacao = table.Column<int>(type: "INTEGER", nullable: false),
                    PontosColocacao = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PontuacoesEquipes", x => new { x.IdPartida, x.IdEquipe });
                    table.ForeignKey(
                        name: "FK_PontuacoesEquipes_Equipes_IdEquipe",
                        column: x => x.IdEquipe,
                        principalTable: "Equipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PontuacoesEquipes_Partidas_IdPartida",
                        column: x => x.IdPartida,
                        principalTable: "Partidas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PontuacoesJogadores",
                columns: table => new
                {
                    IdPartida = table.Column<int>(type: "INTEGER", nullable: false),
                    IdJogador = table.Column<int>(type: "INTEGER", nullable: false),
                    IdEquipe = table.Column<int>(type: "INTEGER", nullable: false),
                    Kills = table.Column<int>(type: "INTEGER", nullable: false),
                    Mortes = table.Column<int>(type: "INTEGER", nullable: false),
                    Dano = table.Column<float>(type: "REAL", nullable: false),
                    Assistencias = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PontuacoesJogadores", x => new { x.IdPartida, x.IdJogador });
                    table.ForeignKey(
                        name: "FK_PontuacoesJogadores_Equipes_IdEquipe",
                        column: x => x.IdEquipe,
                        principalTable: "Equipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PontuacoesJogadores_Jogadores_IdJogador",
                        column: x => x.IdJogador,
                        principalTable: "Jogadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PontuacoesJogadores_Partidas_IdPartida",
                        column: x => x.IdPartida,
                        principalTable: "Partidas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Jogadores_EquipeId",
                table: "Jogadores",
                column: "EquipeId");

            migrationBuilder.CreateIndex(
                name: "IX_PontuacoesEquipes_IdEquipe",
                table: "PontuacoesEquipes",
                column: "IdEquipe");

            migrationBuilder.CreateIndex(
                name: "IX_PontuacoesJogadores_IdEquipe",
                table: "PontuacoesJogadores",
                column: "IdEquipe");

            migrationBuilder.CreateIndex(
                name: "IX_PontuacoesJogadores_IdJogador",
                table: "PontuacoesJogadores",
                column: "IdJogador");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PontuacoesEquipes");

            migrationBuilder.DropTable(
                name: "PontuacoesJogadores");

            migrationBuilder.DropTable(
                name: "Jogadores");

            migrationBuilder.DropTable(
                name: "Partidas");

            migrationBuilder.DropTable(
                name: "Equipes");
        }
    }
}
