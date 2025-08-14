using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TP07.Models;

namespace TP7.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult CargarTareas(int IDu)
    {
        ViewBag.TareasIntegrante = BD.DevolverTareas(IDu);

        return View();
    }

    public IActionResult CrearTarea()
    {

        return View("Login.cshtml");
    }

    public IActionResult CrearTareaGuardar(string Titulo, string Descripcion, DateOnly Fecha, bool Finalizada)
    {
        Tarea tareaNueva = new Tarea(Titulo, Descripcion, Fecha, Finalizada);
        HttpContext.Session.SetString("usuario",tareaNueva.IdUsuario.ToString(usuario));
        
        BD.CrearTarea(tareaNueva);
        return View();
    }

    public IActionResult FinalizarTarea()
    {
        return View();
    }

    public IActionResult EliminarTarea()
    {
        return View();
    }

    public IActionResult EditarTarea()
    {
        return View();
    }
    public IActionResult EditarTareaGuardar()
    {
        return View();
    }
}
