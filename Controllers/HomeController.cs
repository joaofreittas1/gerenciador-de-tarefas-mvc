using AplicacaoWebTarefas.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Globalization;

namespace AplicacaoWebTarefas.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var lista = Tarefa.GetTarefa();
            ViewBag.Lista = lista;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SalvarTarefa(string nome_tarefa, string vlr_custo, DateTime dt_limite)
        {
            if (!string.IsNullOrEmpty(nome_tarefa) && !string.IsNullOrEmpty(vlr_custo) && dt_limite != DateTime.MinValue)
            {
                var tarefa = new Tarefa();
                tarefa.NomeTarefa = nome_tarefa;

                decimal custo;
                if (Decimal.TryParse(vlr_custo, NumberStyles.Currency, new CultureInfo("pt-BR"), out custo))
                {
                    tarefa.Custo = custo;
                    tarefa.DateLimite = dt_limite;

                    try
                    {
                        tarefa.Salvar();
                        TempData["Mensagem"] = "Tarefa cadastrada com sucesso!";
                    }
                    catch (Exception ex)
                    {

                        if(ex.Message.Contains("Violation of UNIQUE KEY constraint 'UQ__tb_taref__C0E1DBEAC2D4D1A1'. Cannot insert duplicate key in object 'dbo.tb_tarefa'."))
                        TempData["Erro"] = "Não é possivel inserir uma tarefa com mesmo nome";
                    }
                }
            }
            else
            {
                TempData["Erro"] = "Erro ao cadastrar a tarefa, Por Favor preencha os campos corretamente";
            }

            return RedirectToAction("Privacy");

        }

        [HttpPost]
        public ActionResult AlterarTarefa(int codigoTarefa,string nomeTarefa, string valorcusto, DateTime dtLimite)
        {
            if (!string.IsNullOrEmpty(nomeTarefa) && !string.IsNullOrEmpty(valorcusto) && dtLimite != DateTime.MinValue)
            {
                var tarefa = new Tarefa();

                tarefa.CodigoTarefa = codigoTarefa;
                tarefa.NomeTarefa = nomeTarefa;

                decimal custo;
                if (Decimal.TryParse(valorcusto, NumberStyles.Currency, new CultureInfo("pt-BR"), out custo))
                {
                    tarefa.Custo = custo;
                    tarefa.DateLimite = dtLimite;

                    try
                    {
                        tarefa.Alterar();
                        TempData["Mensagem"] = "Tarefa alterada com sucesso!";
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.Contains("Violation of UNIQUE KEY constraint 'UQ__tb_taref__C0E1DBEAC2D4D1A1'."))
                            TempData["Erro"] = "Já existe outra tarefa com esse mesmo nome.Por favor colocar um nome diferente";
                    }
                }
            }
            else
            {
                TempData["Erro"] = "Erro ao alterar a tarefa, Por Favor preencha os campos corretamente";
            }

            return RedirectToAction("");
        }

        [HttpPost] // Altere para HttpDelete se estiver usando DELETE
        public JsonResult ExcluirTarefa(int codigoTarefa)
        {
            var tarefa = new Tarefa();
            var mensagem = "";

            tarefa.CodigoTarefa = codigoTarefa;

            try
            {
                tarefa.Excluir();
                mensagem = "Tarefa excluída com sucesso!";
            }
            catch (Exception ex)
            {
                mensagem = "Erro ao excluir a tarefa.";
            }

            return Json(new { mensagem });
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}