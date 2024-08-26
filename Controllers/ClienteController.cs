using Fiap.Web.Alunos.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Web.Alunos.Controllers
{
    public class ClienteController : Controller
    {
        private List<ClienteModel> _clientes;

        public ClienteController()
        {
            _clientes = GerarClientesMocados();
        }
        public IActionResult Index()
        {

            Console.WriteLine(_clientes.Count); 
            return View();
        }

        public static List<ClienteModel> GerarClientesMocados()
        {
            var clientes = new List<ClienteModel>();

            for (int i = 1; i <= 5; i++)
            {
                var cliente = new ClienteModel
                {
                    ClienteId = i,
                    Nome = "Nome" + i,
                    Sobrenome = "Sobrenome" + i,
                    email = "cliente" + i + "a@example.com",
                    DataNascimento = DateTime.Now.AddYears(-30),
                    Observacao = "Cliente antigo, prefere contato por telefone." + i,
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
