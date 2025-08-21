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
        if (BD.Registro(usuario, contraseña))
        {
            Usuario integrante = BD.Login(usuario);
            HttpContext.Session.SetString("UsuarioId", integrante.ID.ToString());

            return RedirectToAction("Index", "Home");
        }

        ViewBag.Error = true;
        return View("Login");
    }
    public IActionResult CerrarSesión(string usuario, string contraseña)
    {
        HttpContext.Session.SetString("UsuarioId", "0");
        return View();
    }


}
