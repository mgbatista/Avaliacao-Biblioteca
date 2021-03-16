using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaAlugueis
{
    public class EmprestimoLivro
    {
        /*public EmprestimoLivro(long idCliente, long numeroTombo, DateTime dataEmprestimo, DateTime dataDevolucao, int statusEmprestimo)
        {
            IdCliente = idCliente;
            NumeroTombo = numeroTombo;
            DataEmprestimo = dataEmprestimo;
            DataDevolucao = dataDevolucao;
            StatusEmprestimo = statusEmprestimo;
        }*/

        public long IdCliente { get; set; }
        public long NumeroTombo { get; set; }
        public DateTime DataEmprestimo { get; set; }
        public DateTime DataDevolucao { get; set; }
        public int StatusEmprestimo { get; set; }

        public override string ToString()
        {
            return ("IdCliente: " + IdCliente + "\nNúmeroTombo: " + NumeroTombo + "\nData Empréstimo: " + DataEmprestimo + 
                "\nData Devolução: " + DataDevolucao + "\nStatus Empréstimo: " + StatusEmprestimo);
        }

    }
}
