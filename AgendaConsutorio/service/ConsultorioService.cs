using AgendaConsutorio.dto;
using AgendaConsutorio.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaConsutorio.service
{
    public class ConsultorioService
    {
        private List<Paciente> pacientes = new List<Paciente>();
        private List<Agendamento> agendamentos = new List<Agendamento>();

        public void AdicionarPaciente(Paciente paciente)
        {
            if (pacientes.Any(p => p.CPF == paciente.CPF))
            {
                Console.WriteLine("Erro: CPF já cadastrado");
                return;
            }

            pacientes.Add(paciente);
            Console.WriteLine("Paciente cadastrado com sucesso!");
        }

        public void ExcluirPaciente(string cpf)
        {
            var paciente = pacientes.FirstOrDefault(p => p.CPF == cpf);
            if (paciente == null)
            {
                Console.WriteLine("Erro: paciente não cadastrado");
                return;
            }
            if (agendamentos.Any(a => a.CPF == cpf && a.IsFuturo()))
            {
                Console.WriteLine("Erro: paciente está agendado.");
                return;
            }

            pacientes.Remove(paciente);
            agendamentos.RemoveAll(a => a.CPF == cpf);
            Console.WriteLine("Paciente excluído com sucesso!");
        }

        public void AgendarConsulta(Agendamento agendamento)
        {
            if (!pacientes.Any(p => p.CPF == agendamento.CPF))
            {
                Console.WriteLine("Erro: paciente não cadastrado");
                return;
            }

            if (!agendamento.IsFuturo())
            {
                Console.WriteLine("Erro: o agendamento deve ser para uma data e hora futura.");
                return;
            }

            if (agendamentos.Any(a => a.DataConsulta == agendamento.DataConsulta &&
                                      ((a.HoraInicio < agendamento.HoraFim && a.HoraFim > agendamento.HoraInicio))))
            {
                Console.WriteLine("Erro: já existe uma consulta agendada nesse horário");
                return;
            }

            agendamentos.Add(agendamento);
            Console.WriteLine("Agendamento realizado com sucesso!");
        }

        public void CancelarAgendamento(string cpf, DateTime dataConsulta, TimeSpan horaInicio)
        {
            var agendamento = agendamentos.FirstOrDefault(a => a.CPF == cpf &&
                                                               a.DataConsulta == dataConsulta &&
                                                               a.HoraInicio == horaInicio &&
                                                               a.IsFuturo());
            if (agendamento == null)
            {
                Console.WriteLine("Erro: agendamento não encontrado");
                return;
            }

            agendamentos.Remove(agendamento);
            Console.WriteLine("Agendamento cancelado com sucesso!");
        }

        public void ListarPacientes(bool ordenarPorNome)
        {
            var listaOrdenada = ordenarPorNome
                ? pacientes.OrderBy(p => p.Nome).ToList()
                : pacientes.OrderBy(p => p.CPF).ToList();

            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("CPF Nome                          Dt.Nasc.  Idade");
            Console.WriteLine("------------------------------------------------------------");

            foreach (var paciente in listaOrdenada)
            {
                Console.WriteLine($"{paciente.CPF} {paciente.Nome,-30} {paciente.DataNascimento:dd/MM/yyyy} {paciente.CalcularIdade(paciente.DataNascimento)}");
                var agendamento = agendamentos.FirstOrDefault(a => a.CPF == paciente.CPF && a.IsFuturo());
                if (agendamento != null)
                {
                    Console.WriteLine($"Agendado para: {agendamento.DataConsulta:dd/MM/yyyy}");
                    Console.WriteLine($"{agendamento.HoraInicio:hh\\:mm} às {agendamento.HoraFim:hh\\:mm}");
                }
            }
            Console.WriteLine("------------------------------------------------------------");
        }

        public void ListarAgenda(DateTime? dataInicial = null, DateTime? dataFinal = null)
        {
            var agendaFiltrada = agendamentos
                .Where(a => (!dataInicial.HasValue || a.DataConsulta >= dataInicial.Value) &&
                            (!dataFinal.HasValue || a.DataConsulta <= dataFinal.Value))
                .OrderBy(a => a.DataConsulta).ThenBy(a => a.HoraInicio).ToList();

            Console.WriteLine("-------------------------------------------------------------");
            Console.WriteLine("Data        H.Ini H.Fim  Tempo   Nome                          Dt.Nasc.");
            Console.WriteLine("-------------------------------------------------------------");

            foreach (var agendamento in agendaFiltrada)
            {
                var paciente = pacientes.First(p => p.CPF == agendamento.CPF);
                var tempoConsulta = agendamento.HoraFim - agendamento.HoraInicio;
                Console.WriteLine($"{agendamento.DataConsulta:dd/MM/yyyy} {agendamento.HoraInicio:hh\\:mm} {agendamento.HoraFim:hh\\:mm}  {tempoConsulta:hh\\:mm}   {paciente.Nome,-30} {paciente.DataNascimento:dd/MM/yyyy}");
            }
            Console.WriteLine("-------------------------------------------------------------");
        }

        public bool PacienteExiste(string cpf)
        {
            return pacientes.Any(p => p.CPF == cpf);
        }

        public bool PossuiAgendamentoFuturo(string cpf)
        {
            return agendamentos.Any(a => a.CPF == cpf && a.IsFuturo());
        }
    }
}