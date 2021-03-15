using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.IO;

namespace BibliotecaAlugueis
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Cliente> listaClientes = new List<Cliente>();
            List<string> listaCpfCliente = new List<string>();
            List<Livro> listaLivros = new List<Livro>();
            List<string> listaIsbnLivro = new List<string>();

            int escolhaMenu = 0;

            Console.WriteLine("Capacidade:" + listaClientes.Capacity);

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

                switch (escolhaMenu)
                {
                    case 1://Cadastro de Cliente OK
                        Console.Clear();
                        CadastroClienteEndereco(listaClientes, listaCpfCliente);
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case 2://Cadastro de Livro
                        Console.Clear();
                        CadastroLivro(listaLivros, listaIsbnLivro);
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case 3://Empréstimo de Livro
                        Console.Clear();
                        listaLivros.ForEach(i => Console.WriteLine(i));
                        Console.ReadKey();

                        break;
                    case 4:
                        //Devolução de Livro
                        
                        break;
                    case 5:
                        //Relatório de Empréstimos e Devoluções
                        break;
                }

            } while (escolhaMenu != 6);

            Console.ReadKey();
        }

        //CLIENTE

        //Função para cadastrar cliente e adicioná-lo na lista
        public static void CadastroClienteEndereco(List<Cliente> listaClientes, List<string> listaCpfCliente)
        {
            long idCliente;
            string cpf, nome, telefone, logradouro, bairro, cidade, estado, cep;
            DateTime dataNascimento;
            Console.WriteLine("\nPreencha o formulário abaixo com os dados do cliente:");
            Console.Write("\nCPF: ");
            cpf = Console.ReadLine();
            LerArquivoCliente(listaCpfCliente);   
            if(listaCpfCliente.Contains(cpf))
            {
                Console.WriteLine("Cliente já cadastrado!\nPressione qualquer tecla para voltar ao Menu Principal");
            }
            else if (ValidaCpf(listaClientes, cpf) == false)
            {
                Console.Write("Nome: ");
                nome = Console.ReadLine();
                Console.Write("Data de Nascimento(mm/dd/yyyy): ");
                var entradaData = Console.ReadLine();
                DateTime.TryParse(entradaData, out dataNascimento);
                Console.Write("Telefone: ");
                telefone = Console.ReadLine();
                Console.WriteLine("\nEndereço");
                Console.Write("Logradouro e número: ");
                logradouro = Console.ReadLine();
                Console.Write("Bairro: ");
                bairro = Console.ReadLine();
                Console.Write("Cidade: ");
                cidade = Console.ReadLine();
                Console.Write("Estado: ");
                estado = Console.ReadLine();
                Console.Write("CEP: ");
                cep = Console.ReadLine();
                Console.WriteLine("");

                idCliente = new Random().Next(3, 1000);
                Cliente cliente = new Cliente(idCliente, cpf, nome, dataNascimento, telefone, logradouro, bairro, cidade, estado, cep);
                listaClientes.Add(cliente);
                listaClientes = listaClientes.OrderBy(x => x.Nome).ToList();
                EscreveArquivoCliente(listaClientes);
                Console.WriteLine($"Cliente cadastrado com sucesso! \nSeu Id é: {cliente.IdCliente}");
            } else Console.WriteLine("Cliente já cadastrado!\nPressione qualquer tecla para voltar ao Menu Principal");
        }

        //Função que verifica se o CPF já está cadastrado
        public static bool ValidaCpf(List<Cliente> listaClientes, string cpf)
        {
            foreach (Cliente i in listaClientes)
            {
                if (i.CPF.Equals(cpf))
                    return true;
            }
            return false;
        }

        //Função que define o formato que o cliente será salvo no arquivo
        private static string FormatoArquivoCliente(Cliente c)
        {
            return c.IdCliente + ";" + c.CPF + ";" + c.Nome + ";" + c.DataNascimento.ToString("MM/dd/yyyy") + ";" +
                c.Telefone + ";" + c.Logradouro + ";" + c.Bairro + ";" + c.Cidade + ";" + c.Estado + ";" + c.CEP;
        }
        
        //Função para escrever no Arquivo Cliente
        public static void EscreveArquivoCliente(List<Cliente> listaClientes)
        {
            using (StreamWriter file = new StreamWriter(@"C:\Users\maiar\source\repos\mgbatista\Avaliacao-Biblioteca\Arquivos\CLIENTE.csv", append: true)) //append para pular linha e salvar nova
            {
                foreach (Cliente c in listaClientes)
                    
                    file.WriteLine(FormatoArquivoCliente(c)); 
            }
        }
        
        //Função para ler o Arquivo Cliente(CPF)
        public static List<string> LerArquivoCliente(List<string> listaCpfCliente)
        {
            //Verifica se o arquivo existe
            if (File.Exists(@"C:\Users\maiar\source\repos\mgbatista\Avaliacao-Biblioteca\Arquivos\CLIENTE.csv"))
            {
                using (var lendo = new StreamReader(@"C:\Users\maiar\source\repos\mgbatista\Avaliacao-Biblioteca\Arquivos\CLIENTE.csv"))
                {
                    //Variáveis
                    string cpf;
                    //Enquanto existir
                    while (!lendo.EndOfStream)
                    {
                        var line = lendo.ReadLine().Split(';');
                        cpf = line[1];
                        listaCpfCliente.Add(cpf);
                    }
                }
            }
            return listaCpfCliente;
        }


        //LIVRO

        //Função para cadastrar Livro e adicioná-lo na lista
        public static void CadastroLivro(List<Livro> listaLivros, List<string> listaIsbnLivro)
        {
            long numeroTombo;
            string isbn, titulo, genero, autor;
            DateTime dataPublicacao;

            Console.WriteLine("\nPreencha o formulário abaixo com os dados do livro:");
            Console.Write("\nISBN: ");
            isbn = Console.ReadLine();
            LerArquivoLivro(listaIsbnLivro);
            if (listaIsbnLivro.Contains(isbn))
            {
                Console.WriteLine("Livro já cadastrado!\nPressione qualquer tecla para voltar ao Menu Principal");
            }
            else if (ValidaIsbn(listaLivros, isbn) == false)
            {
                Console.Write("Título: ");
                titulo = Console.ReadLine();
                Console.Write("Gênero: ");
                genero = Console.ReadLine();
                Console.Write("Data de Publicação(mm/dd/yyyy): ");
                var entradaDataPubli = Console.ReadLine();
                DateTime.TryParse(entradaDataPubli, out dataPublicacao);
                Console.Write("Autor: ");
                autor = Console.ReadLine();
                Console.WriteLine("");

                //Método para gerar númerotombo sequencial e automatico
                if (listaIsbnLivro.Count == 0)
                {
                    numeroTombo = 1;
                }
                else numeroTombo = listaIsbnLivro.Count + 1;

                Livro livro = new Livro(numeroTombo, isbn, titulo, genero, dataPublicacao, autor);
                listaLivros.Add(livro);
                listaLivros = listaLivros.OrderBy(x => x.NumeroTombo).ToList();
                EscreveArquivoLivro(listaLivros);
                Console.WriteLine($"Livro cadastrado com sucesso! \nSeu NúmeroTombo é: {livro.NumeroTombo}");
            }
            else Console.WriteLine("Livro já cadastrado!\nPressione qualquer tecla para voltar ao Menu Principal");
        }

        //Função que verifica se o ISBN do livro já está cadastrado
        public static bool ValidaIsbn(List<Livro> listaLivros, string isbn)
        {
            foreach (Livro i in listaLivros)
            {
                if (i.ISBN.Equals(isbn))
                    return true;
            }
            return false;
        }

        //Função que define o formato que o livro será salvo no arquivo
        private static string FormatoArquivoLivro(Livro l)
        {
            return l.NumeroTombo + ";" + l.ISBN + ";" + l.Titulo + ";" + l.Genero + ";" + l.DataPublicacao.ToString("MM/dd/yyyy") + ";" + l.Autor;
        }

        //Função para escrever no Arquivo Livro
        public static void EscreveArquivoLivro(List<Livro> listaLivros)
        {
            using (StreamWriter file = new StreamWriter(@"C:\Users\maiar\source\repos\mgbatista\Avaliacao-Biblioteca\Arquivos\LIVRO.csv", append: true)) //append para pular linha e salvar nova
            {
                foreach (Livro l in listaLivros)

                    file.WriteLine(FormatoArquivoLivro(l));
            }
        }

        //Função para ler o Arquivo Livro
        public static List<string> LerArquivoLivro(List<string> listaIsbnLivro)
        {
            //Verifica se o arquivo existe
            if (File.Exists(@"C:\Users\maiar\source\repos\mgbatista\Avaliacao-Biblioteca\Arquivos\LIVRO.csv"))
            {
                using (var lendo2 = new StreamReader(@"C:\Users\maiar\source\repos\mgbatista\Avaliacao-Biblioteca\Arquivos\LIVRO.csv"))
                {
                    //Variáveis
                    string isbn;
                    //Enquanto existir
                    while (!lendo2.EndOfStream)
                    {
                        var line = lendo2.ReadLine().Split(';');
                        isbn = line[1];
                        listaIsbnLivro.Add(isbn);
                    }
                }
            }
            return listaIsbnLivro;
        }
    }
}
