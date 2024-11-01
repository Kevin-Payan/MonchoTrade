using monchotradebackend.data;
using monchotradebackend.Interface;
using monchotradebackend.service;
using monchotradebackend.repository;

using Microsoft.EntityFrameworkCore;
using authbackend.Data;



var builder = WebApplication.CreateBuilder(args);
var connString = builder.Configuration.GetConnectionString("MonchoDb"); 
builder.Services.AddDbContext<MonchoDbContext>(options => options.UseSqlite(connString)); //Ajustar para sqlite o sqlserver 
builder.Services.AddScoped<IPasswordHashingService, PasswordHashingService>();   //Asi se inicializa lo de los servicios
builder.Services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
builder.Services.AddEndpointsApiExplorer(); 
builder.Services.AddSwaggerGen(); //Swagger support 

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("WebAccess", builder =>
    {
        builder.WithOrigins("http://localhost:5173")
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRouting();
app.UseCors("WebAccess");

// Mapea las rutas de los controladores
app.MapControllers();

await app.MigrateDbAsync(); 

app.Run();





