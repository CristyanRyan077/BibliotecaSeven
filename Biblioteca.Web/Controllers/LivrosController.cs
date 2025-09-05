using Biblioteca.Data.Database;
using Biblioteca.Data.Models;
using Biblioteca.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Biblioteca.Web.Controllers
{
    public class LivrosController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly Context _context;

        public LivrosController(Context context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: Livros
        public IActionResult Index(string search)
        {
            // pega os livros do banco
            var livros = _context.Livros.AsQueryable();

            // se usuario pesquisar, filtra pelo título ou autor
            if (!string.IsNullOrEmpty(search))
            {
                livros = livros.Where(l => l.Titulo.Contains(search) || l.Autor.Contains(search));
            }
            ViewData["Search"] = search;
           
            // retorna a view com a lista de livros
            return View(livros.ToList());
        }

        // GET: Livros/Create
        public IActionResult Create()
        {
            ViewBag.Categorias = new SelectList(new List<string>
            {
                "Romance",
                "Ficção",
                "Fantasia",
                "Terror",
                "Ciência"
            });
            return View();
        }

        // POST: Livros/Create
        [HttpPost]
        public async Task<IActionResult> Create(Livro livro, IFormFile imagem)
        {
            if (ModelState.IsValid)
            {
                // se uma imagem foi enviada, salva na pasta wwwroot/Imagens/livros
                if (imagem != null && imagem.Length > 0)
                {
                    var uploads = Path.Combine(_env.WebRootPath, "Imagens/livros");
                    Directory.CreateDirectory(uploads); // garante que a pasta existe

                    var fileName = Path.GetFileName(imagem.FileName);
                    var filePath = Path.Combine(uploads, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imagem.CopyToAsync(stream);
                    }

                    livro.ImagemUrl = "/Imagens/livros/" + fileName;
                }
                // adiciona o livro ao banco
                _context.Add(livro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categorias = new SelectList(new List<string> { "Romance", "Ficção", "Fantasia", "Terror", "Ciência" });
            return View(livro);
        }

        // GET: Alugueis
        [HttpGet]
        public async Task<IActionResult> Alugueis() 
        {
            var alugueis = await _context.Alugueis
                .Include(a => a.Livro)      // Carrega o livro relacionado
                .Include(a => a.Usuario)    // Carrega o usuário relacionado
                .ToListAsync();
            return View(alugueis);
        }

        // GET: Livros/Alugar/5
        [HttpGet]
        public async Task<IActionResult> Alugar(int livroId)
        {
            var livro = await _context.Livros.FindAsync(livroId);
            if (livro == null) return NotFound();
            var usuario = _context.Usuarios.FirstOrDefault(x => x.Id == 1);
            var vm = new AluguelViewModel
            {
                LivroId = livro.Id,
                UsuarioId = usuario.Id,
                UsuarioNome = usuario.Username,
                TituloLivro = livro.Titulo,
                ImagemLivro = livro.ImagemUrl,
                DataLocacao = DateTime.Now,
                Autor = livro.Autor,
                Categoria = livro.Categoria,
                DataDevolucaoPrevista = DateTime.Now.AddDays(30)
            };

            return View(vm);
        }

        // Post: Livros/Alugar/5
        [HttpPost]
        public async Task<IActionResult> Alugar(AluguelViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var aluguel = new Aluguel
                {
                    LivroId = vm.LivroId,
                    UsuarioId = vm.UsuarioId,
                    DataLocacao = vm.DataLocacao,
                    DataDevolucaoPrevista = vm.DataDevolucaoPrevista
                };

                _context.Add(aluguel);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Livros"); // redireciona para listagem de livros
            }

            return View(vm);
        }

    }
}
