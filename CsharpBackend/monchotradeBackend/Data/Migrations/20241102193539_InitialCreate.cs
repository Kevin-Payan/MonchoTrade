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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SecondLastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProfileImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfileImages_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImages_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "BirthDate", "Country", "Email", "LastName", "Name", "PasswordHash", "PhoneNumber", "SecondLastName" },
                values: new object[,]
                {
                    { 1, new DateOnly(1990, 5, 15), "Mexico", "ana.martinezgomez@example.com", "Martinez", "Ana Luisa", "$2a$11$keFNqsZ2FFMnHMzwmg6Rku/Nn9pT7TVq/LztlRvxaFSAjEutfnkzi", "+52 55 1234 5678", "Gomez" },
                    { 2, new DateOnly(1985, 8, 22), "Mexico", "carlos.gomezlopez@example.com", "Gomez", "Carlos", "$2a$11$WxlNeBmFBcOaVI/iajaej.QBYvk1QmzxlBq1h/ytJYVlSXxTMAtT6", "+52 33 9876 5432", "Lopez" },
                    { 3, new DateOnly(1992, 3, 10), "Mexico", "miguel.torresramirez@example.com", "Torres", "Miguel", "$2a$11$t5LphtCsYZIGyShbOH3YKOoTZwC8ggTqDpsn6dqdbc5RWIhG9Nml6", "+52 81 5555 4444", "Ramirez" },
                    { 4, new DateOnly(1988, 11, 5), "Mexico", "laura.rodriguezfernandez@example.com", "Rodriguez", "Laura", "$2a$11$zPkDmz45DtaZ620BRu.73OiacFKuEYo.ssB1kZpyPRBAXYjEAzUZO", "+52 55 2222 3333", "Fernandez" },
                    { 5, new DateOnly(1995, 7, 18), "Mexico", "juan.sanchezmartinez@example.com", "Sanchez", "Juan Carlos", "$2a$11$J6bICyoShMPY8/eZCTCajO12P.fdZClfL4jZK47AGFQVfjJyMBLKO", "+52 33 7777 8888", "Martinez" },
                    { 6, new DateOnly(1987, 9, 30), "Mexico", "sofia.ramirezcastillo@example.com", "Ramirez", "Sofia", "$2a$11$IwQVi6y85kzeTYm/RI0MHOHn/eyZj4Q4rcLpqvgIienEd3TJHJuti", "+52 81 4444 5555", "Castillo" },
                    { 7, new DateOnly(1993, 2, 14), "Mexico", "andres.lopezcastro@example.com", "Lopez", "Andres", "$2a$11$hOBKxqUHGhR3KjLccNQdM.9pQFm4RPhfY/tE32/zp78v4FMs64MzS", "+52 55 6666 7777", "Castro" },
                    { 8, new DateOnly(1991, 6, 25), "Mexico", "valentina.castroperez@example.com", "Castro", "Valentina", "$2a$11$emC98So.wHi2kKfRLaNESuJ5FoLWuheZO7DBgBCrvqzZJl0XZAZm6", "+52 33 1111 2222", "Perez" },
                    { 9, new DateOnly(1986, 12, 7), "Mexico", "pedro.jimenezmorales@example.com", "Jimenez", "Pedro", "$2a$11$DD.UZQwOVdMRmupttLuQn.av8GcFHkRqXSmw2AbmITEIdkeOalwJW", "+52 81 9999 0000", "Morales" },
                    { 10, new DateOnly(1994, 4, 12), "Mexico", "mariana.diazherrera@example.com", "Diaz", "Mariana", "$2a$11$1ZZlhc.Cjn4QPoC3mcEJ..3q/tds.0PaMUDGLyIFC75IpsE6t/X8S", "+52 55 3333 4444", "Herrera" },
                    { 11, new DateOnly(1989, 10, 20), "Mexico", "javier.moralesjimenez@example.com", "Morales", "Javier", "$2a$11$op1LlJr7V1FXtjgKuq1OguRAwet1rpOKI7a1M8DC7LilOXt/GUnDC", "+52 33 5555 6666", "Jimenez" },
                    { 12, new DateOnly(1996, 1, 8), "Mexico", "gabriela.fernandeztorres@example.com", "Fernandez", "Gabriela", "$2a$11$DvvB6KuutJEBQyUN37.YHeodW9eiugvAztQOrGGLmproleEf.1RFu", "+52 81 2222 3333", "Torres" },
                    { 13, new DateOnly(1984, 8, 16), "Mexico", "tomas.herreramendoza@example.com", "Herrera", "Tomas", "$2a$11$PzFG5Sn7xL8lyFeH.VhZMudZwxpEHlbaakLFm4KSx.x.zRnjRscPa", "+52 55 7777 8888", "Mendoza" },
                    { 14, new DateOnly(1997, 5, 3), "Mexico", "natalia.mendozarodriguez@example.com", "Mendoza", "Natalia", "$2a$11$bOW91KloFpT1G2Duds/ewOYLEZ.zvuNFln9ymQQ2cm1n/P6AvlfpK", "+52 33 4444 5555", "Rodriguez" },
                    { 15, new DateOnly(1983, 11, 22), "Mexico", "luis.ruizsanchez@example.com", "Ruiz", "Luis Fernando", "$2a$11$oCl1A4w1jd4/QFjhtg0WaOGyKAGMn6QjIaIsczF86jH6RmR3VXvCW", "+52 81 6666 7777", "Sanchez" },
                    { 16, new DateOnly(1990, 7, 7), "Mexico", "elena.gonzalezreyes@example.com", "Gonzalez", "Elena", "$2a$11$8twugGYYG50YOudjuPYbZut2vk1ZVcAB13R.Il.6Rw.4ZfQxQT2xK", "+52 55 8888 9999", "Reyes" },
                    { 17, new DateOnly(1986, 3, 19), "Mexico", "ricardo.reyesgarcia@example.com", "Reyes", "Ricardo", "$2a$11$Pphs2ZJ0PSeGHvUGQx3HJebVZthyBoAJB3kQq1XXp9flJe.SHt8ci", "+52 33 0000 1111", "Garcia" },
                    { 18, new DateOnly(1993, 9, 14), "Mexico", "isabel.garciamoreno@example.com", "Garcia", "Isabel", "$2a$11$L3Yt58VFFtxhQmEYNEPmkO1SVc6qx77/bCPX8fp0QuOAW0Vfm2zNC", "+52 81 3333 4444", "Moreno" },
                    { 19, new DateOnly(1988, 6, 30), "Mexico", "daniel.morenosilva@example.com", "Moreno", "Daniel", "$2a$11$d.ebwuu7aPY0yh0IhKWRFO6jjDxHJ.NhElq5QWjMn0DF0bjRRCdue", "+52 55 5555 6666", "Silva" },
                    { 20, new DateOnly(1995, 12, 5), "Mexico", "carmen.silvanavarro@example.com", "Silva", "Carmen", "$2a$11$CWwc3Wjo4QmAmUdaIlUJFehtlh7NALAAQJrKPCdeR8XTxAZLY63Pq", "+52 33 2222 3333", "Navarro" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "CreatedAt", "Description", "IsActive", "Name", "Quantity", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { 1, "Clothing", new DateTime(2024, 11, 2, 12, 35, 39, 373, DateTimeKind.Local).AddTicks(6324), "Classic blue jeans for everyday wear", true, "Men's Jeans", 10, null, 1 },
                    { 2, "Clothing", new DateTime(2024, 11, 2, 12, 35, 39, 373, DateTimeKind.Local).AddTicks(6329), "Comfortable cotton t-shirt, perfect for casual outings", true, "Casual T-Shirt", 5, null, 1 },
                    { 3, "Electronics", new DateTime(2024, 11, 2, 12, 35, 39, 373, DateTimeKind.Local).AddTicks(6379), "Noise-cancelling headphones with high-quality sound", true, "Wireless Headphones", 8, null, 2 },
                    { 4, "Electronics", new DateTime(2024, 11, 2, 12, 35, 39, 373, DateTimeKind.Local).AddTicks(6390), "Portable speaker with long battery life", true, "Bluetooth Speaker", 12, null, 2 },
                    { 5, "Footwear", new DateTime(2024, 11, 2, 12, 35, 39, 373, DateTimeKind.Local).AddTicks(6395), "Comfortable and durable sports shoes", true, "Sports Sneakers", 7, null, 3 },
                    { 6, "Footwear", new DateTime(2024, 11, 2, 12, 35, 39, 373, DateTimeKind.Local).AddTicks(6405), "Lightweight running shoes for everyday exercise", true, "Running Shoes", 10, null, 3 },
                    { 7, "Accessories", new DateTime(2024, 11, 2, 12, 35, 39, 373, DateTimeKind.Local).AddTicks(6410), "Fast-charging USB-C charger", true, "Smartphone Charger", 15, null, 4 },
                    { 8, "Accessories", new DateTime(2024, 11, 2, 12, 35, 39, 373, DateTimeKind.Local).AddTicks(6428), "10,000mAh power bank for mobile devices", true, "Power Bank", 9, null, 4 },
                    { 9, "Clothing", new DateTime(2024, 11, 2, 12, 35, 39, 373, DateTimeKind.Local).AddTicks(6433), "Stylish white formal shirt, perfect for office", true, "Formal Shirt", 11, null, 5 },
                    { 10, "Clothing", new DateTime(2024, 11, 2, 12, 35, 39, 373, DateTimeKind.Local).AddTicks(6437), "Comfortable black dress pants for formal events", true, "Dress Pants", 6, null, 5 },
                    { 11, "Office Supplies", new DateTime(2024, 11, 2, 12, 35, 39, 373, DateTimeKind.Local).AddTicks(6442), "Ergonomic stand for laptops", true, "Laptop Stand", 8, null, 6 },
                    { 12, "Office Supplies", new DateTime(2024, 11, 2, 12, 35, 39, 373, DateTimeKind.Local).AddTicks(6447), "Compact wireless mouse for convenience", true, "Wireless Mouse", 13, null, 6 },
                    { 13, "Accessories", new DateTime(2024, 11, 2, 12, 35, 39, 373, DateTimeKind.Local).AddTicks(6451), "Sporty digital watch with water resistance", true, "Digital Watch", 14, null, 7 },
                    { 14, "Accessories", new DateTime(2024, 11, 2, 12, 35, 39, 373, DateTimeKind.Local).AddTicks(6456), "Tracks steps, calories, and more", true, "Fitness Tracker", 5, null, 7 },
                    { 15, "Clothing", new DateTime(2024, 11, 2, 12, 35, 39, 373, DateTimeKind.Local).AddTicks(6664), "Stylish and warm winter jacket", true, "Men's Jacket", 4, null, 8 },
                    { 16, "Clothing", new DateTime(2024, 11, 2, 12, 35, 39, 373, DateTimeKind.Local).AddTicks(6704), "Soft wool scarf for cold weather", true, "Wool Scarf", 7, null, 8 },
                    { 17, "Electronics", new DateTime(2024, 11, 2, 12, 35, 39, 373, DateTimeKind.Local).AddTicks(6708), "10-inch tablet with high-resolution display", true, "Tablet", 6, null, 9 },
                    { 18, "Electronics", new DateTime(2024, 11, 2, 12, 35, 39, 373, DateTimeKind.Local).AddTicks(6712), "Protective case for 10-inch tablet", true, "Tablet Case", 10, null, 9 },
                    { 19, "Fitness", new DateTime(2024, 11, 2, 12, 35, 39, 373, DateTimeKind.Local).AddTicks(6716), "Non-slip yoga mat for workouts", true, "Yoga Mat", 12, null, 10 },
                    { 20, "Fitness", new DateTime(2024, 11, 2, 12, 35, 39, 373, DateTimeKind.Local).AddTicks(6720), "Adjustable dumbbells for home workouts", true, "Dumbbells Set", 3, null, 10 },
                    { 21, "Office Supplies", new DateTime(2024, 11, 2, 12, 35, 39, 373, DateTimeKind.Local).AddTicks(6724), "Keeps your desk neat and organized", true, "Desk Organizer", 8, null, 11 },
                    { 22, "Office Supplies", new DateTime(2024, 11, 2, 12, 35, 39, 373, DateTimeKind.Local).AddTicks(6728), "Set of three notepads", true, "Notepad Set", 15, null, 11 },
                    { 23, "Home Decor", new DateTime(2024, 11, 2, 12, 35, 39, 373, DateTimeKind.Local).AddTicks(6732), "Elegant ceramic vase for home decor", true, "Ceramic Vase", 7, null, 12 },
                    { 24, "Home Decor", new DateTime(2024, 11, 2, 12, 35, 39, 373, DateTimeKind.Local).AddTicks(6736), "Soft pillow with a decorative cover", true, "Decorative Pillow", 10, null, 12 },
                    { 25, "Electronics", new DateTime(2024, 11, 2, 12, 35, 39, 373, DateTimeKind.Local).AddTicks(6740), "High-precision gaming mouse", true, "Gaming Mouse", 6, null, 13 },
                    { 26, "Electronics", new DateTime(2024, 11, 2, 12, 35, 39, 373, DateTimeKind.Local).AddTicks(6744), "Tactile mechanical keyboard", true, "Mechanical Keyboard", 5, null, 13 },
                    { 27, "Home Decor", new DateTime(2024, 11, 2, 12, 35, 39, 373, DateTimeKind.Local).AddTicks(6748), "Modern abstract wall art", true, "Wall Art", 4, null, 14 },
                    { 28, "Home Decor", new DateTime(2024, 11, 2, 12, 35, 39, 373, DateTimeKind.Local).AddTicks(6757), "Stylish table lamp with adjustable brightness", true, "Table Lamp", 8, null, 14 },
                    { 29, "Electronics", new DateTime(2024, 11, 2, 12, 35, 39, 373, DateTimeKind.Local).AddTicks(6761), "Programmable smart thermostat", true, "Smart Thermostat", 5, null, 15 },
                    { 30, "Electronics", new DateTime(2024, 11, 2, 12, 35, 39, 373, DateTimeKind.Local).AddTicks(6765), "Compact air purifier for small rooms", true, "Air Purifier", 3, null, 15 },
                    { 31, "Accessories", new DateTime(2024, 11, 2, 12, 35, 39, 373, DateTimeKind.Local).AddTicks(6770), "Polarized sunglasses with UV protection", true, "Sunglasses", 10, null, 16 },
                    { 32, "Accessories", new DateTime(2024, 11, 2, 12, 35, 39, 373, DateTimeKind.Local).AddTicks(6774), "Classic leather wallet with multiple compartments", true, "Leather Wallet", 9, null, 16 },
                    { 33, "Clothing", new DateTime(2024, 11, 2, 12, 35, 39, 373, DateTimeKind.Local).AddTicks(6778), "Comfortable cotton hoodie", true, "Cotton Hoodie", 8, null, 17 },
                    { 34, "Clothing", new DateTime(2024, 11, 2, 12, 35, 39, 373, DateTimeKind.Local).AddTicks(6782), "Adjustable sports cap", true, "Sports Cap", 15, null, 17 },
                    { 35, "Electronics", new DateTime(2024, 11, 2, 12, 35, 39, 373, DateTimeKind.Local).AddTicks(6786), "Compact charger for mobile devices", true, "Portable Charger", 14, null, 18 },
                    { 36, "Electronics", new DateTime(2024, 11, 2, 12, 35, 39, 373, DateTimeKind.Local).AddTicks(6790), "Set of 3 USB cables", true, "USB Cable Set", 12, null, 18 },
                    { 37, "Kitchen", new DateTime(2024, 11, 2, 12, 35, 39, 373, DateTimeKind.Local).AddTicks(6794), "Non-stick cookware set", true, "Cookware Set", 6, null, 19 },
                    { 38, "Kitchen", new DateTime(2024, 11, 2, 12, 35, 39, 373, DateTimeKind.Local).AddTicks(6798), "Stainless steel knives for every need", true, "Knife Set", 7, null, 19 },
                    { 39, "Clothing", new DateTime(2024, 11, 2, 12, 35, 39, 373, DateTimeKind.Local).AddTicks(6802), "Comfortable everyday pants", true, "Pants", 6, null, 20 },
                    { 40, "Clothing", new DateTime(2024, 11, 2, 12, 35, 39, 373, DateTimeKind.Local).AddTicks(6806), "Warm sweater for winter", true, "Sweater", 15, null, 20 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_UserId",
                table: "Products",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileImages_UserId",
                table: "ProfileImages",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.DropTable(
                name: "ProfileImages");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
