using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UsuariosWeb.Domain.Entities;
using UsuariosWeb.Domain.Interfaces.Services;
using UsuariosWeb.Presentation.Models;
using UsuariosWeb.Presentation.Reports;

namespace UsuariosWeb.Presentation.Controllers
{
    [Authorize]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioDomainService _usuarioDomainService;

        public UsuarioController(IUsuarioDomainService usuarioDomainService)
        {
            _usuarioDomainService = usuarioDomainService;
        }

        public IActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastro(UsuariosCadastroModel model)
        {
            if (ModelState.IsValid)
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

                    ModelState.Clear();
                    TempData["MensagemSucesso"] = usuario.Nome;

                }
                catch(Exception e)
                {
                    TempData["MensagemErro"] = e.Message;
                }
            }
            return View();
        }

        public IActionResult Consulta()
        {
            var lista = new List<UsuariosConsultaModel>();

            try
            {
                foreach(var item in _usuarioDomainService.ConsultarUsuarios())
                {
                    lista.Add(new UsuariosConsultaModel()
                    {
                        IdUsuario = item.IdUsuario,
                        Nome = item.Nome,
                        Email = item.Email,
                        DataCadastro = item.DataCadastro
                    });
                }
            }
            catch(Exception e)
            {
                TempData["MensagemErro"] = e.Message;
            }

            return View(lista);
        }

        public IActionResult Relatorio()
        {
            try
            {
                //consultar os usuários
                var usuarios = _usuarioDomainService.ConsultarUsuarios();
                //gerando o relatorio PDF
                var pdf = UsuarioReport.GerarPDF(usuarios);
                //download do relatório..
                return File(pdf, "application/pdf", "relatorio.pdf");
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = e.Message;
                return RedirectToAction("Consulta");
            }
        }
        public IActionResult MinhaConta()
        {
            var model = new UsuariosMinhaContaModel();            

            try
            {
                var email = User.Identity.Name;
                var usuario = _usuarioDomainService.ObterUsuario(email);

                model.Nome = usuario.Nome;
                model.Email = usuario.Email;
                model.Perfil = usuario.Perfil.Nome.ToUpper();
                model.DataCadastro = usuario.DataCadastro;

            }
            catch(Exception e)
            {
                TempData["MensagemErro"] = e.Message;
            }

            return View(model);
        }

        public IActionResult Exclusao(Guid id)
        {
            try
            {
                var usuario = new Usuario()
                {
                    IdUsuario = id
                };

                _usuarioDomainService.ExcluirUsuario(usuario);
                TempData["MensagemSucesso"] = "Excluído";
            }
            catch(Exception e)
            {
                TempData["MensagemErro"] = e.Message;
            }
            return RedirectToAction("Consulta");

        }
        public IActionResult Edicao(Guid id)
        {
            var model = new UsuariosEdicaoModel();
            model.ListagemPerfis = new List<SelectListItem>();

            try
            {
                var usuario = _usuarioDomainService.ObterUsuario(id);
                if(usuario != null)
                {
                    model.IdUsuario = usuario.IdUsuario;
                    model.Nome = usuario.Nome;
                    model.Email = usuario.Email;
                    model.IdPerfil = usuario.IdPerfil.ToString();
                    model.ListagemPerfis = ObterListagemPerfis();
                    
                }
            }
            catch(Exception e)
            {
                TempData["MensagemErro"] = e.Message;
            }
            return View();
        }

        [HttpPost]
        public IActionResult Edicao(UsuariosEdicaoModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var usuario = new Usuario()
                    {
                        IdUsuario = model.IdUsuario,
                        Nome = model.Nome,
                        Email = model.Email,
                        IdPerfil = Guid.Parse(model.IdPerfil)
                    };
                    _usuarioDomainService.AtualizarUsuario(usuario);

                    TempData["MensagemSucesso"] = "Atualizado";

                }
                catch(Exception e)
                {
                    TempData["MensagemErro"] = e.Message;
                }
            }
            model.ListagemPerfis = ObterListagemPerfis();
            return View(model);
        }
        private List<SelectListItem> ObterListagemPerfis()
        {
            var lista = new List<SelectListItem>();
            foreach (var item in _usuarioDomainService.ConsultarPerfis())
            {
                lista.Add(new SelectListItem { Value = item.IdPerfil.ToString(), Text = item.Nome.ToUpper() });
            }
            return lista;
        }        
    }
}
