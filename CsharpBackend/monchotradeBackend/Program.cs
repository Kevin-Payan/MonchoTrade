using monchotradebackend.data;
using monchotradebackend.Interface;
using monchotradebackend.service;
using monchotradebackend.repository;

using Microsoft.EntityFrameworkCore;
using authbackend.Data;
using Microsoft.Extensions.FileProviders;



var builder = WebApplication.CreateBuilder(args);
var connString = builder.Configuration.GetConnectionString("MonchoDb"); 
builder.Services.AddDbContext<MonchoDbContext>(options => options.UseSqlServer(connString)); //Ajustar para sqlite o sqlserver 
 //Asi se inicializa lo de los servicios 
builder.Services.AddScoped<IPasswordHashingService, PasswordHashingService>();  //Password service  
builder.Services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));      //Repository service
builder.Services.AddTransient<IFileService, FileService>();                     //File service

//Swagger support 
builder.Services.AddEndpointsApiExplorer(); 
builder.Services.AddSwaggerGen(); 

builder.Services.AddControllers();


//Deja que la pagina web se comunique con la api
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


//Static files para ver las imagenes

app.UseStaticFiles();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "wwwroot")),
    RequestPath = ""
});


app.UseRouting();
app.UseCors("WebAccess");

// Mapea las rutas de los controladores
app.MapControllers();

await app.MigrateDbAsync(); 

app.Run();





