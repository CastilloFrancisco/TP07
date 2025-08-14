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

    [HttpPost]
    public IActionResult Login(string usuario, string contraseña)
    {
        if (BD.Registro(usuario, contraseña))
        {
            Usuario integrante = BD.Login(usuario);

            HttpContext.Session.SetString("UsuarioId", integrante.ID.ToString());

            return RedirectToAction();
        }

        ViewBag.Error = true;
        return View("Index");
    }

    public IActionResult CerrarSesión(string usuario, string contraseña)
    {
        HttpContext.Session.SetString("UsuarioId", "0");
        return View();
    }


}
