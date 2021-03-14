using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaAlugueis
{
    public class Cliente
    {
        public long IdCliente { get; set; }
        public string CPF { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Telefone { get; set; }


        public override string ToString()
        {
            return ("Id: " + IdCliente + "\nCPF: " + CPF + "\nNome: " + Nome + "\nData de Nascimento: " + DataNascimento + "\nTelefone: " + Telefone);
        }

    }
}
