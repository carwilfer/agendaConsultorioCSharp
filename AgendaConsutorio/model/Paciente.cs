using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AgendaConsutorio.model
{
    public class Paciente
    {
        public string CPF { get; private set; }
        public string Nome { get; private set; }
        public DateTime DataNascimento { get; private set; }

        public Paciente(string cpf, string nome, DateTime dataNascimento)
        {
            if (!ValidacaoCPF(cpf))
                throw new ArgumentException("CPF inválido.");
            if (nome.Length < 5)
                throw new ArgumentException("O nome deve ter pelo menos 5 caracteres.");
            if (CalcularIdade(dataNascimento) < 13)
                throw new ArgumentException("Paciente deve ter pelo menos 13 anos.");

            CPF = cpf;
            Nome = nome;
            DataNascimento = dataNascimento;
        }

        public int CalcularIdade(DateTime dataNascimento)
        {
            var hoje = DateTime.Today;
            var idade = hoje.Year - dataNascimento.Year;
            if (dataNascimento.Date > hoje.AddYears(-idade)) idade--;
            return idade;
        }

        private bool ValidacaoCPF(string cpf)
        {
            cpf = Regex.Replace(cpf, @"[^\d]", "");

            if (cpf.Length != 11 || Regex.IsMatch(cpf, @"^(.)\1{10}$"))
                return false;

            int soma = 0;
            for (int i = 0; i < 9; i++)
                soma += int.Parse(cpf[i].ToString()) * (10 - i);

            int resto = soma % 11;
            int digito1 = (resto < 2) ? 0 : 11 - resto;

            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(cpf[i].ToString()) * (11 - i);

            resto = soma % 11;
            int digito2 = (resto < 2) ? 0 : 11 - resto;

            return (digito1 == int.Parse(cpf[9].ToString()) && digito2 == int.Parse(cpf[10].ToString()));
        }
    }
}
