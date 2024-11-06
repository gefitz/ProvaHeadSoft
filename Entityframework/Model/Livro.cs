namespace Entityframework.Model
{

    public class Livro
    {
        public int LivroId { get; set; }
        public string Titulo { get; set; }
        public int AnoPublicacao { get; set; }
        public Autor Autor { get; set; } 
    }
}
