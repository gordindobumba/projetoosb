using Habitos.Application.Interface;
using Habitos.Application.RegrasDeNegocio;
using Habitos.Infrastructure.Repositorio;

// -------------- BUILDER ---------------
var builder = WebApplication.CreateBuilder(args);

// ------------- SERVIÇOS ---------------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IntRepositorioHabito, HabitoRepositorio>();
builder.Services.AddSingleton<IntRepositorioHabitoCompleto, HabitoCompletoRepositorio>();

builder.Services.AddSingleton<CalendarioServico>();
builder.Services.AddTransient<GetCalendarioMensal>();

var app = builder.Build();

// -------------- SWAGGER ---------------
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Calendario API");
    c.RoutePrefix = string.Empty;
});

// ---------- GET: CALENDARIO -----------
app.MapGet("/calendario/{ano}/{mes}", async (int ano, int mes, GetCalendarioMensal calendario) =>
{
    var resultado = await calendario.Assincronizar(ano, mes);
    return Results.Ok(resultado);
})
.WithTags("Calendario")
.WithSummary("Gera o calendário de hábitos para um mês específico");

app.Run();