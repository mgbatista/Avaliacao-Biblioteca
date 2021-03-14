using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaAlugueis
{
    public class Cliente
    {
        public Cliente(long idCliente, string cpf, string nome, DateTime dataNascimento, string telefone, string logradouro, string bairro, string cidade, string estado, string cep)
        {
            IdCliente = idCliente;
            CPF = cpf;
            Nome = nome;
            DataNascimento = dataNascimento;
            Telefone = telefone;
            Logradouro = logradouro;
            Bairro = bairro;
            Cidade = cidade;
            Estado = estado;
            CEP = cep;
        }
        public long IdCliente { get; set; }
        public string CPF { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Telefone { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string CEP { get; set; }


        public override string ToString()
        {
            return ("Id: " + IdCliente + 
                "\nCPF: " + CPF + 
                "\nNome: " + Nome + 
                "\nData de Nascimento: " + DataNascimento + 
                "\nTelefone: " + Telefone + 
                "\nLogradouro: " + Logradouro + 
                "\nBairro: " + Bairro + 
                "\nCidade: " + Cidade + 
                "\nEstado: " + Estado + 
                "\nCEP: " + CEP);
        }
    }
}
