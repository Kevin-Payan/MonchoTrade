using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace monchotradeBackend.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    SecondLastName = table.Column<string>(type: "TEXT", nullable: true),
                    BirthDate = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Country = table.Column<string>(type: "TEXT", nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "BirthDate", "Country", "Email", "LastName", "Name", "PasswordHash", "PhoneNumber", "SecondLastName" },
                values: new object[,]
                {
                    { 1, new DateOnly(1990, 5, 15), "Mexico", "ana.martinezgomez@example.com", "Martinez", "Ana Luisa", "$2a$11$SfjI5GHM24/rxDcV3FqlMuHqM6fy//H7SLt8RfMvPSpaFPRiX4VkO", "+52 55 1234 5678", "Gomez" },
                    { 2, new DateOnly(1985, 8, 22), "Mexico", "carlos.gomezlopez@example.com", "Gomez", "Carlos", "$2a$11$8LDB6DZ4SRaVMD/Z2/6dNueYhBmIQ9YfIt3c/hH9JQZUE2DiAWVdy", "+52 33 9876 5432", "Lopez" },
                    { 3, new DateOnly(1992, 3, 10), "Mexico", "miguel.torresramirez@example.com", "Torres", "Miguel", "$2a$11$UrAOcKu1bTH6dxDfc18nXeJKWVNWg7O0pgTxSdA0e7ENuwPzx5FDC", "+52 81 5555 4444", "Ramirez" },
                    { 4, new DateOnly(1988, 11, 5), "Mexico", "laura.rodriguezfernandez@example.com", "Rodriguez", "Laura", "$2a$11$5ZT4G8ta8Je3btkuAeVuFeVYv1CTHxJbU63xHhY5cWEfvVFK2c3.a", "+52 55 2222 3333", "Fernandez" },
                    { 5, new DateOnly(1995, 7, 18), "Mexico", "juan.sanchezmartinez@example.com", "Sanchez", "Juan Carlos", "$2a$11$J69vWRTZybJYAISsE7WFxO.cPmTlDFAuzGfK5CdtQL.a7620ipJKe", "+52 33 7777 8888", "Martinez" },
                    { 6, new DateOnly(1987, 9, 30), "Mexico", "sofia.ramirezcastillo@example.com", "Ramirez", "Sofia", "$2a$11$AfOWzmsh.m64y8gzpFtmwuEAt.s3mnUW/ITZ04GYxWcX.9ZJBSoFW", "+52 81 4444 5555", "Castillo" },
                    { 7, new DateOnly(1993, 2, 14), "Mexico", "andres.lopezcastro@example.com", "Lopez", "Andres", "$2a$11$i9k5rm.0XGhyQ9A4ENN.X.cPD5hUKF2QYmhSlSKa0D5OGfz.RBLii", "+52 55 6666 7777", "Castro" },
                    { 8, new DateOnly(1991, 6, 25), "Mexico", "valentina.castroperez@example.com", "Castro", "Valentina", "$2a$11$qzflATPSOFU.M/0ko91/GOEqb2cEefV7vsp1BTUzdTbr9rOvqQfKS", "+52 33 1111 2222", "Perez" },
                    { 9, new DateOnly(1986, 12, 7), "Mexico", "pedro.jimenezmorales@example.com", "Jimenez", "Pedro", "$2a$11$Z//63u90J7KuF925iVGgJOrbigT6pBWVBkir70f95/ZLHx4HP8uL2", "+52 81 9999 0000", "Morales" },
                    { 10, new DateOnly(1994, 4, 12), "Mexico", "mariana.diazherrera@example.com", "Diaz", "Mariana", "$2a$11$IpaIMWXRwuqEZsWVt7NYFe23y/0O7u/ywHxFr5mp5hR/Jx9uwZ8qe", "+52 55 3333 4444", "Herrera" },
                    { 11, new DateOnly(1989, 10, 20), "Mexico", "javier.moralesjimenez@example.com", "Morales", "Javier", "$2a$11$Kou1b5hBzhwrcTv8xpPevu4vxcYxmy0nLgczMO9A6/c/C7DoGjSCO", "+52 33 5555 6666", "Jimenez" },
                    { 12, new DateOnly(1996, 1, 8), "Mexico", "gabriela.fernandeztorres@example.com", "Fernandez", "Gabriela", "$2a$11$9FZOEbkaRPFigyWoG0Y1NelmNWwluBjoVZoiC1b3wdDGY8s9RbyJy", "+52 81 2222 3333", "Torres" },
                    { 13, new DateOnly(1984, 8, 16), "Mexico", "tomas.herreramendoza@example.com", "Herrera", "Tomas", "$2a$11$clPFPyV7UKnnrmqy5xUphOJ3XmleQN0beHjCd10SyfRiFkSvYaVTK", "+52 55 7777 8888", "Mendoza" },
                    { 14, new DateOnly(1997, 5, 3), "Mexico", "natalia.mendozarodriguez@example.com", "Mendoza", "Natalia", "$2a$11$SeIt78dborCj071Xbd.Zte4ctT5uMPl1MAbG0AnDWmPIRuVz5/Oai", "+52 33 4444 5555", "Rodriguez" },
                    { 15, new DateOnly(1983, 11, 22), "Mexico", "luis.ruizsanchez@example.com", "Ruiz", "Luis Fernando", "$2a$11$8txbI0wGPjT8lnFI9Uw/W.ApPra8tvPJpMtzqdkFunzIorpRq0pLK", "+52 81 6666 7777", "Sanchez" },
                    { 16, new DateOnly(1990, 7, 7), "Mexico", "elena.gonzalezreyes@example.com", "Gonzalez", "Elena", "$2a$11$aH.oHiQAaK5FRjslM51qRex3b7Pobf.F0Pc9LZ.4UDx/Jn4FMMjpS", "+52 55 8888 9999", "Reyes" },
                    { 17, new DateOnly(1986, 3, 19), "Mexico", "ricardo.reyesgarcia@example.com", "Reyes", "Ricardo", "$2a$11$c6LQPb/C6GdUWIUs7Us4terHpE36NUY6WBONQFSv6PrIXOMCgk6za", "+52 33 0000 1111", "Garcia" },
                    { 18, new DateOnly(1993, 9, 14), "Mexico", "isabel.garciamoreno@example.com", "Garcia", "Isabel", "$2a$11$cqyCEV0eGn5zkZ5OMjm/uu9tCw.SfuXmXjW8xHvDLuFbnlM.KHjXq", "+52 81 3333 4444", "Moreno" },
                    { 19, new DateOnly(1988, 6, 30), "Mexico", "daniel.morenosilva@example.com", "Moreno", "Daniel", "$2a$11$JwXbR7R/bCJyBj79wXgLyu2THNCqoAYpeIBdMc7.2TFbYDIE905xO", "+52 55 5555 6666", "Silva" },
                    { 20, new DateOnly(1995, 12, 5), "Mexico", "carmen.silvanavarro@example.com", "Silva", "Carmen", "$2a$11$iGMDATQMh5HadtqYQFJFbudnIAboGwjOOMqzgtjf3iY61BQbNpBPG", "+52 33 2222 3333", "Navarro" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
