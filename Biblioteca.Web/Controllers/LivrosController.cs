using Biblioteca.Data.Database;
using Biblioteca.Data.Models;
using Microsoft.AspNetCore.Mvc;

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
            // retorna a view com a lista de livros
            return View(livros.ToList());
        }

        // GET: Livros/Create
        public IActionResult Create() => View();

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
            return View(livro);
        }
    }
}
