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

            ViewBag.MostarPapelera = (BD.DevolverTareasEliminadas(IDu).Count > 0);

            return View();
        }

        public IActionResult CrearTarea()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UsuarioId")) || int.Parse(HttpContext.Session.GetString("UsuarioId")) == 0)
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }
        public IActionResult CrearTareaGuardar(string Titulo, string Descripcion, DateTime FechaFin, bool Finalizada)
        {
            int IDu = int.Parse(HttpContext.Session.GetString("UsuarioId"));

            Tarea tareaNueva = new Tarea(Titulo, Descripcion, FechaFin, Finalizada, IDu);

            BD.CrearTarea(tareaNueva);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult FinalizarTarea(int idTarea)
        {
            BD.FinalizarTarea(idTarea);
            return RedirectToAction("Index", "Home");

        }

        public IActionResult EliminarTarea(int idTarea)
        {
            BD.EliminarTarea(idTarea);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult EditarTarea(int idTarea)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UsuarioId")) || int.Parse(HttpContext.Session.GetString("UsuarioId")) == 0)
            {
                return RedirectToAction("Login", "Account");
            }
            ViewBag.TareaAEditar = BD.TraerTarea(idTarea);
            if (ViewBag.TareaAEditar == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();


        }
        [HttpPost]

        public IActionResult EditarTareaGuardar(int IDt, string nombre, string desc, DateTime fechaF)
        {
            int IDu = int.Parse(HttpContext.Session.GetString("UsuarioId"));

            Tarea t = new Tarea(IDt, nombre, desc, fechaF, false, IDu);

            BD.ActualizarTarea(t);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Papelera()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UsuarioId")) || int.Parse(HttpContext.Session.GetString("UsuarioId")) == 0)
            {
                return RedirectToAction("Login", "Account");
            }

            List<Tarea> borradas = BD.DevolverTareasEliminadas(int.Parse(HttpContext.Session.GetString("UsuarioId")));

            ViewBag.TareasBorradas = borradas;

            
            return View();


        }
        [HttpPost]

        public IActionResult PapeleraGuardar(List<int> sacarDeLaPapelera)
        {
            int IDu = int.Parse(HttpContext.Session.GetString("UsuarioId"));

            foreach (int i in sacarDeLaPapelera)
            {
                BD.RestaurarTarea(i);
            }


            return RedirectToAction("Index", "Home");
        }
        
          public IActionResult CompartirTareas(int idTarea)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UsuarioId")) || int.Parse(HttpContext.Session.GetString("UsuarioId")) == 0)
            {
                return RedirectToAction("Login", "Account");
            }

            int idU = int.Parse(HttpContext.Session.GetString("UsuarioId"));

            List<Usuario> gente = BD.DevolverOtrosUsuarios(idU);

            ViewBag.OtrosUsuarios = gente;

            ViewBag.TareaCompartir = BD.TraerTarea(idTarea);

            return View();
        }
        [HttpPost]

        public IActionResult CompartirTareasGuardar(int IDt, List<int> compartirA)
        {

            foreach (int i in compartirA)
            {
                Tarea T = BD.TraerTarea(IDt);
                T.IdUsuario = i;

                BD.CrearTarea(T);
                
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
