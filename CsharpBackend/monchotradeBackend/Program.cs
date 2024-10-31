using monchotradebackend.data;
using monchotradebackend.Interface;
using monchotradebackend.service;

using Microsoft.EntityFrameworkCore;
using authbackend.Data;



var builder = WebApplication.CreateBuilder(args);
var connString = builder.Configuration.GetConnectionString("MonchoDb"); 
builder.Services.AddDbContext<MonchoDbContext>(options => options.UseSqlite(connString)); //Ajustar para sqlite o sqlserver 
builder.Services.AddScoped<IPasswordHashingService, PasswordHashingService>();   //Asi se inicializa lo de los servicios
builder.Services.AddEndpointsApiExplorer(); 
builder.Services.AddSwaggerGen(); //Swagger support 

builder.Services.AddControllers();



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRouting();

// Mapea las rutas de los controladores
app.MapControllers();

await app.MigrateDbAsync(); 

app.Run();





