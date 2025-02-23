using Application.Models.Authorization;
using Domain;
using Microsoft.AspNetCore.Identity;
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
                        Telefono = "0992620529",
                    };
                    await userManager.CreateAsync(usuarioAdmin, "RonnyPetBow2024$");
                    await userManager.AddToRoleAsync(usuarioAdmin, Role.Admin);


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
                    var Precidencia = new Usuario
                    {
                        Nombre = "Precidencia",
                        Apellido = "Precidencia",
                        Email = "presidencia@spe-ecuador.org",
                        UserName = "Presidencia97",
                        Telefono = "0998358664",
                    };
                    await userManager.CreateAsync(Precidencia, "PresidenciaPetBow2024@");
                    await userManager.AddToRoleAsync(Precidencia, Role.Juez);


                    var usuarioCompetidor = new Usuario
                    {
                        Nombre = "Alvaro",
                        Apellido = "Mina",
                        Email = "jomina97@competidor.com",
                        UserName = "jomina97",
                        Telefono = "0998358664",
                        Equipo = 1
                    };
                    await userManager.CreateAsync(usuarioCompetidor, "KevMinPetBow2024$");
                    await userManager.AddToRoleAsync(usuarioCompetidor, Role.Competidor);


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

                    var CapitanESPE = new Usuario
                    {
                        Nombre = "Ronald",
                        Apellido = "Villacis",
                        Email = "ravillacis3@espe.edu.ec",
                        UserName = "CapitanEspe",
                        Telefono = "0999999999",
                        Equipo = 1
                    };
                    await userManager.CreateAsync(CapitanESPE, "RonaldVillacisEspePetBow2024$");
                    await userManager.AddToRoleAsync(CapitanESPE, Role.Competidor);

                    var CapitanEPN = new Usuario
                    {
                        Nombre = "Jesus",
                        Apellido = "Leon",
                        Email = "jesus.leon@epn.edu.ec",
                        UserName = "CapitanEPN",
                        Telefono = "0999999999",
                        Equipo = 2
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
                        Equipo = 5
                    };
                    await userManager.CreateAsync(CapitanUCE, "CapSebastianUCEPetBow2024$");
                    await userManager.AddToRoleAsync(CapitanUCE, Role.Competidor);

                    var CapitanUPSE = new Usuario
                    {
                        Nombre = "Anthony",
                        Apellido = "Miranda",
                        Email = "anthony.mirandal@upse.edu.ec",
                        UserName = "CapitanUPSE",
                        Telefono = "0999999999",
                        Equipo = 6
                    };
                    await userManager.CreateAsync(CapitanUPSE, "CapMirandaUPSEPetBow2024$");
                    await userManager.AddToRoleAsync(CapitanUPSE, Role.Competidor);

                    var CapitanESPOCH = new Usuario
                    {
                        Nombre = "Yordan",
                        Apellido = "Calero",
                        Email = "yecalero2001@gmail.com",
                        UserName = "CapitanESPOCH",
                        Telefono = "0999999999",
                        Equipo = 3
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
                        Equipo = 4
                    };
                    await userManager.CreateAsync(CapitanESPOL, "CapBozaESPOLPetBow2024$");
                    await userManager.AddToRoleAsync(CapitanESPOL, Role.Competidor);
                }

                if (!context.Competencias.Any())
                {
                    var actual = new Competencia
                    {
                        Anio = 2024
                    };
                    await context.Competencias!.AddAsync(actual);
                    await context.SaveChangesAsync();
                }

                //Insertar Banco de Preguntas

                if (!context.BancoPreguntas.Any())
                {
                    var actual = new BancoPregunta
                    {
                        Periodo = "Marzo-2024",
                        CompetenciaId = 1,

                    };
                    await context.BancoPreguntas!.AddAsync(actual);
                    await context.SaveChangesAsync();
                }


                // Insertar EQUIPOS

                if (!context.Equipos.Any())
                {
                    var ESPE = new Equipo
                    {
                        Nombre = "ESPE",
                        Universidad = "Universidad de las Fuerzas Armadas ESPE",
                        CompetenciaId = 1,
                    };
                    await context.AddAsync(ESPE);

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

                    var UPSE = new Equipo
                    {
                        Nombre = "UPSE",
                        Universidad = "Universidad Estatal Peninsula de Santa Elena",
                        CompetenciaId = 1,
                    };
                    await context.AddAsync(UPSE);
                    await context.SaveChangesAsync();
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
