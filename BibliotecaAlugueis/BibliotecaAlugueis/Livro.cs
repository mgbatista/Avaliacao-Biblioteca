using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaAlugueis
{
    public class Livro
    {
       /* public Livro(long numeroTombo, string isbn, string titulo, string genero, DateTime dataPublicacao, string autor)
        {
            NumeroTombo = numeroTombo;
            ISBN = isbn;
            Titulo = titulo;
            Genero = genero;
            DataPublicacao = dataPublicacao;
            Autor = autor;
        }*/
        public long NumeroTombo { get; set; }
        public string ISBN { get; set; }
        public string Titulo { get; set; }
        public string Genero { get; set; }
        public DateTime DataPublicacao { get; set; }
        public string Autor { get; set; }


        public override string ToString()
        {
            return ("Número Tombo: " + NumeroTombo + "\nISBN: " + ISBN + "\nTítulo: " + Titulo + "\nGênero: " + Genero + 
                "\nData de Publicação: " + DataPublicacao + "\nAutor: " + Autor);
        }
    }
}
