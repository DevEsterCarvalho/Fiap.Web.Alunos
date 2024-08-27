using Fiap.Web.Alunos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering; 
namespace Fiap.Web.Alunos.Controllers
{
    public class ClienteController : Controller
    {

        public IList<ClienteModel> clientes { get; set; }

        public IList<RepresentanteModel> representantes { get; set; }
        public ClienteController()
        {
            //Simula a busca de clientes no banco de dados
            clientes = GerarClientesMocados();
            representantes = GerarRepresentantesMocados();
        }
        public IActionResult Index()
        {

            if (clientes == null)
            {
                clientes = new List<ClienteModel>();
            }
            return View(clientes);
        }

        [HttpGet]
        public IActionResult Create()
        {
            Console.WriteLine("Executou a Action Cadastrar()");

            var selectListRepresentantes =
                new SelectList(representantes,
                                nameof(RepresentanteModel.RepresentanteId),
                                nameof(RepresentanteModel.NomeRepresentante));

            ViewBag.Representantes = selectListRepresentantes;

            return View(new ClienteModel());
        }
        [HttpPost]
        public IActionResult Create(ClienteModel clienteModel)
        {
            Console.WriteLine("Gravando o cliente");
            TempData["mensagemSucesso"] = $"O cliente {clienteModel.Nome} foi cadastrado com suceso";
            return RedirectToAction(nameof(Index));
        }

        public static List<RepresentanteModel> GerarRepresentantesMocados()
        {
            var representantes = new List<RepresentanteModel>
            {
                new RepresentanteModel { RepresentanteId = 1, NomeRepresentante = "Representante 1", CPF = "111.111.111-11" },
                new RepresentanteModel { RepresentanteId = 2, NomeRepresentante = "Representante 2", CPF = "222.222.222-22" },
                new RepresentanteModel { RepresentanteId = 3, NomeRepresentante = "Representante 3", CPF = "333.333.333-33" },
                new RepresentanteModel { RepresentanteId = 4, NomeRepresentante = "Representante 4", CPF = "444.444.444-44" }
            };
            return representantes;
        }

        public static List<ClienteModel> GerarClientesMocados()
        {
            var clientes = new List<ClienteModel>();
            for (int i = 1; i <= 5; i++)
            {
                var cliente = new ClienteModel
                {
                    ClienteId = i,
                    Nome = "Cliente" + i,
                    Sobrenome = "Sobrenome" + i,
                    Email = "cliente" + i + "@example.com",
                    DataNascimento = DateTime.Now.AddYears(-30),
                    Observacao = "Observação do cliente " + i,
                    RepresentanteId = i,
                    Representante = new RepresentanteModel
                    {
                        RepresentanteId = i,
                        NomeRepresentante = "Representante" + i,
                        CPF = "00000000191"
                    }
                };
                clientes.Add(cliente);
            }
            return clientes;
        }
    }
}