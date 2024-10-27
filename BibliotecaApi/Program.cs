using Microsoft.EntityFrameworkCore;
using BibliotecaApi.Data;
using BibliotecaApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Adiciona o contexto ao contêiner de serviços
builder.Services.AddDbContext<BibliotecaContext>(options =>
    options.UseSqlite("Data Source=biblioteca.db"));

// Adiciona serviços para o Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configura o Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Mapeia os endpoints CRUD
app.MapGet("/bibliotecas", async (BibliotecaContext db) => await db.Bibliotecas.ToListAsync());

app.MapGet("/bibliotecas/{id}", async (int id, BibliotecaContext db) =>
{
    return await db.Bibliotecas.FindAsync(id) is Biblioteca biblioteca ? Results.Ok(biblioteca) : Results.NotFound();
});

app.MapPost("/bibliotecas", async (Biblioteca biblioteca, BibliotecaContext db) =>
{
    db.Bibliotecas.Add(biblioteca);
    await db.SaveChangesAsync();
    return Results.Created($"/bibliotecas/{biblioteca.Id}", biblioteca);
});

app.MapPut("/bibliotecas/{id}", async (int id, Biblioteca bibliotecaAtualizada, BibliotecaContext db) =>
{
    var biblioteca = await db.Bibliotecas.FindAsync(id);
    if (biblioteca is null) return Results.NotFound();

    biblioteca.Nome = bibliotecaAtualizada.Nome;
    biblioteca.InicioFuncionamento = bibliotecaAtualizada.InicioFuncionamento;
    biblioteca.FimFuncionamento = bibliotecaAtualizada.FimFuncionamento;
    biblioteca.Inauguracao = bibliotecaAtualizada.Inauguracao;
    biblioteca.Contato = bibliotecaAtualizada.Contato;

    await db.SaveChangesAsync();
    return Results.Ok(biblioteca);
});

app.MapDelete("/bibliotecas/{id}", async (int id, BibliotecaContext db) =>
{
    var biblioteca = await db.Bibliotecas.FindAsync(id);
    if (biblioteca is null) return Results.NotFound();

    db.Bibliotecas.Remove(biblioteca);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.Run();
