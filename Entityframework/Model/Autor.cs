namespace Entityframework.Model
{


    public class Autor
    {
        public int AutorId { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public ICollection<Livro> Livros { get; set; } = new List<Livro>();
    }

}
