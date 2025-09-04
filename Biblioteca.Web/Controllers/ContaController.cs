using Biblioteca.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace Biblioteca.Web.Controllers
{
    public class ContaController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _config;
        public ContaController(IHttpClientFactory httpClientFactory, IConfiguration config)
        {
            _httpClientFactory = httpClientFactory;
            _config = config;
        }
        // criação das telas de Login e Registro usando as viewmodels criadas
        [HttpGet]
        public IActionResult Registrar() => View(new RegistrarViewModel());

        [HttpGet]
        public IActionResult Login() => View(new LoginViewModel());

        //http post para enviar os dados para a api
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            // se não for válido, recria a tela de login
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            // declara o client http e a url da api
            var client = _httpClientFactory.CreateClient();
            var urlApi = _config["ApiUrl"] + "/api/auth/login";

            // serializa os dados do modelo para json
            var json = JsonSerializer.Serialize(new { model.Username, model.Password });

            // envia os dados para a api
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(urlApi, content);
            // se o login for invalido, retorna a tela de login com a mensagem de erro
            if (!response.IsSuccessStatusCode)
            {
                model.ErrorMessage = "Usuário ou senha inválidos";
                return View(model);
            }
            // se o login for valido, lê o token retornado pela api
            var respostaJson = await response.Content.ReadAsStringAsync();
            var doc = JsonDocument.Parse(respostaJson);
            var token = doc.RootElement.GetProperty("token").GetString();

            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> Registrar(RegistrarViewModel model)
        {
            // se não for válido, recria a tela de registro
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                // declara o client http e a url da api
                var client = _httpClientFactory.CreateClient();
                var urlApi = _config["ApiUrl"] + "/api/auth/register";
                Debug.WriteLine(urlApi);

                // serializa os dados do modelo para json
                var json = JsonSerializer.Serialize(new
                {
                    model.Username,
                    model.Email,
                    model.Password,
                    model.Telefone
                });
                Debug.WriteLine(json);

                // envia os dados para a api
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(urlApi, content);
                // se o registro falhar, retorna a tela de registro com a mensagem de erro
                if (!response.IsSuccessStatusCode)
                {
                    var respostaJson = await response.Content.ReadAsStringAsync();
                    model.ErrorMessage = $"Erro ao registrar usuário: {response.StatusCode} - {respostaJson}";

                    return View(model);
                }
                return RedirectToAction("Login", "Conta");
            }
            catch (Exception ex)
            {
                model.ErrorMessage = "Erro inesperado: " + ex.Message;
                return View(model);
            }
        }
    }
}
