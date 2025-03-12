using Application.Models.Authorization;
using Domain;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Drawing.Drawing2D;

namespace Infraestructure.Persistence
{
    public class PetrobowlDbContextData
    {
        public static async Task LoadDataAsync(
            PetrobowlDbContext context,
            UserManager<Usuario> userManager,
            RoleManager<IdentityRole> roleManager,
            ILoggerFactory loggerFactory)
        {
            try
            {
                if (!roleManager.Roles.Any())
                {
                    await roleManager.CreateAsync(new IdentityRole(Role.Admin));
                    await roleManager.CreateAsync(new IdentityRole(Role.Competidor));
                    await roleManager.CreateAsync(new IdentityRole(Role.Juez));
                    await roleManager.CreateAsync(new IdentityRole(Role.Veedor));
                }

                if (!userManager.Users.Any())
                {

                    //administradores

                    var usuarioAdmin = new Usuario
                    {
                        Nombre = "Joel",
                        Apellido = "Coque",
                        Email = "joel.coque@epn.edu.ec",
                        UserName = "shadowcoque",
                        Telefono = "0992620528",
                    };
                    await userManager.CreateAsync(usuarioAdmin, "JoelPetBow2024$");
                    await userManager.AddToRoleAsync(usuarioAdmin, Role.Admin);

                    var usuarioAdmin2 = new Usuario
                    {
                        Nombre = "Ronny",
                        Apellido = "Amores",
                        Email = "ronny.amores@epn.edu.ec",
                        UserName = "ronalatiburona",
                        Telefono = "",
                    };
                    await userManager.CreateAsync(usuarioAdmin2, "RonnyPetBow2024$");
                    await userManager.AddToRoleAsync(usuarioAdmin2, Role.Admin);

                    var usuarioAdmin3 = new Usuario
                    {
                        Nombre = "Mario",
                        Apellido = "Jariin",
                        Email = "mkjarrin@uce.edu.ec",
                        UserName = "mkjarrin",
                        Telefono = "",
                    };
                    await userManager.CreateAsync(usuarioAdmin3, "MarioPetBow2024$");
                    await userManager.AddToRoleAsync(usuarioAdmin3, Role.Admin);



                    //Jueces

                    var Marielisa = new Usuario
                    {
                        Nombre = "Marielisa",
                        Apellido = "Barragan",
                        Email = "linda.barragan@eppetroecuador.ec",
                        UserName = "MarielisaBarragan",
                        Telefono = "099999999",
                    };
                    await userManager.CreateAsync(Marielisa, "MarielisaBarraganPetBow2024$");
                    await userManager.AddToRoleAsync(Marielisa, Role.Juez);
                    //Juez 1
                    var Nelson = new Usuario
                    {
                        Nombre = "Nelson",
                        Apellido = "Bravo",
                        Email = "nelsonadrian.enriquezbravo@halliburton.com",
                        UserName = "NelsonBravo",
                        Telefono = "0998358664",
                    };
                    await userManager.CreateAsync(Nelson, "NelsonBravoPetBow2024$");
                    await userManager.AddToRoleAsync(Nelson, Role.Juez);

                    //Juez 3
                    var Alvaro = new Usuario
                    {
                        Nombre = "Alvaro",
                        Apellido = "Izurieta",
                        Email = "aballesteros3@slb.com",
                        UserName = "AlvaroIzurieta",
                        Telefono = "0998358664",
                    };
                    await userManager.CreateAsync(Alvaro, "AlvaroIzurietaPetBow2024$");
                    await userManager.AddToRoleAsync(Alvaro, Role.Juez);

                    //Juez 4
                    var Edwars = new Usuario
                    {
                        Nombre = "Edwars",
                        Apellido = "Naranjo",
                        Email = "edwars.naranjo@halliburton.com",
                        UserName = "EdwarsNaranjo",
                        Telefono = "0998358664",
                    };
                    await userManager.CreateAsync(Edwars, "AlvaroNaranjoPetBow2024$");
                    await userManager.AddToRoleAsync(Edwars, Role.Juez);

                    //Juez 4
                    var Lenin = new Usuario
                    {
                        Nombre = "Lenin",
                        Apellido = "Pozo",
                        Email = "lenin.pozo@novometgroup.com",
                        UserName = "LeninPozo",
                        Telefono = "0998358664",
                    };
                    await userManager.CreateAsync(Lenin, "LeninPozoPetBow2024$");
                    await userManager.AddToRoleAsync(Lenin, Role.Juez);
                    //Juez 5
                    var Maria = new Usuario
                    {
                        Nombre = "Maria",
                        Apellido = "Angelica",
                        Email = "mariaangelica.garcia@bakerhughes.com",
                        UserName = "mangelica97",
                        Telefono = "0998358664",
                    };
                    await userManager.CreateAsync(Maria, "MariaPetBow2024$");
                    await userManager.AddToRoleAsync(Maria, Role.Juez);
                    //juez
                    var Christofer = new Usuario
                    {
                        Nombre = "Christofer",
                        Apellido = "Mayorga",
                        Email = "christopher.mayorga@halliburton.com",
                        UserName = "cmayorga97",
                        Telefono = "0998358664",
                    };
                    await userManager.CreateAsync(Christofer, "ChristoferPetBow2024@");
                    await userManager.AddToRoleAsync(Christofer, Role.Juez);
                    //juez
                    var Presidencia = new Usuario
                    {
                        Nombre = "Presidencia",
                        Apellido = "Presidencia",
                        Email = "presidencia@spe-ecuador.org",
                        UserName = "Presidencia97",
                        Telefono = "0998358664",
                    };
                    await userManager.CreateAsync(Presidencia, "PresidenciaPetBow2024@");
                    await userManager.AddToRoleAsync(Presidencia, Role.Juez);


                    /*var usuarioCompetidor = new Usuario
                    {
                        Nombre = "Alvaro",
                        Apellido = "Mina",
                        Email = "jomina97@competidor.com",
                        UserName = "jomina97",
                        Telefono = "0998358664",
                        Equipo = 1
                    };
                    await userManager.CreateAsync(usuarioCompetidor, "KevMinPetBow2024$");
                    await userManager.AddToRoleAsync(usuarioCompetidor, Role.Competidor);*/


                    //Veedores 1
                    var Cristopher = new Usuario
                    {
                        Nombre = "Cristopher",
                        Apellido = "Saca",
                        Email = "cristophersaca10@gmail.com",
                        UserName = "CristopherSaca",
                        Telefono = "0998358664",
                    };
                    await userManager.CreateAsync(Cristopher, "CristopherSacaPetBow2024$");
                    await userManager.AddToRoleAsync(Cristopher, Role.Veedor);

                    //Veedores 2
                    var Pamela = new Usuario
                    {
                        Nombre = "Pamela",
                        Apellido = "Garcia",
                        Email = "asistencia@spe-ecuador.org",
                        UserName = "PamelaGarcia",
                        Telefono = "0998358664",
                    };
                    await userManager.CreateAsync(Pamela, "PamelaGarciaPetBow2024$");
                    await userManager.AddToRoleAsync(Pamela, Role.Veedor);

                    //Veedores 3
                    var Juan = new Usuario
                    {
                        Nombre = "Juan",
                        Apellido = "Salinas",
                        Email = "juan-pablo.s@hotmail.com",
                        UserName = "JuanSalinas",
                        Telefono = "0998358664",
                    };
                    await userManager.CreateAsync(Juan, "JuanSalinasPetBow2024$");
                    await userManager.AddToRoleAsync(Juan, Role.Veedor);

                    //Veedores 4
                    var Katherine = new Usuario
                    {
                        Nombre = "Katherine",
                        Apellido = "Cruz",
                        Email = "katherine.lizeth.cruz1996@gmail.com",
                        UserName = "KatherineCruz",
                        Telefono = "0998358664",
                    };
                    await userManager.CreateAsync(Katherine, "KatherineCruzPetBow2024$");
                    await userManager.AddToRoleAsync(Katherine, Role.Veedor);

                    //Veedores 5
                    var Cristina = new Usuario
                    {
                        Nombre = "Cristina",
                        Apellido = "Falconi",
                        Email = "cristinafalconi20@hotmail.com",
                        UserName = "CristinaFalconi",
                        Telefono = "0998358664",
                    };
                    await userManager.CreateAsync(Cristina, "CristinaFalconiPetBow2024$");
                    await userManager.AddToRoleAsync(Cristina, Role.Veedor);


                    //Veedores 6
                    var Maximo = new Usuario
                    {
                        Nombre = "Maximo",
                        Apellido = "Nastacuas",
                        Email = "maximo-0396@hotmail.com",
                        UserName = "MaximoNastacuas",
                        Telefono = "0998358664",
                    };
                    await userManager.CreateAsync(Maximo, "MaximoNastacuasPetBow2024$");
                    await userManager.AddToRoleAsync(Maximo, Role.Veedor);

                    // Capitanes

                    /*var CapitanESPE = new Usuario
                    {
                        Nombre = "Ronald",
                        Apellido = "Villacis",
                        Email = "ravillacis3@espe.edu.ec",
                        UserName = "CapitanEspe",
                        Telefono = "0999999999",
                        Equipo = 1
                    };
                    await userManager.CreateAsync(CapitanESPE, "RonaldVillacisEspePetBow2024$");
                    await userManager.AddToRoleAsync(CapitanESPE, Role.Competidor);*/

                    var CapitanEPN = new Usuario
                    {
                        Nombre = "Jesus",
                        Apellido = "Leon",
                        Email = "jesus.leon@epn.edu.ec",
                        UserName = "CapitanEPN",
                        Telefono = "0999999999",
                        Equipo = 1
                    };
                    await userManager.CreateAsync(CapitanEPN, "CapJesusEPNPetBow2024$");
                    await userManager.AddToRoleAsync(CapitanEPN, Role.Competidor);

                    var CapitanUCE = new Usuario
                    {
                        Nombre = "Sebastian",
                        Apellido = "Altamirano",
                        Email = "saaltamirano@uce.edu.ec",
                        UserName = "CapitanUCE",
                        Telefono = "0999999999",
                        Equipo = 4
                    };
                    await userManager.CreateAsync(CapitanUCE, "CapSebastianUCEPetBow2024$");
                    await userManager.AddToRoleAsync(CapitanUCE, Role.Competidor);

                    /*var CapitanUPSE = new Usuario
                    {
                        Nombre = "Anthony",
                        Apellido = "Miranda",
                        Email = "anthony.mirandal@upse.edu.ec",
                        UserName = "CapitanUPSE",
                        Telefono = "0999999999",
                        Equipo = 6
                    };
                    await userManager.CreateAsync(CapitanUPSE, "CapMirandaUPSEPetBow2024$");
                    await userManager.AddToRoleAsync(CapitanUPSE, Role.Competidor);*/

                    var CapitanESPOCH = new Usuario
                    {
                        Nombre = "Yordan",
                        Apellido = "Calero",
                        Email = "yecalero2001@gmail.com",
                        UserName = "CapitanESPOCH",
                        Telefono = "0999999999",
                        Equipo = 2
                    };
                    await userManager.CreateAsync(CapitanESPOCH, "CapYordanESPOCHPetBow2024$");
                    await userManager.AddToRoleAsync(CapitanESPOCH, Role.Competidor);

                    var CapitanESPOL = new Usuario
                    {
                        Nombre = "Miguel",
                        Apellido = "Boza",
                        Email = "angmboza@espol.edu.ec",
                        UserName = "MiguelBoza",
                        Telefono = "0999999999",
                        Equipo = 3
                    };
                    await userManager.CreateAsync(CapitanESPOL, "CapBozaESPOLPetBow2024$");
                    await userManager.AddToRoleAsync(CapitanESPOL, Role.Competidor);
                }

                if (!context.Competencias.Any())
                {
                    var actual = new Competencia
                    {
                        Anio = 2025
                    };
                    await context.Competencias!.AddAsync(actual);
                    await context.SaveChangesAsync();
                }

                //Insertar Banco de Preguntas

                if (!context.BancoPreguntas.Any())
                {
                    var actual = new BancoPregunta
                    {
                        Periodo = "Marzo-2025",
                        CompetenciaId = 1,

                    };
                    await context.BancoPreguntas!.AddAsync(actual);
                    await context.SaveChangesAsync();
                }


                /* Insertar una Pregunta y su Respuesta
                if (!context.Preguntas.Any())
                {
                    var bancoPregunta = await context.BancoPreguntas
                        .FirstOrDefaultAsync(x => x.Periodo == "Marzo-2025");

                    if (bancoPregunta != null)
                    {
                        var pregunta = new Pregunta
                        {
                            Descripcion = "¿Cuál es la capital de Francia?",
                            BancoPreguntaId = bancoPregunta.id,
                            Status = PreguntaStatus.Activa,
                            CreatedBy = "Admin",
                            CreatedDate = DateTime.Now
                        };

                        await context.Preguntas.AddAsync(pregunta);
                        await context.SaveChangesAsync();

                        var preguntaInsertada = await context.Preguntas
                            .FirstOrDefaultAsync(p => p.Descripcion == "¿Cuál es la capital de Francia?");

                        if (preguntaInsertada != null)
                        {
                            var respuesta = new Respuesta
                            {
                                Descripcion = "París",
                                Correcta = true,
                                PreguntaId = preguntaInsertada.id,
                                CreatedBy = "Admin",
                                CreatedDate = DateTime.Now
                            };

                            await context.Respuestas.AddAsync(respuesta);
                            await context.SaveChangesAsync();
                        }
                    }
                }*/

                // Asegúrate de que el banco de preguntas existe
                var bancoPregunta = await context.BancoPreguntas
                    .FirstOrDefaultAsync(x => x.Periodo == "Marzo-2025");

                if (bancoPregunta != null)
                {
                    // Lista de preguntas y respuestas (Aquí debes poner tus preguntas)
                    var listaPreguntas = new List<(string Pregunta, string Respuesta)>
                    {
                        ("¿Cuál es la capital de Francia?", "París"),
                        ("¿Cuántos continentes hay en el mundo?", "7"),
                        ("¿Quién escribió 'Cien años de soledad'?", "Gabriel García Márquez"),
                        ("¿En qué año llegó el hombre a la Luna?", "1969"),
                        ("¿Cuál es el planeta más grande del sistema solar?", "Júpiter"),
                        // Agrega aquí más preguntas hasta llegar a las 200
                    };

                    var preguntasAInsertar = new List<Pregunta>();
                    var respuestasAInsertar = new List<Respuesta>();

                    foreach (var (preguntaTexto, respuestaTexto) in listaPreguntas)
                    {
                        var pregunta = new Pregunta
                        {
                            Descripcion = preguntaTexto,
                            BancoPreguntaId = bancoPregunta.id,
                            Status = PreguntaStatus.Activa,
                            CreatedBy = "Admin",
                            CreatedDate = DateTime.Now
                        };

                        preguntasAInsertar.Add(pregunta);
                    }

                    // Insertamos todas las preguntas en un solo paso
                    await context.Preguntas.AddRangeAsync(preguntasAInsertar);
                    await context.SaveChangesAsync();

                    // Ahora insertamos las respuestas vinculadas a las preguntas
                    foreach (var pregunta in preguntasAInsertar)
                    {
                        var respuesta = new Respuesta
                        {
                            Descripcion = listaPreguntas.First(p => p.Pregunta == pregunta.Descripcion).Respuesta,
                            Correcta = true, // Ajustar si hay múltiples respuestas
                            PreguntaId = pregunta.id,
                            CreatedBy = "Admin",
                            CreatedDate = DateTime.Now
                        };

                        respuestasAInsertar.Add(respuesta);
                    }

                    // Insertamos todas las respuestas en un solo paso
                    await context.Respuestas.AddRangeAsync(respuestasAInsertar);
                    await context.SaveChangesAsync();

                    Console.WriteLine($"✅ Se han insertado {preguntasAInsertar.Count} preguntas y respuestas correctamente.");
                }
                else
                {
                    Console.WriteLine("❌ No se encontró el banco de preguntas 'Marzo-2025'.");
                }





                // Insertar EQUIPOS

                if (!context.Equipos.Any())
                {
                    var EPN = new Equipo
                    {
                        Nombre = "EPN",
                        Universidad = "Universidad Politecnica Nacional",
                        CompetenciaId = 1,
                    };
                    await context.AddAsync(EPN);

                    var ESPOCH = new Equipo
                    {
                        Nombre = "ESPOCH",
                        Universidad = "Escuela Superior Politecnica de Chimborazo",
                        CompetenciaId = 1,
                    };
                    await context.AddAsync(ESPOCH);

                    var ESPOL = new Equipo
                    {
                        Nombre = "ESPOL",
                        Universidad = "Escuela Superior Politecnica del Litoral",
                        CompetenciaId = 1,
                    };
                    await context.AddAsync(ESPOL);

                    var UCE = new Equipo
                    {
                        Nombre = "UCE",
                        Universidad = "Universidad Central Del Ecuador",
                        CompetenciaId = 1,
                    };
                    await context.AddAsync(UCE);
                    await context.SaveChangesAsync();
                }
                // ========================== ✅ UNIVERSIDAD EPN ==========================
                var existeEPN = await context.Equipos.AnyAsync(e => e.Nombre == "EPN");
                if (!existeEPN)
                {
                    await context.Equipos.AddAsync(new Equipo { Nombre = "EPN", Universidad = "Escuela Politécnica Nacional", CompetenciaId = 1 });
                    await context.SaveChangesAsync();
                }

                var competidoresEPN = new List<Usuario>
{
    new Usuario { Nombre = "Santiago", Apellido = "Benitez", Email = "sbenitez@epn.edu.ec", UserName = "sbenitez", Telefono = "0999999999", Equipo = 1, IsActive = true },
    new Usuario { Nombre = "Fernanda", Apellido = "Yanez", Email = "fyanez@epn.edu.ec", UserName = "fyanez", Telefono = "0999999999", Equipo = 1, IsActive = true },
    new Usuario { Nombre = "Miguel", Apellido = "Cordero", Email = "mcordero@epn.edu.ec", UserName = "mcordero", Telefono = "0999999999", Equipo = 1, IsActive = true },
    new Usuario { Nombre = "Alejandro", Apellido = "Ponce", Email = "aponce@epn.edu.ec", UserName = "aponce", Telefono = "0999999999", Equipo = 1, IsActive = false } // 🔴 Reserva
};

                foreach (var competidor in competidoresEPN)
                {
                    var usuarioExistente = await userManager.FindByEmailAsync(competidor.Email);
                    if (usuarioExistente == null)
                    {
                        var result = await userManager.CreateAsync(competidor, $"{competidor.Nombre}2024$");
                        if (result.Succeeded)
                        {
                            Persona nuevaPersona = new Persona
                            {
                                Nombre = competidor.Nombre,
                                Apellido = competidor.Apellido,
                                Email = competidor.Email,
                                Capitan = false,
                                EquipoId = 1,
                                UsuarioId = competidor.Id,
                                Status = competidor.IsActive ? UserStatus.Activo : UserStatus.Inactivo
                            };
                            await context.Personas!.AddAsync(nuevaPersona);
                            await context.SaveChangesAsync();
                            Console.WriteLine($"✅ Competidor {competidor.Nombre} {competidor.Apellido} agregado a EPN.");
                        }
                    }
                }

                // ========================== ✅ UNIVERSIDAD ESPOCH ==========================
                var existeESPOCH = await context.Equipos.AnyAsync(e => e.Nombre == "ESPOCH");
                if (!existeESPOCH)
                {
                    await context.Equipos.AddAsync(new Equipo { Nombre = "ESPOCH", Universidad = "Escuela Superior Politécnica de Chimborazo", CompetenciaId = 1 });
                    await context.SaveChangesAsync();
                }

                var competidoresESPOCH = new List<Usuario>
{
    new Usuario { Nombre = "Daniela", Apellido = "Castro", Email = "dcastro@espoch.edu.ec", UserName = "dcastro", Telefono = "0999999999", Equipo = 2, IsActive = true },
    new Usuario { Nombre = "Javier", Apellido = "Torres", Email = "jtorres@espoch.edu.ec", UserName = "jtorres", Telefono = "0999999999", Equipo = 2, IsActive = true },
    new Usuario { Nombre = "Paula", Apellido = "Jimenez", Email = "pjimenez@espoch.edu.ec", UserName = "pjimenez", Telefono = "0999999999", Equipo = 2, IsActive = true },
    new Usuario { Nombre = "Andrea", Apellido = "Lopez", Email = "alopez@espoch.edu.ec", UserName = "alopez", Telefono = "0999999999", Equipo = 2, IsActive = false } // 🔴 Reserva
};

                foreach (var competidor in competidoresESPOCH)
                {
                    var usuarioExistente = await userManager.FindByEmailAsync(competidor.Email);
                    if (usuarioExistente == null)
                    {
                        var result = await userManager.CreateAsync(competidor, $"{competidor.Nombre}2024$");
                        if (result.Succeeded)
                        {
                            Persona nuevaPersona = new Persona
                            {
                                Nombre = competidor.Nombre,
                                Apellido = competidor.Apellido,
                                Email = competidor.Email,
                                Capitan = false,
                                EquipoId = 2,
                                UsuarioId = competidor.Id,
                                Status = competidor.IsActive ? UserStatus.Activo : UserStatus.Inactivo
                            };
                            await context.Personas!.AddAsync(nuevaPersona);
                            await context.SaveChangesAsync();
                            Console.WriteLine($"✅ Competidor {competidor.Nombre} {competidor.Apellido} agregado a ESPOCH.");
                        }
                    }
                }

                // ========================== ✅ UNIVERSIDAD ESPOL ==========================
                var existeESPOL = await context.Equipos.AnyAsync(e => e.Nombre == "ESPOL");
                if (!existeESPOL)
                {
                    await context.Equipos.AddAsync(new Equipo { Nombre = "ESPOL", Universidad = "Escuela Superior Politécnica del Litoral", CompetenciaId = 1 });
                    await context.SaveChangesAsync();
                }

                var competidoresESPOL = new List<Usuario>
                {
                    new Usuario { Nombre = "Luis", Apellido = "Torres", Email = "ltorres@espol.edu.ec", UserName = "ltorres", Telefono = "0999999999", Equipo = 3, IsActive = true },
                    new Usuario { Nombre = "Carlos", Apellido = "Navarro", Email = "cnavarro@espol.edu.ec", UserName = "cnavarro", Telefono = "0999999999", Equipo = 3, IsActive = true },
                    // 🔴 Reserva
                };

                foreach (var competidor in competidoresESPOL)
                {
                    var usuarioExistente = await userManager.FindByEmailAsync(competidor.Email);
                    if (usuarioExistente == null)
                    {
                        var result = await userManager.CreateAsync(competidor, $"{competidor.Nombre}2024$");
                        if (result.Succeeded)
                        {
                            Persona nuevaPersona = new Persona
                            {
                                Nombre = competidor.Nombre,
                                Apellido = competidor.Apellido,
                                Email = competidor.Email,
                                Capitan = false,
                                EquipoId = 3,
                                UsuarioId = competidor.Id,
                                Status = competidor.IsActive ? UserStatus.Activo : UserStatus.Inactivo
                            };
                            await context.Personas!.AddAsync(nuevaPersona);
                            await context.SaveChangesAsync();
                            Console.WriteLine($"✅ Competidor {competidor.Nombre} {competidor.Apellido} agregado a ESPOL.");
                        }
                    }
                }

                // ========================== ✅ UNIVERSIDAD UCE ==========================
                var existeUCE = await context.Equipos.AnyAsync(e => e.Nombre == "UCE");
                if (!existeUCE)
                {
                    await context.Equipos.AddAsync(new Equipo { Nombre = "UCE", Universidad = "Universidad Central del Ecuador", CompetenciaId = 1 });
                    await context.SaveChangesAsync();
                }

                var competidoresUCE = new List<Usuario>
                {
                    new Usuario { Nombre = "Marcos", Apellido = "Gonzalez", Email = "mgonzalez@uce.edu.ec", UserName = "mgonzalez", Telefono = "0999999999", Equipo = 4, IsActive = true },
                    new Usuario { Nombre = "Isabel", Apellido = "Ramirez", Email = "iramirez@uce.edu.ec", UserName = "iramirez", Telefono = "0999999999", Equipo = 4, IsActive = true },
                    new Usuario { Nombre = "Ricardo", Apellido = "Vera", Email = "rvera@uce.edu.ec", UserName = "rvera", Telefono = "0999999999", Equipo = 4, IsActive = true },
                    new Usuario { Nombre = "Julian", Apellido = "Perez", Email = "jperez@uce.edu.ec", UserName = "jperez", Telefono = "0999999999", Equipo = 4, IsActive = false } // 🔴 Reserva
                };
                foreach (var competidor in competidoresUCE)
                {
                    var usuarioExistente = await userManager.FindByEmailAsync(competidor.Email);
                    if (usuarioExistente == null)
                    {
                        var result = await userManager.CreateAsync(competidor, $"{competidor.Nombre}2024$");
                        if (result.Succeeded)
                        {
                            Persona nuevaPersona = new Persona
                            {
                                Nombre = competidor.Nombre,
                                Apellido = competidor.Apellido,
                                Email = competidor.Email,
                                Capitan = false,
                                EquipoId = 4,
                                UsuarioId = competidor.Id,
                                Status = competidor.IsActive ? UserStatus.Activo : UserStatus.Inactivo
                            };
                            await context.Personas!.AddAsync(nuevaPersona);
                            await context.SaveChangesAsync();
                            Console.WriteLine($"✅ Competidor {competidor.Nombre} {competidor.Apellido} agregado a uce.");
                        }
                    }
                }





            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<PetrobowlDbContext>();
                logger.LogError(ex.Message);
            }

        }
    }
}