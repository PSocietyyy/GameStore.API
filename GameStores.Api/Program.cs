using GameStores.Api.Data;
using GameStores.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidation();

builder.AddGameStoreDb();

builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapAccountEndpoints();
app.MapDeveloperEndpoints();
app.MapGenreEndpoints();
app.MapGameEndpoints();

app.migrateDb();
app.Run();
