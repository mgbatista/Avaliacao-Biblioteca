using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaAlugueis
{
    class Cliente
    {
        public long IdCliente { get; set; }
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Telefone { get; set; }


        public override string ToString()
        {
            return ("Id: " + IdCliente + "\nCPF: " + Cpf + "\nNome: " + Nome + "\nData de Nascimento: " + DataNascimento + "\nTelefone: " + Telefone);
        }

    }
}
