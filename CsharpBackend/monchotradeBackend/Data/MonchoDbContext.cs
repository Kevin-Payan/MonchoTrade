using monchotradebackend.models.entities;
using Microsoft.EntityFrameworkCore;
using monchotradebackend.Interface;

namespace monchotradebackend.data;

public class MonchoDbContext: DbContext{


    private readonly IPasswordHashingService _passwordHashingService;
    private readonly ILogger<MonchoDbContext> _logger;

    public MonchoDbContext(DbContextOptions<MonchoDbContext> options,IPasswordHashingService passwordHashingService, ILogger<MonchoDbContext> logger) : base(options){
        _passwordHashingService = passwordHashingService ?? throw new ArgumentNullException(nameof(passwordHashingService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
  
    }

    public DbSet<User> Users {get;set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //Hago seed de 20 usuario, todos mismo password
        modelBuilder.Entity<User>().HasData(
        new User { Id = 1, Name = "Ana Luisa", LastName = "Martinez", SecondLastName = "Gomez", BirthDate = new DateOnly(1990, 5, 15), Email = "ana.martinezgomez@example.com", Country = "Mexico", PhoneNumber = "+52 55 1234 5678", PasswordHash = _passwordHashingService.HashPassword("Mexicali#11") },
        new User { Id = 2, Name = "Carlos", LastName = "Gomez", SecondLastName = "Lopez", BirthDate = new DateOnly(1985, 8, 22), Email = "carlos.gomezlopez@example.com", Country = "Mexico", PhoneNumber = "+52 33 9876 5432", PasswordHash = _passwordHashingService.HashPassword("Mexicali#11") },
        new User { Id = 3, Name = "Miguel", LastName = "Torres", SecondLastName = "Ramirez", BirthDate = new DateOnly(1992, 3, 10), Email = "miguel.torresramirez@example.com", Country = "Mexico", PhoneNumber = "+52 81 5555 4444", PasswordHash = _passwordHashingService.HashPassword("Mexicali#11") },
        new User { Id = 4, Name = "Laura", LastName = "Rodriguez", SecondLastName = "Fernandez", BirthDate = new DateOnly(1988, 11, 5), Email = "laura.rodriguezfernandez@example.com", Country = "Mexico", PhoneNumber = "+52 55 2222 3333", PasswordHash = _passwordHashingService.HashPassword("Mexicali#11") },
        new User { Id = 5, Name = "Juan Carlos", LastName = "Sanchez", SecondLastName = "Martinez", BirthDate = new DateOnly(1995, 7, 18), Email = "juan.sanchezmartinez@example.com", Country = "Mexico", PhoneNumber = "+52 33 7777 8888", PasswordHash = _passwordHashingService.HashPassword("Mexicali#11") },
        new User { Id = 6, Name = "Sofia", LastName = "Ramirez", SecondLastName = "Castillo", BirthDate = new DateOnly(1987, 9, 30), Email = "sofia.ramirezcastillo@example.com", Country = "Mexico", PhoneNumber = "+52 81 4444 5555", PasswordHash = _passwordHashingService.HashPassword("Mexicali#11") },
        new User { Id = 7, Name = "Andres", LastName = "Lopez", SecondLastName = "Castro", BirthDate = new DateOnly(1993, 2, 14), Email = "andres.lopezcastro@example.com", Country = "Mexico", PhoneNumber = "+52 55 6666 7777", PasswordHash = _passwordHashingService.HashPassword("Mexicali#11") },
        new User { Id = 8, Name = "Valentina", LastName = "Castro", SecondLastName = "Perez", BirthDate = new DateOnly(1991, 6, 25), Email = "valentina.castroperez@example.com", Country = "Mexico", PhoneNumber = "+52 33 1111 2222", PasswordHash = _passwordHashingService.HashPassword("Mexicali#11") },
        new User { Id = 9, Name = "Pedro", LastName = "Jimenez", SecondLastName = "Morales", BirthDate = new DateOnly(1986, 12, 7), Email = "pedro.jimenezmorales@example.com", Country = "Mexico", PhoneNumber = "+52 81 9999 0000", PasswordHash = _passwordHashingService.HashPassword("Mexicali#11") },
        new User { Id = 10, Name = "Mariana", LastName = "Diaz", SecondLastName = "Herrera", BirthDate = new DateOnly(1994, 4, 12), Email = "mariana.diazherrera@example.com", Country = "Mexico", PhoneNumber = "+52 55 3333 4444", PasswordHash = _passwordHashingService.HashPassword("Mexicali#11") },
        new User { Id = 11, Name = "Javier", LastName = "Morales", SecondLastName = "Jimenez", BirthDate = new DateOnly(1989, 10, 20), Email = "javier.moralesjimenez@example.com", Country = "Mexico", PhoneNumber = "+52 33 5555 6666", PasswordHash = _passwordHashingService.HashPassword("Mexicali#11") },
        new User { Id = 12, Name = "Gabriela", LastName = "Fernandez", SecondLastName = "Torres", BirthDate = new DateOnly(1996, 1, 8), Email = "gabriela.fernandeztorres@example.com", Country = "Mexico", PhoneNumber = "+52 81 2222 3333", PasswordHash = _passwordHashingService.HashPassword("Mexicali#11") },
        new User { Id = 13, Name = "Tomas", LastName = "Herrera", SecondLastName = "Mendoza", BirthDate = new DateOnly(1984, 8, 16), Email = "tomas.herreramendoza@example.com", Country = "Mexico", PhoneNumber = "+52 55 7777 8888", PasswordHash = _passwordHashingService.HashPassword("Mexicali#11") },
        new User { Id = 14, Name = "Natalia", LastName = "Mendoza", SecondLastName = "Rodriguez", BirthDate = new DateOnly(1997, 5, 3), Email = "natalia.mendozarodriguez@example.com", Country = "Mexico", PhoneNumber = "+52 33 4444 5555", PasswordHash = _passwordHashingService.HashPassword("Mexicali#11") },
        new User { Id = 15, Name = "Luis Fernando", LastName = "Ruiz", SecondLastName = "Sanchez", BirthDate = new DateOnly(1983, 11, 22), Email = "luis.ruizsanchez@example.com", Country = "Mexico", PhoneNumber = "+52 81 6666 7777", PasswordHash = _passwordHashingService.HashPassword("Mexicali#11") },
        new User { Id = 16, Name = "Elena", LastName = "Gonzalez", SecondLastName = "Reyes", BirthDate = new DateOnly(1990, 7, 7), Email = "elena.gonzalezreyes@example.com", Country = "Mexico", PhoneNumber = "+52 55 8888 9999", PasswordHash = _passwordHashingService.HashPassword("Mexicali#11") },
        new User { Id = 17, Name = "Ricardo", LastName = "Reyes", SecondLastName = "Garcia", BirthDate = new DateOnly(1986, 3, 19), Email = "ricardo.reyesgarcia@example.com", Country = "Mexico", PhoneNumber = "+52 33 0000 1111", PasswordHash = _passwordHashingService.HashPassword("Mexicali#11") },
        new User { Id = 18, Name = "Isabel", LastName = "Garcia", SecondLastName = "Moreno", BirthDate = new DateOnly(1993, 9, 14), Email = "isabel.garciamoreno@example.com", Country = "Mexico", PhoneNumber = "+52 81 3333 4444", PasswordHash = _passwordHashingService.HashPassword("Mexicali#11") },
        new User { Id = 19, Name = "Daniel", LastName = "Moreno", SecondLastName = "Silva", BirthDate = new DateOnly(1988, 6, 30), Email = "daniel.morenosilva@example.com", Country = "Mexico", PhoneNumber = "+52 55 5555 6666", PasswordHash = _passwordHashingService.HashPassword("Mexicali#11") },
        new User { Id = 20, Name = "Carmen", LastName = "Silva", SecondLastName = "Navarro", BirthDate = new DateOnly(1995, 12, 5), Email = "carmen.silvanavarro@example.com", Country = "Mexico", PhoneNumber = "+52 33 2222 3333", PasswordHash = _passwordHashingService.HashPassword("Mexicali#11") }
        );


        //Soluciona un problema con sqlite
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            entity.SetSchema(null);
        }
    }


}