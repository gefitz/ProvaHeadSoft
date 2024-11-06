using Entityframework.Context;
using Entityframework.Model;
using Entityframework.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configuração do DbContext com banco em memória
builder.Services.AddDbContext<BibliotecaContext>(options =>
    options.UseInMemoryDatabase("BibliotecaDB"));

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions
    (options => { options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles; 
        options.JsonSerializerOptions.WriteIndented = true; });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<LivroService>();
builder.Services.AddScoped<AutorService>();

var app = builder.Build();

// Chamada para popular o banco com dados iniciais
SeedDatabase(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

void SeedDatabase(WebApplication app)
{
    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<BibliotecaContext>();

        // Limpa o banco antes de adicionar os dados (opcional)
        context.Livros.RemoveRange(context.Livros);
        context.Autores.RemoveRange(context.Autores);
        context.SaveChanges();

        // Criação de autores
        var autor1 = new Autor { Nome = "Gabriel Garcia Marquez", DataNascimento = new DateTime(1927, 3, 6) };
        var autor2 = new Autor { Nome = "Isabel Allende", DataNascimento = new DateTime(1942, 8, 2) };
        var autor3 = new Autor { Nome = "Jorge Luis Borges", DataNascimento = new DateTime(1899, 8, 24) };
        var autor4 = new Autor { Nome = "Mario Vargas Llosa", DataNascimento = new DateTime(1936, 3, 28) };
        var autor5 = new Autor { Nome = "Pablo Neruda", DataNascimento = new DateTime(1904, 7, 12) };
        var autor6 = new Autor { Nome = "Clarice Lispector", DataNascimento = new DateTime(1920, 12, 10) };
        var autor7 = new Autor { Nome = "Machado de Assis", DataNascimento = new DateTime(1839, 6, 21) };

        // Criação de livros com autores associados
        var livro1 = new Livro { Titulo = "Cem Anos de Solidão", AnoPublicacao = 1967, Autor = autor1  };
        var livro2 = new Livro { Titulo = "Amor nos Tempos do Cólera", AnoPublicacao = 1985, Autor =  autor1  };
        var livro3 = new Livro { Titulo = "A Casa dos Espíritos", AnoPublicacao = 1982, Autor =  autor2  };
        var livro4 = new Livro { Titulo = "O Aleph", AnoPublicacao = 1949, Autor =  autor3  };
        var livro5 = new Livro { Titulo = "A Cidade e os Cachorros", AnoPublicacao = 1963, Autor =  autor4  };
        var livro6 = new Livro { Titulo = "Confesso que Vivi", AnoPublicacao = 1974, Autor =  autor5  };
        var livro7 = new Livro { Titulo = "A Paixão Segundo G.H.", AnoPublicacao = 1964, Autor =  autor6  };
        var livro8 = new Livro { Titulo = "Dom Casmurro", AnoPublicacao = 1899, Autor =  autor7  };
        var livro9 = new Livro { Titulo = "Memórias Póstumas de Brás Cubas", AnoPublicacao = 1881, Autor =  autor7  };

        // Adiciona os autores e livros ao contexto
        context.Autores.AddRange(autor1, autor2, autor3, autor4, autor5, autor6, autor7);
        context.Livros.AddRange(livro1, livro2, livro3, livro4, livro5, livro6, livro7, livro8, livro9);

        // Salva as mudanças no banco em memória
        context.SaveChanges();
    }
}