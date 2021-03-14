using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaAlugueis
{
    class Program
    {
        static void Main(string[] args)
        {
            Cliente c = new Cliente();
            Endereco e = new Endereco();
            Livro l = new Livro();

            int escolhaMenu = 0;

            do
            {
                Console.WriteLine("\n*** BIBLIOTECA MUNICIPAL ***");
                Console.WriteLine("\nMENU");
                Console.WriteLine("\n1- Cadastro de Cliente");
                Console.WriteLine("2- Cadastro de Livro");
                Console.WriteLine("3- Empréstimo de Livro");
                Console.WriteLine("4- Devolução de Livro");
                Console.WriteLine("5- Relatório de Empréstimos e Devoluções");

                Console.Write("\nInforme a opção desejada: ");
                escolhaMenu = int.Parse(Console.ReadLine());

                Console.WriteLine("");

            } while (escolhaMenu <=0 && escolhaMenu >= 6);
                  

        }
    }
}
