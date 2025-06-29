using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using PokeTrackerApi.RunTime.Database;
using PokeTrackerApi.RunTime.Endpoints;
using PokeTrackerApi.RunTime.Models;
using PokeTrackerApi.RunTime.Models.Responses;
using PokeTrackerApi.RunTime.Utils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.Configure<JsonOptions>(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverterWithAttributeSupport(JsonNamingPolicy.CamelCase, allowIntegerValues: false));
});
builder.Services.AddSingleton<PokemonRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapPokemonEndpoints();
app.MapPokemonCountsEndpoints();

app.Run();
