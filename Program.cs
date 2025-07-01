using ControleCampeonato.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Adiciona serviços necessários
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configura o context para usar SQLite
builder.Services.AddDbContext<ControleCampeonatoContext>(options =>
    options.UseSqlite("Data Source=controlecampeonato.db"));

var app = builder.Build();

app.UseDefaultFiles();    // Procura index.html
app.UseStaticFiles();     // Habilita arquivos de wwwroot

app.MapControllers();     // Mapeia controllers

app.Run();

