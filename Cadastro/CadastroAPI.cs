using Cadastro.Data;
using Cadastro.DTOs;
using Cadastro.Servicos;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ---------- BANCO DE DADOS ----------
builder.Services.AddDbContext<BancoDados>(options =>
    options.UseSqlite("Data Source=contas_cadastradas.db"));

// ---------- SERVIÇOS ----------
builder.Services.AddScoped<Autenticador>();
builder.Services.AddScoped<ServiçoEmail>();

// ---------- SWAGGER ----------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// ----------- ENDPOINT: CADASTRAR -----------
app.MapPost("/cadastrar", async(CadastrarDTO dto, Autenticador aut) =>
{
    var msg = await aut.Cadastrar(dto);
    return Results.Ok(msg);
});

// ----------- ENDPOINT: LOGIN ---------------
app.MapPost("/login", async(LoginDTO dto, Autenticador aut) =>
{
    var token = await aut.Login(dto);
    return token == null
        ? Results.BadRequest("Login ou senha incorretos.")
        : Results.Ok($"Login feito com sucesso. {new { token }}");
});

// ----------- ENDPOINT: LOGOUT --------------
app.MapPost("/logout", async(string token, Autenticador aut) =>
{
    var ok = await aut.Logout(token);
    return ok
        ? Results.Ok("Logout feito com sucesso.")
        : Results.BadRequest("Token inválido ou usuário já deslogado.");
});

// --------- ENDPOINT: EDITAR PERFIL ---------
app.MapPost("/editar-perfil", async(EditarPerfilDTO dto, Autenticador aut) =>
{
    var mensagem = aut.EditarPerfil(dto.Token, dto.NovoNome, dto.SenhaAtual, dto.NovaSenha);

    return Results.Ok(mensagem);
});

// -------- ENDPOINT: RECUPERAR SENHA --------
app.MapPost("/esqueci-senha", async(EsqueciSenhaDTO dto, Autenticador aut) =>
{
   var codigo = await aut.GerarCodigo(dto.Email);
   
   return Results.Ok(new {msg = codigo});
});

app.MapPost("/resetar-senha", async (ReiniciarSenhaDTO dto, Autenticador auth) =>
{
    var resultado = await auth.ReiniciarSenha(dto.Codigo, dto.Senha);
    return Results.Ok(new {msg = resultado});
});

app.Run();