using Application.Contracts.Identity;
using Application.Models.Authorization;
using Application.Persistence;
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using petrotest.Models;
using System.Linq.Expressions;
using System.Security.Claims;

namespace petrotest.Controllers
{
    public class CompetidorController : Controller
    {
        private readonly SignInManager<Usuario> _signInManager;
        private readonly UserManager<Usuario> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;
        public CompetidorController(SignInManager<Usuario> signInManager,
            UserManager<Usuario> userManager,
            RoleManager<IdentityRole> roleManager,
            IUnitOfWork unitOfWork,
            IAuthService authService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
            _authService = authService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Equipo()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var includes = new List<Expression<Func<Persona, object>>>
            {
                p => p.Equipo!,
                p => p.Usuario!
            };
            var capitan = await _unitOfWork.Repository<Persona>().GetAsync(x => x.UsuarioId == userId && x.Capitan == 
                                                                            true, null,includes);
            if(capitan.Count != 0) {
                var datosCapitan = await _unitOfWork.Repository<Persona>().GetEntityAsync(x => x.UsuarioId ==
                                                                                     capitan.FirstOrDefault()!.UsuarioId);
                var equipoCapitan = capitan.FirstOrDefault()!.EquipoId;

                var integrantesEquipo = await _unitOfWork.Repository<Persona>().GetAsync(x => x.EquipoId ==
                                                                                        equipoCapitan && x.Capitan ==
                                                                                        false && x.Status == UserStatus.Activo, null, includes);

                if (integrantesEquipo == null)
                {
                    ViewBag.capitan = datosCapitan;
                    return View(capitan);
                }
                var integranteReserva = await _unitOfWork.Repository<Persona>().GetEntityAsync(x => x.Status ==
                                                                                            UserStatus.Inactivo && x.EquipoId ==
                                                                                            capitan.FirstOrDefault()!.EquipoId);

                if (integranteReserva != null)
                {
                    ViewBag.capitan = datosCapitan;
                    ViewBag.equipo = integrantesEquipo;
                    ViewBag.reserva = integranteReserva;
                    ViewBag.numEquipo = integrantesEquipo.Count;
                    return View(capitan);
                }

                ViewBag.capitan = datosCapitan;
                ViewBag.equipo = integrantesEquipo;
                ViewBag.numEquipo = integrantesEquipo.Count;
                return View(capitan);
            }
            else
            {

                var integrante = await _unitOfWork.Repository<Persona>().GetEntityAsync(x => x.UsuarioId == userId && x.Capitan ==
                                                                            false, includes);
                var equipo = await _unitOfWork.Repository<Equipo>().GetEntityAsync(x => x.id == integrante.EquipoId);

                ViewBag.capcount = capitan.Count;
                ViewBag.integrante = integrante;
                ViewBag.equipo = equipo;
                return View();
            }
            
        }

        [HttpGet]
        public async Task<IActionResult> Registrar()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var includes = new List<Expression<Func<Persona, object>>>
            {
                p => p.Equipo!,
                p => p.Usuario!
            };
            var capitan = await _unitOfWork.Repository<Persona>().GetAsync(x => x.UsuarioId == userId && x.Capitan == true, null, includes);

            return View(capitan);
        }
        [HttpPost]
        public async Task<IActionResult> Registrar(RegistrarViewModel registrarViewModel)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "No se pudo completar el registro";
                return RedirectToAction("Index","Competidor");
            }
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var includes = new List<Expression<Func<Persona, object>>>
            {
                p => p.Equipo!,
                p => p.Usuario!
            };
            var capitan = await _unitOfWork.Repository<Persona>().GetAsync(x => x.UsuarioId == userId && x.Capitan == true, null, includes);

            var usuarioNuevo = await _unitOfWork.Repository<Persona>().GetEntityAsync(x => x.Email == registrarViewModel.Email);
            if (usuarioNuevo == null)
            {
                var reserva = await _unitOfWork.Repository<Persona>().GetEntityAsync(x => x.Status == UserStatus.Inactivo && x.Email == registrarViewModel.Email);
                if (reserva != null)
                {
                    TempData["Error"] = "Ya se encontró competidor de reserva";
                    return RedirectToAction("Index", "Competidor");
                }

                Usuario user = new Usuario();
                user.Email = registrarViewModel.Email;
                user.Nombre = registrarViewModel.Nombre;
                user.Apellido = registrarViewModel.Apellido;
                user.Equipo = capitan.FirstOrDefault()!.EquipoId;
                user.UserName = registrarViewModel.Nombre+registrarViewModel.Apellido;
                if (registrarViewModel.Reserva == true)
                {
                    user.IsActive = false;
                }
                else
                {
                    user.IsActive = true;
                }

                var result = await _userManager.CreateAsync(user, registrarViewModel.Password);
                if (!result.Succeeded)
                {
                    TempData["Error"] = "No se registró el usuario";
                    return RedirectToAction("Index", "Competidor");
                }

                await _userManager.AddToRoleAsync(user, Role.Competidor);

                var registrado = await _unitOfWork.Repository<Usuario>().GetEntityAsync(x => x.Email == registrarViewModel.Email);


                Persona nuevo = new Persona();
                nuevo.Nombre = registrarViewModel.Nombre;
                nuevo.Apellido = registrarViewModel.Apellido;
                nuevo.Capitan = false;
                nuevo.Email = registrarViewModel.Email;
                nuevo.EquipoId = capitan.FirstOrDefault()!.EquipoId;
                nuevo.UsuarioId = registrado.Id;
                if (registrarViewModel.Reserva == true)
                {
                    nuevo.Status = UserStatus.Inactivo;
                }
                else
                {
                    nuevo.Status = UserStatus.Activo;
                }

                _unitOfWork.Repository<Persona>().AddEntity(nuevo);

                var resultPersona = await _unitOfWork.Complete();
                if (resultPersona <= 0)
                {
                    return BadRequest();
                }

                return RedirectToAction("Equipo", "Competidor");
            }
                return View();
        }

        [HttpGet]
        public async Task<IActionResult> Editar()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var includes = new List<Expression<Func<Persona, object>>>
            {
                p => p.Equipo!,
                p => p.Usuario!
            };
            var capitan = await _unitOfWork.Repository<Persona>().GetAsync(x => x.UsuarioId == userId && x.Capitan == true, null, includes);

            return View(capitan);
        }



        public IActionResult Competir()
        {
            return View();
        }

    }
}
