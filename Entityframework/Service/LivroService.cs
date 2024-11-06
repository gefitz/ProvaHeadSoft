using Entityframework.Context;
using Entityframework.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Entityframework.Service
{
    public class LivroService
    {
        private readonly BibliotecaContext _context;

        public LivroService(BibliotecaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Livro>> GetAll()
        {
            var livros = await _context.Livros.Include(a => a.Autor).ToListAsync();
            return livros;
        }
        public async Task<Livro> GetById(int id)
        {

            var livro = await _context.Livros.Include(a => a.Autor).Where(l => l.LivroId == id).FirstOrDefaultAsync();

            return livro;
        }
        public async Task<Livro> AddLivro(Livro livro)
        {
            try
            {
                var autor = await _context.Autores.FindAsync(livro.Autor.AutorId);
                if (autor == null)
                {
                    _context.Livros.Add(livro);
                    await _context.SaveChangesAsync();

                }
                else
                {
                    livro.Autor = autor;
                    _context.Livros.Add(livro);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return livro;
        }
        [HttpPut]
        public async Task<Livro> UpdateLivro(Livro livro)
        {

            try
            {
                _context.Entry(livro).State = EntityState.Modified;
                await _context.SaveChangesAsync();


            }
            catch (Exception ex)
            {
                return null;
            }
            return livro;
        }
        public async Task<bool> RemoveLivro(int id)
        {
            try
            {
                var livro = await _context.Livros.Where(l => l.LivroId == id).FirstOrDefaultAsync();

                if (livro == null) return false;

                _context.Livros.Remove(livro);
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
