using Application.Persistence;
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace petrotest.Controllers
{
    public class AdministradorController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<Usuario> _userManager;

        public AdministradorController(IUnitOfWork unitOfWork,
                                       UserManager<Usuario> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GestionCompetidores()
        {
            var includes = new List<Expression<Func<Persona, object>>>
            {
                p => p.Equipo!
            };

            var rubros = await _unitOfWork.Repository<Persona>().GetAsync(x => x.id != 0, null, includes);
            return View();
        }
    }
}
