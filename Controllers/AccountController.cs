using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TP07.Models;

namespace TP07.Controllers;

public class AccountController : Controller
{
    private readonly ILogger<AccountController> _logger;

    public AccountController(ILogger<AccountController> logger)
    {
        _logger = logger;
    }
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Logear(string usuario, string contraseña)
    {
        if (BD.LogIn(usuario, contraseña))
        {
            Usuario integrante = BD.TraerUsuario(usuario);
            BD.ActualizarFechaLogin(integrante.ID);

            HttpContext.Session.SetString("UsuarioId", integrante.ID.ToString());

            return RedirectToAction("Index", "Home");
        }

        ViewBag.Error = true;
        return View("Login");
    }
    public IActionResult CerrarSesión(string usuario, string contraseña)
    {
        BD.ActualizarFechaLogin(int.Parse(HttpContext.Session.GetString("UsuarioId")));
        HttpContext.Session.SetString("UsuarioId", "0");

        return RedirectToAction("Login", "Account");
    }

    public IActionResult CrearUsuario(string usuario, string contraseña)
    {
        return View();
    }

    [HttpPost]
    public IActionResult RegistrarUsuario(string usuario, string contraseña, string nombre, string apellido, string foto)
    {
        if (BD.TraerUsuario(usuario) == null)
        {
            Usuario u = new Usuario(usuario, contraseña, nombre, apellido, foto);

            BD.RegistrarUsuario(u);

            return RedirectToAction("Login", "Account");

        }

        return RedirectToAction("CrearUsuario", "Account");


    }

}
