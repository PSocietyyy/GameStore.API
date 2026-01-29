using GameStores.Api.Data;
using GameStores.Api.Endpoints;
using GameStores.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidation();

builder.AddGameStoreDb();

builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Register Service
builder.Services.AddScoped<AccountService>();
builder.Services.AddScoped<DeveloperService>();
builder.Services.AddScoped<GenreService>();
builder.Services.AddScoped<GameService>();

var app = builder.Build();
app.UseStaticFiles();
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
