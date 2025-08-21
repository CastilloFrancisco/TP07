using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TP07.Models;

namespace TP7.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {

            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UsuarioId")) || int.Parse(HttpContext.Session.GetString("UsuarioId")) == 0)
            {
                return RedirectToAction("Login", "Account");
            }

            int IDu = int.Parse(HttpContext.Session.GetString("UsuarioId"));
            ViewBag.Usuario = BD.TraerUsuarioPorId(IDu);

            ViewBag.Tareas = BD.DevolverTareas(IDu);

            return View();
        }

        public IActionResult CargarTareas()
        {
            int IDu = int.Parse(HttpContext.Session.GetString("UsuarioId"));
            ViewBag.TareasIntegrante = BD.DevolverTareas(IDu);

            return View();
        }

        public IActionResult CrearTarea()
        {
            return View("Login");
        }

        public IActionResult CrearTareaGuardar(string Titulo, string Descripcion, DateTime Fecha, bool Finalizada)
        {
            int IDu = int.Parse(HttpContext.Session.GetString("UsuarioId"));

            Tarea tareaNueva = new Tarea(Titulo, Descripcion, Fecha, Finalizada, IDu);

            BD.CrearTarea(tareaNueva);
            return View();
        }

        public IActionResult FinalizarTarea(int idTarea)
        {
            BD.FinalizarTarea(idTarea);

            return View("Index", "Home");
        }

        public IActionResult EliminarTarea(int idTarea)
        {
            BD.FinalizarTarea(idTarea);

            return View("Index", "Home");
        }

        public IActionResult EditarTarea(int idTarea)
        {
            ViewBag.TareaAEditar = BD.TraerTarea(idTarea);
            if (ViewBag.TareaAEditar == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();


        }

        public IActionResult EditarTareaGuardar(int idTarea)
        {
            //      BD.ActualizarTarea(idTarea);

            return View("Index", "Home");
        }
    }
}
