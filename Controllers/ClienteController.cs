using Fiap.Web.Alunos.Data.Contexts;
using Fiap.Web.Alunos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace Fiap.Web.Alunos.Controllers
{
    public class ClienteController : Controller
    {
        private readonly DatabaseContext _context;

        public ClienteController(DatabaseContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Carrega todos os clientes do banco de dados
            var clientes = _context.Clientes.ToList();
            return View(clientes);

        }

        [HttpGet]
        public IActionResult Create()
        {
            var representantes = _context.RepresentanteModels.ToList();
            var selectListRepresentantes = new SelectList(representantes, nameof(RepresentanteModel.RepresentanteId), nameof(RepresentanteModel.NomeRepresentante));

            ViewBag.Representantes = selectListRepresentantes;

            return View(new ClienteModel());
        }

        [HttpPost]
        public IActionResult Create(ClienteModel clienteModel)
        {
            _context.Clientes.Add(clienteModel);
            _context.SaveChanges();

            TempData["mensagemSucesso"] = $"O cliente {clienteModel.Nome} foi cadastrado com sucesso";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Edit(ClienteModel clienteModel)
        {
            _context.Update(clienteModel);
            _context.SaveChanges();
            TempData["mensagemSucesso"] = $"Os dados do cliente {clienteModel.Nome} foram alterados com sucesso";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var cliente = _context.Clientes.Find(id);
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
                _context.SaveChanges();
                TempData["mensagemSucesso"] = $"Os dados do cliente {cliente.Nome} foram removidos com sucesso";
            }
            else
            {
                TempData["mensagemSucesso"] = "OPS !!! Cliente inexistente.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
