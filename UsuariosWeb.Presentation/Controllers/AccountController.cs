using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UsuariosWeb.Domain.Entities;
using UsuariosWeb.Domain.Interfaces.Services;
using UsuariosWeb.Presentation.Models;

namespace UsuariosWeb.Presentation.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUsuarioDomainService _usuarioDomainService;

        public AccountController(IUsuarioDomainService usuarioDomainService)
        {
            _usuarioDomainService = usuarioDomainService;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(AccountLoginModel model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    var usuario = _usuarioDomainService.AutenticarUsuario(model.Email, model.Senha);
                    #region Permissões do usuário

                    var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, usuario.Email) }, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    #endregion

                    return RedirectToAction("Index", "Home");

                }
                catch (Exception e)
                {
                    TempData["MensagemErro"] = e.Message;
                }
            }
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(AccountRegisterModel model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    var usuario = new Usuario()
                    {
                        IdUsuario = Guid.NewGuid(),
                        Nome = model.Nome,
                        Email = model.Email,
                        Senha = model.Senha,
                        DataCadastro = DateTime.Now
                    };

                    _usuarioDomainService.CadastrarUsuario(usuario);

                    TempData["MensagemSucesso"] = "Sucesso";
                    ModelState.Clear();

                }
                catch(Exception e)
                {
                    TempData["MensagemErro"] = e.Message;
                }
            }
            return View();
        }

        public IActionResult Logout()
        {
            #region Remover permissão
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            #endregion

            return RedirectToAction("Login", "Account");
        }
    }
}
