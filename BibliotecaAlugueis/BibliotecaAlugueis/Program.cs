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
                        listaClientes.ForEach(i => Console.WriteLine(i));
                        Console.ReadKey();
                        break;
                    case 3://Empréstimo de Livro


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
        public static void CadastroClienteEndereco(List<Cliente> listaClientes, List<string> listaCpfCliente)
        {
            long idCliente;
            string cpf, nome, telefone, logradouro, bairro, cidade, estado, cep;
            DateTime dataNascimento;
            Console.WriteLine("\nPreencha o formulário abaixo com os dados do cliente:");
            Console.Write("\nCPF: ");
            cpf = Console.ReadLine();
            LerArquivo(listaCpfCliente);   
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
            using (StreamWriter file = new StreamWriter(@"C:\Users\maiar\source\repos\mgbatista\Avaliacao-Biblioteca\Arquivos\CLIENTE.csv", append: true)) //append para pular linha e salvar nova
            {
                foreach (Cliente c in listaClientes)
                    
                    file.WriteLine(FormatoArquivoCliente(c)); 
            }
        }
        
        //Função para ler o Arquivo
        public static List<string> LerArquivo(List<string> listaCpfCliente)
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
    }
}
