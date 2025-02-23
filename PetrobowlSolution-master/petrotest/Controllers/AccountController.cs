using Application.Persistence;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using petrotest.Models;

namespace petrotest.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<Usuario> _signInManager;
        private readonly UserManager<Usuario> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUnitOfWork _unitOfWork;


        public AccountController(SignInManager<Usuario> signInManager,
            UserManager<Usuario> userManager,
            RoleManager<IdentityRole> roleManager,
            IUnitOfWork unitOfWork)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel user)
        {
            if (ModelState.IsValid)
            {
                var log = await _userManager.FindByEmailAsync(user.Email);
                if (log != null)
                {
                    var passwordValidator = _userManager.PasswordValidators.FirstOrDefault();
                    var result2 = await passwordValidator!.ValidateAsync(_userManager, null!, user.Password);
                    var blocked = await _userManager.IsLockedOutAsync(log);
                    var passwordCorrect = await _userManager.CheckPasswordAsync(log, user.Password);
                    var roles = await _userManager.GetRolesAsync(log);
                    var claims = await _userManager.GetClaimsAsync(log);
                    var result = await _signInManager.PasswordSignInAsync(user.Email, user.Password!, user.RememberMe, false);

                    if (!log.IsActive)
                    {
                        return Json(new { status = 403, details = "Este usuario está desactivado y no puede iniciar sesión" });
                    }
                    if (passwordCorrect)
                    {
                        await _signInManager.SignInAsync(log, user.RememberMe);

                        foreach (var roleName in roles)
                        {
                            var role = await _roleManager.FindByNameAsync(roleName);
                            if (role != null)
                            {
                                var rolName = role.Name;

                                if (rolName == "Admin")
                                {
                                    return RedirectToAction("Index", "Administrador");
                                }
                                else if (rolName == "Juez")
                                {
                                    return RedirectToAction("Index", "Juez");
                                }
                                else if (rolName == "Competidor")
                                {
                                    var actual = await _unitOfWork.Repository<Usuario>().GetEntityAsync(x => x.Email.Equals(user.Email));
                                    var competidor = await _unitOfWork.Repository<Persona>().GetAsync(x => x.Email.Equals(user.Email), null);
                                    if (!competidor.Any())
                                    {
                                        var comp = new Persona();
                                        comp.Email = user.Email;
                                        comp.Nombre = actual.Nombre;
                                        comp.Apellido = actual.Apellido;
                                        comp.UsuarioId = actual.Id;
                                        comp.Capitan = true;
                                        comp.Status = UserStatus.Activo;
                                        comp.EquipoId = actual.Equipo;

                                        _unitOfWork.Repository<Persona>().AddEntity(comp);

                                        var resultPersona = await _unitOfWork.Complete();

                                        if (resultPersona <= 0)
                                        {
                                            return BadRequest();
                                        }

                                        return RedirectToAction("Index", "Competidor");
                                    }


                                    return RedirectToAction("Index", "Competidor");
                                }
                                else if (rolName == "Veedor")
                                {
                                    return RedirectToAction("Index", "Veedor");
                                }
                            }
                        }
                        return RedirectToAction("Index", "Home");
                    }

                    ModelState.AddModelError(string.Empty, "Credenciales incorrectas");
                }



            }
            return View(user);
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                };

                var result = await _userManager.CreateAsync((Usuario)user, model.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync((Usuario)user, isPersistent: false);

                    return RedirectToAction("index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");

            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

    }
}
