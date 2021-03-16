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
            List<EmprestimoLivro> listaEmprestimo = new List<EmprestimoLivro>();
            List<long> listaNumeroTomboLivro = new List<long>();
            List<long> listaIdCliente = new List<long>();
            List<EmprestimoLivro> listaEmprestimoStatus = new List<EmprestimoLivro>();

            EmprestimoLivro emprestimo = new EmprestimoLivro();

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
                    case 2://Cadastro de Livro OK
                        Console.Clear();
                        LerListaLivro(listaLivros);
                        CadastroLivro(listaLivros, listaIsbnLivro);
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case 3://Empréstimo de Livro OK
                        Console.Clear();
                        listaLivros = LerListaLivro(listaLivros);
                        foreach (var item in listaLivros)
                        {
                            Console.WriteLine(item);
                        }
                        listaEmprestimo = LerArqEmprestimoStatusNt(listaEmprestimo);
                        foreach (var elemento in listaEmprestimo)
                        {
                            Console.WriteLine(elemento);
                        }    
                        EmprestimoDeLivro(listaEmprestimo, listaNumeroTomboLivro, listaLivros, listaClientes, listaIdCliente, listaCpfCliente, listaEmprestimoStatus, emprestimo);
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case 4://Devolução de Livro

                        break;
                    case 5://Relatório de Empréstimos e Devoluções
                        
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
            if (listaCpfCliente.Contains(cpf))
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

        // Função pra ler arquivo cliente e pegar id
        public static long LeArquivoSalvaId()
        {
            long dadoId = 0;
            //Verifica se o arquivo existe
            if (File.Exists(@"C:\Users\maiar\source\repos\mgbatista\Avaliacao-Biblioteca\Arquivos\CLIENTE.csv"))
            {
                using (var lendo4 = new StreamReader(@"C:\Users\maiar\source\repos\mgbatista\Avaliacao-Biblioteca\Arquivos\CLIENTE.csv"))
                {
                    //Enquanto existir
                    while (!lendo4.EndOfStream)
                    {
                        var line = lendo4.ReadLine().Split(';');
                        dadoId = long.Parse(line[0]);
                    }
                }
            }
            return dadoId;
        }

        //Função para ler o Arquivo Cliente(IdCliente)
        public static List<long> LerArquivoClienteIdCliente(List<long> listaIdCliente, List<Cliente> listaClientes)
        {
            //Verifica se o arquivo existe
            if (File.Exists(@"C:\Users\maiar\source\repos\mgbatista\Avaliacao-Biblioteca\Arquivos\CLIENTE.csv"))
            {
                using (var lendo4 = new StreamReader(@"C:\Users\maiar\source\repos\mgbatista\Avaliacao-Biblioteca\Arquivos\CLIENTE.csv"))
                {
                    //Variáveis
                    long idCliente;

                    //Enquanto existir
                    while (!lendo4.EndOfStream)
                    {
                        var line = lendo4.ReadLine().Split(';');
                        idCliente = long.Parse(line[0]);
                        listaIdCliente.Add(idCliente);
                    }
                }
            }
            return listaIdCliente;
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

                Livro livro = new Livro()
                {
                    NumeroTombo = numeroTombo,
                    ISBN = isbn,
                    Titulo = titulo,
                    Genero = genero,
                    DataPublicacao = DateTime.Parse(entradaDataPubli),
                    Autor = autor
                };
                
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

        //Função para ler o Arquivo Livro(ISBN)
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

        //Função para ler o Arquivo Livro
        public static List<Livro> LerListaLivro(List<Livro> listaLivros)
        {

            if (File.Exists(@"C:\Users\maiar\source\repos\mgbatista\Avaliacao-Biblioteca\Arquivos\LIVRO.csv"))
            {
                using (var lendo7 = new StreamReader(@"C:\Users\maiar\source\repos\mgbatista\Avaliacao-Biblioteca\Arquivos\LIVRO.csv"))
                {
                    //Enquanto existir
                    while (!lendo7.EndOfStream)
                    {
                        var line = lendo7.ReadLine().Split(';');
                        Livro objlivro = new Livro()
                        {
                            NumeroTombo = long.Parse(line[0]),
                            ISBN = (line[1]),
                            Titulo = (line[2]),
                            Genero = (line[3]),
                            DataPublicacao = DateTime.Parse(line[4]),
                            Autor = (line[5])
                        };

                       
                        listaLivros.Add(objlivro);
                    }
                }
            }
            return listaLivros;
        }

        // Função pra ler arquivo livro e pegar numerotombo
        public static long LeArquivoSalvaNumeroTombo()
        {
            long dadontombo = 0;
            //Verifica se o arquivo existe
            if (File.Exists(@"C:\Users\maiar\source\repos\mgbatista\Avaliacao-Biblioteca\Arquivos\LIVRO.csv"))
            {
                using (var lendo1 = new StreamReader(@"C:\Users\maiar\source\repos\mgbatista\Avaliacao-Biblioteca\Arquivos\LIVRO.csv"))
                {
                    //Enquanto existir
                    while (!lendo1.EndOfStream)
                    {
                        var line = lendo1.ReadLine().Split(';');
                        dadontombo = long.Parse(line[0]);
                    }
                }
            }
            return dadontombo;
        }


        //EMPRESTIMO LIVRO

        //Função para empréstimo de livro e adicioná-lo na lista
        public static void EmprestimoDeLivro(List<EmprestimoLivro> listaEmprestimo, List<long> listaNumeroTomboLivro, List<Livro> listaLivros, List<Cliente> listaClientes, List<long> listaIdCliente, List<string> listaCpfCliente, List<EmprestimoLivro> listaEmprestimoStatus, EmprestimoLivro objemprestimo)
        {
            long numeroTombo;
            DateTime dataEmprestimo, dataDevolucao;
            int statusEmprestimo = 0;
            string cpf;
            int tomboEncontrado = 0;
            int statusEncontrado = 0;

            Console.WriteLine("\n>>> Formulário para Empréstimo de Livro <<<");
            Console.WriteLine("\n Preencha as informações abaixo:");
            Console.Write("\nNumeroTombo: ");
            numeroTombo = long.Parse(Console.ReadLine());

            foreach (var elemento in listaEmprestimo)
            {
                if (elemento.NumeroTombo == numeroTombo)
                {
                    Console.WriteLine("Livro Emprestado, aguarde devolução!");
                    statusEncontrado = 1;
                }
            }
            if (statusEncontrado != 1)
            {
                for (int i = 0; i < listaLivros.Count; i++)
                {
                    if (listaLivros[i].NumeroTombo == numeroTombo)
                    {
                        tomboEncontrado++;
                    }
                }

                Console.Write("CPF: ");
                cpf = Console.ReadLine();
                LerArquivoCliente(listaCpfCliente);
                if (listaCpfCliente.Contains(cpf))
                {
                    dataEmprestimo = DateTime.Now;
                    Console.Write("Data de Devolução(mm/dd/yyyy): ");
                    var entradaDataDev = Console.ReadLine();
                    DateTime.TryParse(entradaDataDev, out dataDevolucao);
                    Console.WriteLine("");
                    statusEmprestimo = 1;


                    EmprestimoLivro emprestimo = new EmprestimoLivro()
                    {
                        IdCliente = LeArquivoSalvaId(),
                        NumeroTombo = LeArquivoSalvaNumeroTombo(),
                        DataEmprestimo = dataEmprestimo,
                        DataDevolucao = dataDevolucao,
                        StatusEmprestimo = statusEmprestimo
                    };

                    listaEmprestimo.Add(emprestimo);

                    listaEmprestimo = listaEmprestimo.OrderBy(x => x.DataEmprestimo).ToList();
                    EscreveArquivoEmprestimo(listaEmprestimo);
                    Console.WriteLine($"Empréstimo realizado com sucesso!\nPressione qualquer tecla para voltar ao Menu Principal");
                }
                else Console.WriteLine("Cliente não cadastrado!");
            }

        }    

        //Função que define o formato que o empréstimo será salvo no arquivo
        private static string FormatoArquivoEmprestimo(EmprestimoLivro e)
        {
            return e.IdCliente + ";" + e.NumeroTombo + ";" + e.DataEmprestimo.ToString("MM/dd/yyyy") + ";" + e.DataDevolucao.ToString("MM/dd/yyyy") + ";" + e.StatusEmprestimo;
        }

        //Função para escrever no Arquivo Emprestimo
        public static void EscreveArquivoEmprestimo(List<EmprestimoLivro> listaEmprestimo)
        {
            using (StreamWriter file = new StreamWriter(@"C:\Users\maiar\source\repos\mgbatista\Avaliacao-Biblioteca\Arquivos\EMPRESTIMO.csv", append: true)) //append para pular linha e salvar nova
            {
                foreach (EmprestimoLivro e in listaEmprestimo)

                    file.WriteLine(FormatoArquivoEmprestimo(e));
            }
        }

        //Função para ler o Arquivo Emprestimo
        public static List<EmprestimoLivro> LerArqEmprestimoStatusNt(List<EmprestimoLivro> emprestimoLivros)
        {

            if (File.Exists(@"C:\Users\maiar\source\repos\mgbatista\Avaliacao-Biblioteca\Arquivos\EMPRESTIMO.csv"))
            {
                using (var lendo5 = new StreamReader(@"C:\Users\maiar\source\repos\mgbatista\Avaliacao-Biblioteca\Arquivos\EMPRESTIMO.csv"))
                {
                    //Enquanto existir
                    while (!lendo5.EndOfStream)
                    {
                        var line = lendo5.ReadLine().Split(';');
                        EmprestimoLivro objemprestimo = new EmprestimoLivro()
                        {
                            IdCliente = long.Parse(line[0]),
                            NumeroTombo = long.Parse(line[1]),
                            DataEmprestimo = DateTime.Parse(line[2]),
                            DataDevolucao = DateTime.Parse(line[3]),
                            StatusEmprestimo = int.Parse(line[4])
                        };

                        emprestimoLivros.Add(objemprestimo);
                    }
                }
            }
            return emprestimoLivros;
        }
         
    }
}
