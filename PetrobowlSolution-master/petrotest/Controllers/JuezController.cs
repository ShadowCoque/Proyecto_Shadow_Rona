using Application.Contracts.Identity;
using Application.Persistence;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using petrotest.Models;
using System.Linq.Expressions;
using System.Security.Claims;

namespace petrotest.Controllers
{
    public class JuezController : Controller
    {
        private readonly SignInManager<Usuario> _signInManager;
        private readonly UserManager<Usuario> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;

        public JuezController(SignInManager<Usuario> signInManager,
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

        public IActionResult Equipos()
        {
            return View();
        }
        public async Task<IActionResult> Usuarios()
        {

            var user = await _unitOfWork.Repository<Usuario>().GetAsync(x => x.Equipo != null);

            return View(user);
        }

        public async Task<IActionResult> Editar()
        {
            var user = await _unitOfWork.Repository<Usuario>().GetAsync(x => x.Equipo != null);

            return View(user);
        }


        public IActionResult Ver(int Id)
        {
            

            return View();
        }
        [Authorize(Roles = "Admin, Juez, Veedor")]
        public async Task<IActionResult> Resultados()
        {
            var includes = new List<Expression<Func<Resultado, object>>>
            {
                p => p.Competencias!,
                p => p.Equipos!
            };
            var resultados = await _unitOfWork.Repository<Resultado>().GetAsync(x => x.EquipoId != null, null, includes);

            return View(resultados);
        }

        [Authorize(Roles = "Admin, Juez")]
        public async Task<IActionResult> Preguntas()
        {


            var includes = new List<Expression<Func<Pregunta, object>>>
            {
                p => p.Respuestas!
            };
            var pregunta = await _unitOfWork.Repository<Pregunta>().GetAsync(x => x.Descripcion != null, null, includes);

            return View(pregunta);
        }
        
        public async Task<IActionResult> Torneo()
        {
            var equipo = await _unitOfWork.Repository<Equipo>().GetAsync(x => string.IsNullOrEmpty(x.Grupo));
            var GrupoA = await _unitOfWork.Repository<Equipo>().GetAsync(x => x.Grupo == "A");
            var GrupoB = await _unitOfWork.Repository<Equipo>().GetAsync(x => x.Grupo == "B");
          
            ViewBag.GrupoA = GrupoA;
            ViewBag.GrupoB = GrupoB;
            return View(equipo);
        }

        [Authorize(Roles = "Admin, Juez")]
        public async Task<IActionResult> Sortear()
        {

            var equiposSinGrupo = await _unitOfWork.Repository<Equipo>().GetAsync(x => string.IsNullOrEmpty(x.Grupo));

            Random random = new Random();
            var equiposSorteados = equiposSinGrupo.OrderBy(x => random.Next()).ToList();

            for (int i = 0; i < equiposSorteados.Count; i++)
            {
                var equipo = equiposSorteados[i];
                if (i < 3)
                {
                    equipo.Grupo = "A";
                }
                else
                {
                    equipo.Grupo = "B";
                }
                await _unitOfWork.Repository<Equipo>().UpdateAsync(equipo);
            }
            return RedirectToAction("Torneo", "Juez");
        }

        public IActionResult Start()
        {

            return View();
        }
        public async Task<IActionResult> STorneo()
        {
            var GrupoA = await _unitOfWork.Repository<Equipo>().GetAsync(x => x.Grupo == "A");
            var GrupoB = await _unitOfWork.Repository<Equipo>().GetAsync(x => x.Grupo == "B");
            ViewBag.GrupoA = GrupoA;
            ViewBag.GrupoB = GrupoB;
            return View();
        }
        [Authorize(Roles = "Admin, Juez, Veedor")]
        public async Task<IActionResult> ResultadosA()
        {

            var includes = new List<Expression<Func<Resultado, object>>>
            {
                p => p.Competencias!,
                p => p.Equipos!
            };
            var resultados = await _unitOfWork.Repository<Resultado>().GetAsync(x => x.EquipoId != null && x.Equipos.Grupo == "A", null, includes);

            return View(resultados);
        }

        [Authorize(Roles = "Admin, Juez, Veedor")]

        public async Task<IActionResult> ResultadosB()
        {
            var includes = new List<Expression<Func<Resultado, object>>>
            {
                p => p.Competencias!,
                p => p.Equipos!
            };
            var resultados = await _unitOfWork.Repository<Resultado>().GetAsync(x => x.EquipoId != null && x.Equipos.Grupo == "B", null, includes);

            return View(resultados);
        }

        [HttpPost]
        public async Task<IActionResult> Sumar(int id)
        {
            var includes = new List<Expression<Func<Resultado, object>>>
            {
                p => p.Equipos!,
                p => p.Competencias!
            };
            var resultados = await _unitOfWork.Repository<Resultado>().GetEntityAsync(x => x.EquipoId == id, includes);

            if (resultados == null)
            {
                var result = new Resultado();
                result.EquipoId = id;
                result.CompetenciaId = 1;
                result.Total = 10;

                _unitOfWork.Repository<Resultado>().AddEntity(result);

                var resultPersona = await _unitOfWork.Complete();

                if (resultPersona <= 0)
                {
                    return BadRequest();
                }

                return RedirectToAction("Start","Juez");
            }else if (resultados != null)
            {
                resultados.Total = resultados.Total + 10;
                await _unitOfWork.Repository<Resultado>().UpdateAsync(resultados);
                return RedirectToAction("Start", "Juez");
            }

            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Restar(int id)
        {
            var includes = new List<Expression<Func<Resultado, object>>>
            {
                p => p.Equipos!,
                p => p.Competencias!
            };
            var resultados = await _unitOfWork.Repository<Resultado>().GetEntityAsync(x => x.EquipoId == id, includes);

            if (resultados == null)
            {
                var result = new Resultado();
                result.EquipoId = id;
                result.CompetenciaId = 1;
                result.Total = -5;

                _unitOfWork.Repository<Resultado>().AddEntity(result);

                var resultPersona = await _unitOfWork.Complete();

                if (resultPersona <= 0)
                {
                    return BadRequest();
                }

                return RedirectToAction("Start", "Juez");
            }
            else if (resultados != null)
            {
                resultados.Total = resultados.Total - 5;
                await _unitOfWork.Repository<Resultado>().UpdateAsync(resultados);
                return RedirectToAction("Start", "Juez");
            }


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EditarUsuario(EditarUsuarioViewModel editarUsuarioViewModel)
        {

            var usuario = await _unitOfWork.Repository<Usuario>().GetEntityAsync(x => x.Id == editarUsuarioViewModel.Id);

            usuario.Nombre = editarUsuarioViewModel.Nombre;
            usuario.Apellido = editarUsuarioViewModel.Apellido;
            usuario.Email = editarUsuarioViewModel.Email;
            usuario.IsActive = editarUsuarioViewModel.Estado;

            await _unitOfWork.Repository<Usuario>().UpdateAsync(usuario);

            var persona = await _unitOfWork.Repository<Persona>().GetEntityAsync(x => x.UsuarioId == editarUsuarioViewModel.Id);
            if(editarUsuarioViewModel.Estado == true)
            {
                persona.Status = UserStatus.Activo;
                await _unitOfWork.Repository<Persona>().UpdateAsync(persona);

            }
            else
            {
                persona.Status = UserStatus.Inactivo;
                await _unitOfWork.Repository<Persona>().UpdateAsync(persona);

            }

            return RedirectToAction("Usuarios","Juez");
        }
        [HttpPost]
        public async Task<IActionResult> EditarPregunta(PreguntaViewModel preguntaViewModel)
        {

            var pregunta = await _unitOfWork.Repository<Pregunta>().GetEntityAsync(x => x.id == preguntaViewModel.Idpreg);
            pregunta.Descripcion = preguntaViewModel.Pregunta;
            await _unitOfWork.Repository<Pregunta>().UpdateAsync(pregunta);

            var respuesta = await _unitOfWork.Repository<Respuesta>().GetEntityAsync(x => x.id == preguntaViewModel.Idresp);
            respuesta.Descripcion = preguntaViewModel.Respuesta;
           
            await _unitOfWork.Repository<Respuesta>().UpdateAsync(respuesta);

            return RedirectToAction("Preguntas","Juez");
        }


        [Authorize(Roles = "Admin, Juez, Veedor")]
        public async Task<IActionResult> ESPE()
        {
          

                var includes = new List<Expression<Func<Persona, object>>>
            {
                p => p.Equipo!,
                p => p.Usuario!
            };
                var user = await _unitOfWork.Repository<Persona>().GetAsync(x => x.EquipoId == 1 && x.Capitan == false && x.Status == UserStatus.Inactivo==false, null, includes);
                var cap = await _unitOfWork.Repository<Persona>().GetAsync(x => x.EquipoId == 1 && x.Capitan == true, null, includes);
                ViewBag.cap = cap;
                var reserva = await _unitOfWork.Repository<Persona>().GetEntityAsync(x => x.Status ==
                                                                                            UserStatus.Inactivo && x.EquipoId ==1);
                ViewBag.reserva = reserva;
                ViewBag.Count= user.Count();
                ViewBag.capCount = cap.Count();
                return View(user);

        }
        [Authorize(Roles = "Admin, Juez, Veedor")]
        public async Task<IActionResult> ESPOCH()
        {

            var includes = new List<Expression<Func<Persona, object>>>
            {
                p => p.Equipo!,
                p => p.Usuario!
            };
            var user = await _unitOfWork.Repository<Persona>().GetAsync(x => x.EquipoId == 3 && x.Capitan == false && x.Status == UserStatus.Inactivo==false, null, includes);
            var cap = await _unitOfWork.Repository<Persona>().GetAsync(x => x.EquipoId == 3 && x.Capitan == true, null, includes);
            ViewBag.cap = cap;
            var reserva = await _unitOfWork.Repository<Persona>().GetEntityAsync(x => x.Status ==
                                                                                        UserStatus.Inactivo && x.EquipoId == 3);
            ViewBag.reserva = reserva;
            ViewBag.Count = user.Count();
            ViewBag.capCount = cap.Count();
            return View(user);


        }

        [Authorize(Roles = "Admin, Juez, Veedor")]
        public async Task<IActionResult> UCE()
        {

            var includes = new List<Expression<Func<Persona, object>>>
            {
                p => p.Equipo!,
                p => p.Usuario!
            };
            var user = await _unitOfWork.Repository<Persona>().GetAsync(x => x.EquipoId == 5 && x.Capitan == false && x.Status == UserStatus.Inactivo == false, null, includes);
            var cap = await _unitOfWork.Repository<Persona>().GetAsync(x => x.EquipoId == 5 && x.Capitan == true, null, includes);
            ViewBag.cap = cap;
            var reserva = await _unitOfWork.Repository<Persona>().GetEntityAsync(x => x.Status ==
                                                                                        UserStatus.Inactivo && x.EquipoId ==5);
            ViewBag.reserva = reserva;
            ViewBag.Count = user.Count();
            ViewBag.capCount = cap.Count();
            return View(user);


        }

        [Authorize(Roles = "Admin, Juez, Veedor")]
        public async Task<IActionResult> ESPOL()
        {

            var includes = new List<Expression<Func<Persona, object>>>
            {
                p => p.Equipo!,
                p => p.Usuario!
            };
            var user = await _unitOfWork.Repository<Persona>().GetAsync(x => x.EquipoId == 4 && x.Capitan == false && x.Status == UserStatus.Inactivo == false, null, includes);
            var cap = await _unitOfWork.Repository<Persona>().GetAsync(x => x.EquipoId == 4 && x.Capitan == true, null, includes);
            ViewBag.cap = cap;
            var reserva = await _unitOfWork.Repository<Persona>().GetEntityAsync(x => x.Status ==
                                                                                        UserStatus.Inactivo && x.EquipoId == 4);
            ViewBag.reserva = reserva;
            ViewBag.Count = user.Count();
            ViewBag.capCount = cap.Count();
            return View(user);


        }

        [Authorize(Roles = "Admin, Juez, Veedor")]
        public async Task<IActionResult> UPSE()
        {
            var includes = new List<Expression<Func<Persona, object>>>
            {
                p => p.Equipo!,
                p => p.Usuario!
            };
            var user = await _unitOfWork.Repository<Persona>().GetAsync(x => x.EquipoId == 6 && x.Capitan == false && x.Status == UserStatus.Inactivo == false, null, includes);
            var cap = await _unitOfWork.Repository<Persona>().GetAsync(x => x.EquipoId == 6 && x.Capitan == true, null, includes);
            ViewBag.cap = cap;
            var reserva = await _unitOfWork.Repository<Persona>().GetEntityAsync(x => x.Status ==
                                                                                        UserStatus.Inactivo && x.EquipoId ==6);
            ViewBag.reserva = reserva;
            ViewBag.Count = user.Count();
            ViewBag.capCount = cap.Count();
            return View(user);
        }

        [Authorize(Roles = "Admin, Juez, Veedor")]
        public async Task<IActionResult> EPN()
        {

            var includes = new List<Expression<Func<Persona, object>>>
            {
                p => p.Equipo!,
                p => p.Usuario!
            };
            var user = await _unitOfWork.Repository<Persona>().GetAsync(x => x.EquipoId == 2 && x.Capitan == false && x.Status == UserStatus.Inactivo == false, null, includes);
            var cap = await _unitOfWork.Repository<Persona>().GetAsync(x => x.EquipoId == 2 && x.Capitan == true, null, includes);
            ViewBag.cap = cap;
            var reserva = await _unitOfWork.Repository<Persona>().GetEntityAsync(x => x.Status ==
                                                                                        UserStatus.Inactivo && x.EquipoId == 2);
            ViewBag.reserva = reserva;
            ViewBag.Count = user.Count();
            ViewBag.capCount = cap.Count();
            return View(user);


        }
    }
}
