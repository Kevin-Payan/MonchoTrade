﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using monchotradebackend.data;

#nullable disable

namespace monchotradeBackend.Data.Migrations
{
    [DbContext(typeof(MonchoDbContext))]
    partial class MonchoDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.10");

            modelBuilder.Entity("monchotradebackend.models.entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateOnly>("BirthDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("SecondLastName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BirthDate = new DateOnly(1990, 5, 15),
                            Country = "Mexico",
                            Email = "ana.martinezgomez@example.com",
                            LastName = "Martinez",
                            Name = "Ana Luisa",
                            PasswordHash = "$2a$11$SfjI5GHM24/rxDcV3FqlMuHqM6fy//H7SLt8RfMvPSpaFPRiX4VkO",
                            PhoneNumber = "+52 55 1234 5678",
                            SecondLastName = "Gomez"
                        },
                        new
                        {
                            Id = 2,
                            BirthDate = new DateOnly(1985, 8, 22),
                            Country = "Mexico",
                            Email = "carlos.gomezlopez@example.com",
                            LastName = "Gomez",
                            Name = "Carlos",
                            PasswordHash = "$2a$11$8LDB6DZ4SRaVMD/Z2/6dNueYhBmIQ9YfIt3c/hH9JQZUE2DiAWVdy",
                            PhoneNumber = "+52 33 9876 5432",
                            SecondLastName = "Lopez"
                        },
                        new
                        {
                            Id = 3,
                            BirthDate = new DateOnly(1992, 3, 10),
                            Country = "Mexico",
                            Email = "miguel.torresramirez@example.com",
                            LastName = "Torres",
                            Name = "Miguel",
                            PasswordHash = "$2a$11$UrAOcKu1bTH6dxDfc18nXeJKWVNWg7O0pgTxSdA0e7ENuwPzx5FDC",
                            PhoneNumber = "+52 81 5555 4444",
                            SecondLastName = "Ramirez"
                        },
                        new
                        {
                            Id = 4,
                            BirthDate = new DateOnly(1988, 11, 5),
                            Country = "Mexico",
                            Email = "laura.rodriguezfernandez@example.com",
                            LastName = "Rodriguez",
                            Name = "Laura",
                            PasswordHash = "$2a$11$5ZT4G8ta8Je3btkuAeVuFeVYv1CTHxJbU63xHhY5cWEfvVFK2c3.a",
                            PhoneNumber = "+52 55 2222 3333",
                            SecondLastName = "Fernandez"
                        },
                        new
                        {
                            Id = 5,
                            BirthDate = new DateOnly(1995, 7, 18),
                            Country = "Mexico",
                            Email = "juan.sanchezmartinez@example.com",
                            LastName = "Sanchez",
                            Name = "Juan Carlos",
                            PasswordHash = "$2a$11$J69vWRTZybJYAISsE7WFxO.cPmTlDFAuzGfK5CdtQL.a7620ipJKe",
                            PhoneNumber = "+52 33 7777 8888",
                            SecondLastName = "Martinez"
                        },
                        new
                        {
                            Id = 6,
                            BirthDate = new DateOnly(1987, 9, 30),
                            Country = "Mexico",
                            Email = "sofia.ramirezcastillo@example.com",
                            LastName = "Ramirez",
                            Name = "Sofia",
                            PasswordHash = "$2a$11$AfOWzmsh.m64y8gzpFtmwuEAt.s3mnUW/ITZ04GYxWcX.9ZJBSoFW",
                            PhoneNumber = "+52 81 4444 5555",
                            SecondLastName = "Castillo"
                        },
                        new
                        {
                            Id = 7,
                            BirthDate = new DateOnly(1993, 2, 14),
                            Country = "Mexico",
                            Email = "andres.lopezcastro@example.com",
                            LastName = "Lopez",
                            Name = "Andres",
                            PasswordHash = "$2a$11$i9k5rm.0XGhyQ9A4ENN.X.cPD5hUKF2QYmhSlSKa0D5OGfz.RBLii",
                            PhoneNumber = "+52 55 6666 7777",
                            SecondLastName = "Castro"
                        },
                        new
                        {
                            Id = 8,
                            BirthDate = new DateOnly(1991, 6, 25),
                            Country = "Mexico",
                            Email = "valentina.castroperez@example.com",
                            LastName = "Castro",
                            Name = "Valentina",
                            PasswordHash = "$2a$11$qzflATPSOFU.M/0ko91/GOEqb2cEefV7vsp1BTUzdTbr9rOvqQfKS",
                            PhoneNumber = "+52 33 1111 2222",
                            SecondLastName = "Perez"
                        },
                        new
                        {
                            Id = 9,
                            BirthDate = new DateOnly(1986, 12, 7),
                            Country = "Mexico",
                            Email = "pedro.jimenezmorales@example.com",
                            LastName = "Jimenez",
                            Name = "Pedro",
                            PasswordHash = "$2a$11$Z//63u90J7KuF925iVGgJOrbigT6pBWVBkir70f95/ZLHx4HP8uL2",
                            PhoneNumber = "+52 81 9999 0000",
                            SecondLastName = "Morales"
                        },
                        new
                        {
                            Id = 10,
                            BirthDate = new DateOnly(1994, 4, 12),
                            Country = "Mexico",
                            Email = "mariana.diazherrera@example.com",
                            LastName = "Diaz",
                            Name = "Mariana",
                            PasswordHash = "$2a$11$IpaIMWXRwuqEZsWVt7NYFe23y/0O7u/ywHxFr5mp5hR/Jx9uwZ8qe",
                            PhoneNumber = "+52 55 3333 4444",
                            SecondLastName = "Herrera"
                        },
                        new
                        {
                            Id = 11,
                            BirthDate = new DateOnly(1989, 10, 20),
                            Country = "Mexico",
                            Email = "javier.moralesjimenez@example.com",
                            LastName = "Morales",
                            Name = "Javier",
                            PasswordHash = "$2a$11$Kou1b5hBzhwrcTv8xpPevu4vxcYxmy0nLgczMO9A6/c/C7DoGjSCO",
                            PhoneNumber = "+52 33 5555 6666",
                            SecondLastName = "Jimenez"
                        },
                        new
                        {
                            Id = 12,
                            BirthDate = new DateOnly(1996, 1, 8),
                            Country = "Mexico",
                            Email = "gabriela.fernandeztorres@example.com",
                            LastName = "Fernandez",
                            Name = "Gabriela",
                            PasswordHash = "$2a$11$9FZOEbkaRPFigyWoG0Y1NelmNWwluBjoVZoiC1b3wdDGY8s9RbyJy",
                            PhoneNumber = "+52 81 2222 3333",
                            SecondLastName = "Torres"
                        },
                        new
                        {
                            Id = 13,
                            BirthDate = new DateOnly(1984, 8, 16),
                            Country = "Mexico",
                            Email = "tomas.herreramendoza@example.com",
                            LastName = "Herrera",
                            Name = "Tomas",
                            PasswordHash = "$2a$11$clPFPyV7UKnnrmqy5xUphOJ3XmleQN0beHjCd10SyfRiFkSvYaVTK",
                            PhoneNumber = "+52 55 7777 8888",
                            SecondLastName = "Mendoza"
                        },
                        new
                        {
                            Id = 14,
                            BirthDate = new DateOnly(1997, 5, 3),
                            Country = "Mexico",
                            Email = "natalia.mendozarodriguez@example.com",
                            LastName = "Mendoza",
                            Name = "Natalia",
                            PasswordHash = "$2a$11$SeIt78dborCj071Xbd.Zte4ctT5uMPl1MAbG0AnDWmPIRuVz5/Oai",
                            PhoneNumber = "+52 33 4444 5555",
                            SecondLastName = "Rodriguez"
                        },
                        new
                        {
                            Id = 15,
                            BirthDate = new DateOnly(1983, 11, 22),
                            Country = "Mexico",
                            Email = "luis.ruizsanchez@example.com",
                            LastName = "Ruiz",
                            Name = "Luis Fernando",
                            PasswordHash = "$2a$11$8txbI0wGPjT8lnFI9Uw/W.ApPra8tvPJpMtzqdkFunzIorpRq0pLK",
                            PhoneNumber = "+52 81 6666 7777",
                            SecondLastName = "Sanchez"
                        },
                        new
                        {
                            Id = 16,
                            BirthDate = new DateOnly(1990, 7, 7),
                            Country = "Mexico",
                            Email = "elena.gonzalezreyes@example.com",
                            LastName = "Gonzalez",
                            Name = "Elena",
                            PasswordHash = "$2a$11$aH.oHiQAaK5FRjslM51qRex3b7Pobf.F0Pc9LZ.4UDx/Jn4FMMjpS",
                            PhoneNumber = "+52 55 8888 9999",
                            SecondLastName = "Reyes"
                        },
                        new
                        {
                            Id = 17,
                            BirthDate = new DateOnly(1986, 3, 19),
                            Country = "Mexico",
                            Email = "ricardo.reyesgarcia@example.com",
                            LastName = "Reyes",
                            Name = "Ricardo",
                            PasswordHash = "$2a$11$c6LQPb/C6GdUWIUs7Us4terHpE36NUY6WBONQFSv6PrIXOMCgk6za",
                            PhoneNumber = "+52 33 0000 1111",
                            SecondLastName = "Garcia"
                        },
                        new
                        {
                            Id = 18,
                            BirthDate = new DateOnly(1993, 9, 14),
                            Country = "Mexico",
                            Email = "isabel.garciamoreno@example.com",
                            LastName = "Garcia",
                            Name = "Isabel",
                            PasswordHash = "$2a$11$cqyCEV0eGn5zkZ5OMjm/uu9tCw.SfuXmXjW8xHvDLuFbnlM.KHjXq",
                            PhoneNumber = "+52 81 3333 4444",
                            SecondLastName = "Moreno"
                        },
                        new
                        {
                            Id = 19,
                            BirthDate = new DateOnly(1988, 6, 30),
                            Country = "Mexico",
                            Email = "daniel.morenosilva@example.com",
                            LastName = "Moreno",
                            Name = "Daniel",
                            PasswordHash = "$2a$11$JwXbR7R/bCJyBj79wXgLyu2THNCqoAYpeIBdMc7.2TFbYDIE905xO",
                            PhoneNumber = "+52 55 5555 6666",
                            SecondLastName = "Silva"
                        },
                        new
                        {
                            Id = 20,
                            BirthDate = new DateOnly(1995, 12, 5),
                            Country = "Mexico",
                            Email = "carmen.silvanavarro@example.com",
                            LastName = "Silva",
                            Name = "Carmen",
                            PasswordHash = "$2a$11$iGMDATQMh5HadtqYQFJFbudnIAboGwjOOMqzgtjf3iY61BQbNpBPG",
                            PhoneNumber = "+52 33 2222 3333",
                            SecondLastName = "Navarro"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
