using AgendaConsutorio.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaConsutorio.service
{
    public class AgendaService
    {
        public List<Agendamento> Agendamentos { get; private set; } = new List<Agendamento>();

        public bool VerificarConflito(DateTime data, TimeSpan horaInicio, TimeSpan horaFim)
        {
            return Agendamentos.Any(a =>
                a.DataConsulta == data &&
                ((horaInicio >= a.HoraInicio && horaInicio < a.HoraFim) ||
                 (horaFim > a.HoraInicio && horaFim <= a.HoraFim)));
        }

        public void AgendarConsulta(Agendamento agendamento)
        {
            if (VerificarConflito(agendamento.DataConsulta, agendamento.HoraInicio, agendamento.HoraFim))
            {
                Console.WriteLine("Erro: Conflito de horário com outro agendamento.");
                return;
            }
            Agendamentos.Add(agendamento);
            Console.WriteLine("Consulta agendada com sucesso.");
        }

        public void AlterarAgendamento(string cpf, DateTime dataConsulta, TimeSpan horaInicio, TimeSpan horaFim)
        {
            var agendamento = Agendamentos.FirstOrDefault(a => a.CPF == cpf && a.DataConsulta == dataConsulta && a.HoraInicio == horaInicio);
            if (agendamento == null)
            {
                Console.WriteLine("Erro: Agendamento não encontrado.");
                return;
            }

            // Verifica se existe conflito com o novo horário
            if (VerificarConflito(dataConsulta, horaInicio, horaFim))
            {
                Console.WriteLine("Erro: Não é possível alterar, há um conflito de horário.");
                return;
            }

            // Atualiza os dados do agendamento
            agendamento.DataConsulta = dataConsulta;
            agendamento.HoraInicio = horaInicio;
            agendamento.HoraFim = horaFim;
            Console.WriteLine("Agendamento alterado com sucesso.");
        }

        public void CancelarAgendamento(string cpf, DateTime dataConsulta, TimeSpan horaInicio)
        {
            var agendamento = Agendamentos.FirstOrDefault(a => a.CPF == cpf && a.DataConsulta == dataConsulta && a.HoraInicio == horaInicio);
            if (agendamento != null)
            {
                Agendamentos.Remove(agendamento);
                Console.WriteLine("Agendamento cancelado com sucesso.");
            }
            else
            {
                Console.WriteLine("Erro: Agendamento não encontrado.");
            }
        }

        public void ListarAgenda()
        {
            if (Agendamentos.Count == 0)
            {
                Console.WriteLine("Nenhum agendamento encontrado.");
                return;
            }

            foreach (var agendamento in Agendamentos)
            {
                Console.WriteLine($"Paciente: {agendamento.CPF} | Data: {agendamento.DataConsulta.ToShortDateString()} | Hora: {agendamento.HoraInicio} - {agendamento.HoraFim}");
            }
        }

        public void ListarAgenda(DateTime dataInicial, DateTime dataFinal)
        {
            var agendamentosFiltrados = Agendamentos.Where(a => a.DataConsulta >= dataInicial && a.DataConsulta <= dataFinal).ToList();
            if (!agendamentosFiltrados.Any())
            {
                Console.WriteLine("Nenhum agendamento encontrado no intervalo especificado.");
                return;
            }

            foreach (var agendamento in agendamentosFiltrados)
            {
                Console.WriteLine($"Paciente: {agendamento.CPF} | Data: {agendamento.DataConsulta.ToShortDateString()} | Hora: {agendamento.HoraInicio} - {agendamento.HoraFim}");
            }
        }
    }
}
