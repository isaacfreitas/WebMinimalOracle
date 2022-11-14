using Microsoft.EntityFrameworkCore;
using WebMinimalOracle.Config;
using WebMinimalOracle.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer(); //abre o navegador enquanto o projeto executa 
builder.Services.AddSwaggerGen();

var stringConexao = "User Id=SYSTEM;Password=izq1998hu3;Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST =localhost)(PORT=1521)))(CONNECT_DATA=(SERVICE_NAME=XE)))";
builder.Services.AddDbContext<Contexto>
    (options => options.UseOracle(stringConexao));

var app = builder.Build();
app.UseSwagger();

//API post  
app.MapPost("AdicionaProduto", async (Produto produto, Contexto contexto) =>
{
    contexto.Produto.Add(produto);
    await contexto.SaveChangesAsync();
});

//API delete
app.MapPost("ExcluirProduto/{id}", async (int id, Contexto contexto) =>
{
    var produto = await contexto.Produto.FirstOrDefaultAsync(p => p.Id == id); // p de produto/id do produto == id
    if (produto != null)
    {
        contexto.Produto.Remove(produto); //remove
        await contexto.SaveChangesAsync(); //espera excluir a ação do async
    }
});
//API getAll
app.MapGet("ListarProdutos", async (Contexto contexto) =>
{
    return await contexto.Produto.ToListAsync();
});

//API getProductById
app.MapGet("ObterProduto/{id}", async (int id, Contexto contexto) =>
{
    return await contexto.Produto.FirstOrDefaultAsync(p => p.Id == id);
});



app.UseSwaggerUI();
app.Run();