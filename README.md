# 🏆 Controle de Campeonato

Sistema simples de controle de campeonato esportivo, desenvolvido como trabalho de faculdade. A aplicação permite:

- Cadastro de equipes e jogadores
- Registro de partidas
- Cadastramento de pontuações por equipe e jogador
- Visualização de pontuações registradas

## 🚀 Tecnologias Utilizadas

- **ASP.NET Core Web API**
- **C#**
- **HTML + CSS**
- **SQLite**
- **Entity Framework Core**

---

## 💡 Objetivo do Projeto

O objetivo é oferecer uma aplicação web onde seja possível simular o controle de um campeonato por rodadas/partidas, registrando:

- Times e jogadores participantes
- Kills, assistências, dano causado e mortes dos jogadores
- Colocação de cada time nas partidas
- Cálculo automático da pontuação da equipe por colocação + kills

---

## ⚙️ Pré-requisitos

Antes de rodar o projeto, certifique-se de ter o seguinte instalado:

- [.NET 6 SDK ou superior](https://dotnet.microsoft.com/download)
- Um navegador moderno (para abrir a interface HTML)
- Git (opcional, apenas se quiser clonar ou contribuir)
- Um editor como VS Code ou Visual Studio (recomendado)

---

## 🔧 Como rodar o projeto

1. Clone o repositório:
   ```bash
   git clone https://github.com/RenanMEleoterio/Projeto-Faculdade.git
   cd Projeto-Faculdade
   ```

2. Restaure os pacotes e compile:
   ```bash
   dotnet restore
   dotnet build
   ```

3. Rode a API:
   ```bash
   dotnet run
   ```

4. Abra o navegador e vá até:
   ```
   http://localhost:5121/index.html
   ```

---

## 🛠 Estrutura do Projeto

- `Controllers/`: Lida com as requisições HTTP
- `Models/`: Entidades do sistema
- `DTO/`: Objetos de transferência de dados
- `Data/`: Contexto do Entity Framework e configurações de banco
- `Migrations/`: Migrações do EF Core
- `wwwroot/`: Interface em HTML + CSS

---

## 🧠 Lógica de Pontuação

- A pontuação da **equipe** é baseada em:
  - Total de kills dos jogadores da equipe na partida
  - Colocação final (1º lugar = 10 pts, 2º = 8 pts, ...)

---

## 📝 Observações

Este projeto foi feito como atividade acadêmica. Algumas partes estão simplificadas e podem ser melhoradas com:

- Uso de autenticação/autorização
- Persistência em banco relacional mais robusto
- Front-end com framework moderno (React, Angular...)

---
