using Habitos.Application.Interface;
using Habitos.Application.RegrasDeNegocio;
using Habitos.Infrastructure.Repositorio;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IntRepositorioHabito, HabitoRepositorio>();
builder.Services.AddSingleton<CalendarioServico>();
builder.Services.AddTransient<GetCalendarioMensal>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Habitos API");
    c.RoutePrefix = string.Empty;
});

app.MapGet("/calendario/{ano}/{mes}", async (
    int ano,
    int mes,
    GetCalendarioMensal calendario) =>
{
    var resultado = await calendario.Sincronizar(ano, mes);
    return Results.Ok(resultado);
})
.WithTags("Calendario")
.WithSummary("Gera o calendário de hábitos para um mês específico");

app.Run();