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
                    case 1://Cadastro de Cliente
                        Console.Clear();
                        CadastroClienteEndereco(listaClientes);
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case 2://Cadastro de Livro
                        Console.Clear();
                        listaClientes.ForEach(i => Console.WriteLine(i));
                        Console.ReadKey();
                        break;
                    case 3:
                        //Empréstimo de Livro
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

        //Função para cadastrar cliente e adicioná-lo na lista
        public static void CadastroClienteEndereco(List<Cliente> listaClientes)
        {
            long idCliente;
            string cpf, nome, telefone, logradouro, bairro, cidade, estado, cep;
            DateTime dataNascimento;

            Console.WriteLine("\nPreencha o formulário abaixo com os dados do cliente:");
            Console.Write("\nCPF: ");
            cpf = Console.ReadLine();
            if (ValidaCpf(listaClientes, cpf) == false)
            {
                Console.Write("Nome: ");
                nome = Console.ReadLine();
                Console.Write("Data de Nascimento(mm/dd/yyyy): ");
                var entradaData = Console.ReadLine();
                DateTime.TryParse(entradaData, out dataNascimento);
                //dataNascimento = DateTime.ParseExact(nasc, "d", CultureBr);
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
                EscreveArquivo(listaClientes);
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

        
        //Função para escrever no Arquivo
        public static void EscreveArquivo(List<Cliente> listaClientes)
        {
            using (StreamWriter file = new StreamWriter(@"C:\Users\maiar\source\repos\mgbatista\Avaliacao-Biblioteca\Arquivos\CLIENTE.csv", append: true))
            {
                foreach (Cliente c in listaClientes)
                    
                    file.WriteLine(FormatoArquivoCliente(c)); //Escreve a lista no arquivo separando com quebra de linha
            }
        }
        /*
        //Função para ler o Arquivo
        public static void LerArquivo(List<Cliente> listaClientes)
        {
            // SE O ARQUIVO EXISTIR
            if (File.Exists(@"C:\Users\maiar\source\repos\mgbatista\Avaliacao-Biblioteca\Arquivos\CLIENTE.csv"))
            {
                using (var lendo = new StreamReader(@"C:\Users\maiar\source\repos\mgbatista\Avaliacao-Biblioteca\Arquivos\CLIENTE.csv"))
                {
                    // VARIAVEIS
                    long idCliente;
                    string cpf, nome, telefone, logradouro, bairro, cidade, estado, cep;
                    DateTime dataNascimento;

                    //List<string> listaArqCliente = new List<string>();


                    //CultureInfo CultureBr = new CultureInfo(name: "pt-BR"); // DATA NO FORMATO BRASILEIRO

                    // ENQUANTO ARQUIVO EXISTIR
                    while (!lendo.EndOfStream)
                    {
                        var line = lendo.ReadLine();
                        var values = line.Split(';');

                        cpf = values[0];
                       
                        listaArqCliente.Add(values[0]);
                        listaArqCliente.Add(values[1]);
                        listaArqCliente.Add(values[2]);
                        listaArqCliente.Add(values[3]);
                        listaArqCliente.Add(values[4]);
                        listaArqCliente.Add(values[5]);
                        listaArqCliente.Add(values[6]);
                        listaArqCliente.Add(values[7]);
                        listaArqCliente.Add(values[8]);
                        listaArqCliente.Add(values[9]);


                        //string line = file.ReadLine(); // ARMAZENA A LINHA EM CARACTERES

                        //ADICIONANDO CLIENTE A LISTA
                        //listaClientes.Add(new Cliente()
                        Cliente cliente = new Cliente()
                        {
                        IdCliente = idCliente,
                        CPF = cpf,
                        Nome = nome,
                        DataNascimento = Convert.ToDateTime(dataNascimento),
                        Telefone = telefone,
                        Logradouro = logradouro,
                        Bairro = bairro,
                        Cidade = cidade,
                        Estado = estado,
                        CEP = cep,
                        };
                        listaClientes.Add(cliente);
                    }
                }
            }
        }*/

        //teste2 //LE O ARQUIVO E TRANSFORMA EM LISTA
        


    }
}
