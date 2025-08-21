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
        if(int.Parse(HttpContext.Session.GetString("logeado") = null || int.Parse(HttpContext.Session.GetString("logeado") = 0){
                    return View("Login");

        }
        return View();
    }

    public IActionResult CargarTareas()
    {
        int IDu = int.Parse(HttpContext.Session.GetString("logeado"));
        ViewBag.TareasIntegrante = BD.DevolverTareas(IDu);

        return View();
    }

    public IActionResult CrearTarea()
    {

        return View("Login.cshtml");
    }

    public IActionResult CrearTareaGuardar(string Titulo, string Descripcion, DateOnly Fecha, bool Finalizada)
    {
        int IDu = int.Parse(HttpContext.Session.GetString("logeado"));

        Tarea tareaNueva = new Tarea(Titulo, Descripcion, Fecha, Finalizada, IDu);

        BD.CrearTarea(tareaNueva);
        return View();
    }

    public IActionResult FinalizarTarea(int idTarea)
    {
        BD.FinalizarTarea(idTarea);

        return View();
    }

    public IActionResult EliminarTarea(int idTarea)
    {
        BD.FinalizarTarea(idTarea);

        return View();
    }

    public IActionResult EditarTarea(int idTarea)
    {
        ViewBag.TareaAEditar = BD.TraerTarea(idTarea);
        return View();
    }
    public IActionResult EditarTareaGuardar(int idTarea)
    {
        BD.FinalizarTarea(idTarea);

        return View();
    }
}
