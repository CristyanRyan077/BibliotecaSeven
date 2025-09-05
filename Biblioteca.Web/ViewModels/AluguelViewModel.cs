namespace Biblioteca.Web.ViewModels
{
    public class AluguelViewModel
    {
        public int LivroId { get; set; }
        public string TituloLivro { get; set; } = string.Empty;
        public string? ImagemLivro { get; set; }
        public string? Autor { get; set; }
        public string? Categoria { get; set; }

        public int UsuarioId { get; set; }
        public string UsuarioNome { get; set; } = string.Empty;
        public DateTime DataLocacao { get; set; } = DateTime.Now;
        public DateTime DataDevolucaoPrevista { get; set; } = DateTime.Now.AddDays(30);
    }
}

