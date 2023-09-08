using Microsoft.EntityFrameworkCore;
using NegocioApi.Models;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MiNegocioBdContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("CadenaSQL")));

//Para evitar las referencias ciclicas
builder.Services.AddControllers().AddJsonOptions(option => option.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);


//Activando CORS

var misReglasCors = "ReglasCors";
builder.Services.AddCors(options => {
    options.AddPolicy(name: misReglasCors, builder =>
    {
        builder.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();
    });
});


var app = builder.Build();




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.UseCors(misReglasCors);

app.MapControllers();

app.Run();
