using Application.Models.Authorization;
using Application.Persistence;
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Security.Claims;

namespace petrotest.Controllers
{
    public class TorneoController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUnitOfWork _unitOfWork;
        public TorneoController(
            RoleManager<IdentityRole> roleManager,
            IUnitOfWork unitOfWork)
        {
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
        }
        public static Dictionary<int, string> Rooms =
            new Dictionary<int, string>()
            {
                {1,"Sala 1" },
                {2,"Sala 2" },
                {3,"Sala 3" },
                {4,"Sala 4" },
                {5,"Sala 5" },
                {6,"Sala 6" },
                {7,"Sala 7" },
                {8,"Sala 8" },
                {9,"Sala 9" },
                {10,"Semifinal1" },
                {11,"Semifinal2" },
                {12,"Final" },
            };
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult Salas()
        {
            if (User.IsInRole(Role.Juez))
            {
                return View();
            }else if(User.IsInRole(Role.Competidor))
            {
                return View();
            }
            return View();
        }
        public async Task<IActionResult> Room(int room) 
        {
            
            ViewBag.Room = room;

            if (User.IsInRole(Role.Competidor))
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

                var includes = new List<Expression<Func<Persona, object>>>
                    {
                        p => p.Equipo!,
                        p => p.Usuario!
                    };
                var competidor = await _unitOfWork.Repository<Persona>().GetEntityAsync(x => x.UsuarioId == userId,includes);
                ViewBag.Competidor = competidor;
                ViewBag.IsCompetitor = 1;
            }
            else if(User.IsInRole(Role.Juez) || User.IsInRole(Role.Admin))
            {
                var include = new List<Expression<Func<Pregunta, object>>>
                    {
                        p => p.Respuestas!
                    };
                var preguntas = await _unitOfWork.Repository<Pregunta>().GetAsync(x => x.Descripcion != null, null, include);
                ViewBag.Preguntas = preguntas;
                ViewBag.IsCompetitor = 2;
            }

            return View("Room");

        }


    }
}
