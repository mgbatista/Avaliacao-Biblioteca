using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;





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
        public static void CadastroClienteEndereco(List<Cliente>listaClientes)
        {
            long idCliente;
            string cpf, nome, telefone, logradouro, bairro, cidade, estado, cep;
            DateTime dataNascimento;
            
            Console.WriteLine("\nPreencha o formulário abaixo com os dados do cliente:");
            Console.Write("\nCPF: ");
            cpf = Console.ReadLine();
            if(ValidaCpf(listaClientes, cpf) == false)
            {
                Console.Write("Nome: ");
                nome = Console.ReadLine();
                Console.Write("Data de Nascimento(dd/mm/yyyy): ");
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
                Console.WriteLine($"Cliente cadastrado com sucesso! \nSeu Id é: {cliente.IdCliente}");
            }else Console.WriteLine("Cliente já cadastrado!\nPressione qualquer tecla para voltar ao Menu Principal");
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
    }
}
