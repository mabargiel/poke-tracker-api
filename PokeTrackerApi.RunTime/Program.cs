using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using PokeTrackerApi.RunTime.Database;
using PokeTrackerApi.RunTime.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//Services
builder.Services.Configure<JsonOptions>(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    options.JsonSerializerOptions.Converters.Add(
        new JsonStringEnumConverterWithAttributeSupport(JsonNamingPolicy.CamelCase, false));
});
builder.Services.AddSingleton<PokemonRepository>();

//Cors
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy => policy.AllowAnyMethod()
        .AllowAnyHeader()
        .WithOrigins("http://localhost:5173", "http://localhost:5174", "https://poketracker-gtbcane5bvb5a8ad.polandcentral-01.azurewebsites.net/api/", "https://poketracker.mbargiel.dev"));
});

var app = builder.Build();

app.UseCors();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapPokemonEndpoints();
app.MapPokemonCountsEndpoints();

app.Run();
