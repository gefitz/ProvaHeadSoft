using Entityframework.Context;
using Entityframework.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Entityframework.Service
{
    public class AutorService
    {
        private readonly BibliotecaContext _context;

        public AutorService(BibliotecaContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Autor>> GetAll()
        {
            var autor = await _context.Autores.Include(l => l.Livros).ToListAsync();

            return autor;
        }
        public async Task<Autor> GetById(int id)
        {

            var autor = await _context.Autores.Include(l => l.Livros).Where(l => l.AutorId == id).FirstOrDefaultAsync();

            return autor;
        }
        public async Task AddAutor(Autor autor)
        {

            try
            {
                List<Livro> livros = new List<Livro>();
                foreach (var item in autor.Livros)
                {
                    var livro = await _context.Livros.FindAsync(item.LivroId);
                    if (livro == null) { continue; }
                    livros.Add(livro);
                }
                if (livros.Count != 0)
                {
                    autor.Livros = livros;
                    _context.Autores.Add(autor);
                    await _context.SaveChangesAsync();

                }
                else
                {
                    _context.Autores.Add(autor);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                
            }
        }
        public async Task<Autor> UpdateLivro(Autor autor)
        {
            try
            {
                _context.Entry(autor).State = EntityState.Modified;
                await _context.SaveChangesAsync();


            }
            catch (Exception ex)
            {
                return null;
            }
            return autor;
        }
        public async Task<bool> RemoveLivro(int id)
        {
            try
            {

                var autor = await _context.Autores.Where(l => l.AutorId == id).FirstOrDefaultAsync();

                if (autor == null) return false;

                _context.Autores.Remove(autor);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }


    }
}
