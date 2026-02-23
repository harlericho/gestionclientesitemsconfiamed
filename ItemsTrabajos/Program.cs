using ItemsTrabajos.Repositories;
using ItemsTrabajos.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// registrar el repositorio y servicio de items para inyección de dependencias
builder.Services.AddSingleton<ItemRepository>();
builder.Services.AddSingleton<ItemService>();
builder.Services.AddScoped<AsignacionService>(); // se registra como scoped porque depende del cliente http, que no puede ser singleton
builder.Services.AddHttpClient(); // se registra el cliente http para que pueda ser inyectado en el servicio de asignación, y así poder comunicarse con el microservicio de gestion usuarios
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
