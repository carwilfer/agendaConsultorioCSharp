using System;
using System.Collections.Generic;
using System.Linq;
using AgendaConsutorio.service;
using AgendaConsutorio.dto;
using AgendaConsutorio.model;

using System.Text.RegularExpressions;

namespace AgendaConsultorio
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = new ConsultorioService();
            bool sair = false;

            while (!sair)
            {
                Console.Clear();
                Console.WriteLine("Menu Principal");
                Console.WriteLine("1-Cadastro de pacientes");
                Console.WriteLine("2-Agenda");
                Console.WriteLine("3-Fim");
                Console.Write("Escolha uma opção: ");
                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        MenuCadastroPacientes(service);
                        break;
                    case "2":
                        MenuAgenda(service);
                        break;
                    case "3":
                        sair = true;
                        break;
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }
            }
        }

        static void MenuCadastroPacientes(ConsultorioService service)
        {
            bool voltar = false;
            while (!voltar)
            {
                Console.Clear();
                Console.WriteLine("Cadastro de pacientes");
                Console.WriteLine("1-Incluir paciente");
                Console.WriteLine("2-Excluir paciente");
                Console.WriteLine("3-Listar pacientes");
                Console.WriteLine("4-Voltar");
                Console.Write("Escolha uma opção: ");
                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        IncluirPaciente(service);
                        break;
                    case "2":
                        ExcluirPaciente(service);
                        break;
                    case "3":
                        ListarPacientes(service);
                        break;
                    case "4":
                        voltar = true; // Voltar para o menu principal
                        break;
                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }
                Console.ReadKey();
            }
        }

        static void MenuAgenda(ConsultorioService service)
        {
            bool voltar = false;
            while (!voltar)
            {
                Console.Clear();
                Console.WriteLine("Agenda");
                Console.WriteLine("1-Agendar consulta");
                Console.WriteLine("2-Cancelar agendamento");
                Console.WriteLine("3-Listar agenda");
                Console.WriteLine("4-Alterar agendamento");
                Console.WriteLine("5-Voltar");
                Console.Write("Escolha uma opção: ");
                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        AgendarConsulta(service);
                        break;
                    case "2":
                        CancelarAgendamento(service);
                        break;
                    case "3":
                        ListarAgenda(service);
                        break;
                    case "4":
                        AlterarAgendamento(service);  // Nova opção
                        break;
                    case "5":
                        voltar = true; // Voltar para o menu principal
                        break;
                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }
                Console.ReadKey();
            }
        }

        // Métodos específicos para cada ação de menu
        static void IncluirPaciente(ConsultorioService service)
        {
            Console.Write("CPF: ");
            string cpf = Console.ReadLine();
            if (service.PacienteExiste(cpf))
            {
                Console.WriteLine("Erro: CPF já cadastrado.");
                return;
            }
            Console.Write("Nome: ");
            string nome = Console.ReadLine();
            Console.Write("Data de nascimento (dd/MM/yyyy): ");
            DateTime dataNascimento;
            if (!DateTime.TryParse(Console.ReadLine(), out dataNascimento))
            {
                Console.WriteLine("Data de nascimento inválida.");
                return;
            }

            try
            {
                var paciente = new Paciente(cpf, nome, dataNascimento);
                service.AdicionarPaciente(paciente);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erro: {e.Message}");
            }
        }

        static void AlterarAgendamento(ConsultorioService service)
        {
            Console.Write("Digite o CPF do paciente: ");
            string cpf = Console.ReadLine();
            Console.Write("Data da consulta (dd/MM/yyyy): ");
            DateTime dataConsulta;
            if (!DateTime.TryParse(Console.ReadLine(), out dataConsulta))
            {
                Console.WriteLine("Data inválida.");
                return;
            }
            Console.Write("Hora de início (HH:mm): ");
            TimeSpan horaInicio = LerHora();
            if (horaInicio == TimeSpan.MinValue)
            {
                Console.WriteLine("Hora inválida.");
                return;
            }
            Console.Write("Hora de fim (HH:mm): ");
            TimeSpan horaFim = LerHora();  // Adicionando a leitura para a hora de fim
            if (horaFim == TimeSpan.MinValue)
            {
                Console.WriteLine("Hora inválida.");
                return;
            }

            Console.Write("Nova data da consulta (dd/MM/yyyy): ");
            DateTime novaDataConsulta;
            if (!DateTime.TryParse(Console.ReadLine(), out novaDataConsulta))
            {
                Console.WriteLine("Data inválida.");
                return;
            }

            Console.Write("Nova hora de início (HH:mm): ");
            TimeSpan novaHoraInicio = LerHora();
            if (novaHoraInicio == TimeSpan.MinValue)
            {
                Console.WriteLine("Hora inválida.");
                return;
            }

            Console.Write("Nova hora de fim (HH:mm): ");
            TimeSpan novaHoraFim = LerHora();  // Adicionando a leitura para a nova hora de fim
            if (novaHoraFim == TimeSpan.MinValue)
            {
                Console.WriteLine("Hora inválida.");
                return;
            }

            // Verificação mais detalhada para debugar
            Console.WriteLine($"Tentando alterar agendamento de CPF: {cpf}, Data: {dataConsulta.ToShortDateString()}, Hora Início: {horaInicio}, Nova Data: {novaDataConsulta.ToShortDateString()}, Nova Hora Início: {novaHoraInicio}");

            service.AlterarAgendamento(cpf, dataConsulta, horaInicio, horaFim, novaDataConsulta, novaHoraInicio, novaHoraFim);
        }

        static void ExcluirPaciente(ConsultorioService service)
        {
            Console.Write("Digite o CPF do paciente a ser excluído: ");
            service.ExcluirPaciente(Console.ReadLine());
        }

        static void ListarPacientes(ConsultorioService service)
        {
            Console.Write("Deseja ordenar por nome (S/N)? ");
            bool ordenarPorNome = Console.ReadLine().ToUpper() == "S";
            service.ListarPacientes(ordenarPorNome);
        }

        static void AgendarConsulta(ConsultorioService service)
        {
            Console.Write("CPF: ");
            string cpf = Console.ReadLine();
            if (!service.PacienteExiste(cpf))
            {
                Console.WriteLine("Erro: paciente não cadastrado.");
                return;
            }

            if (service.PossuiAgendamentoFuturo(cpf))
            {
                Console.WriteLine("Erro: paciente já possui um agendamento futuro.");
                return;
            }

            Console.Write("Data da consulta (dd/MM/yyyy): ");
            DateTime dataConsulta;
            if (!DateTime.TryParse(Console.ReadLine(), out dataConsulta))
            {
                Console.WriteLine("Data inválida.");
                return;
            }

            Console.Write("Hora de início (HH:mm): ");
            TimeSpan horaInicio = LerHora();
            if (horaInicio == TimeSpan.MinValue)
            {
                Console.WriteLine("Hora inválida.");
                return;
            }

            Console.Write("Hora de fim (HH:mm): ");
            TimeSpan horaFim = LerHora();
            if (horaFim == TimeSpan.MinValue)
            {
                Console.WriteLine("Hora inválida.");
                return;
            }

            try
            {
                var agendamento = new Agendamento(cpf, dataConsulta, horaInicio, horaFim);
                service.AgendarConsulta(agendamento);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erro: {e.Message}");
            }
        }

        static void CancelarAgendamento(ConsultorioService service)
        {
            Console.Write("Digite o CPF do paciente: ");
            string cpf = Console.ReadLine();
            Console.Write("Data da consulta (dd/MM/yyyy): ");
            DateTime dataConsulta;
            if (!DateTime.TryParse(Console.ReadLine(), out dataConsulta))
            {
                Console.WriteLine("Data inválida.");
                return;
            }
            Console.Write("Hora de início (HH:mm): ");
            TimeSpan horaInicio = LerHora();
            if (horaInicio == TimeSpan.MinValue)
            {
                Console.WriteLine("Hora inválida.");
                return;
            }
            service.CancelarAgendamento(cpf, dataConsulta, horaInicio);
        }

        static void ListarAgenda(ConsultorioService service)
        {
            Console.Write("Deseja listar com intervalo de datas (S/N)? ");
            bool intervalo = Console.ReadLine().ToUpper() == "S";
            if (intervalo)
            {
                Console.Write("Data inicial (dd/MM/yyyy): ");
                DateTime dataInicial;
                if (!DateTime.TryParse(Console.ReadLine(), out dataInicial))
                {
                    Console.WriteLine("Data inválida.");
                    return;
                }
                Console.Write("Data final (dd/MM/yyyy): ");
                DateTime dataFinal;
                if (!DateTime.TryParse(Console.ReadLine(), out dataFinal))
                {
                    Console.WriteLine("Data inválida.");
                    return;
                }
                service.ListarAgenda(dataInicial, dataFinal);
            }
            else
            {
                service.ListarAgenda();
            }
        }

        static TimeSpan LerHora()
        {
            string input = Console.ReadLine();
            // Se o formato for "0800", insere o ":" no formato correto
            if (!input.Contains(":") && input.Length == 4)
            {
                input = input.Insert(2, ":");
            }

            // Tenta converter para TimeSpan
            if (TimeSpan.TryParse(input, out TimeSpan hora))
            {
                return hora;
            }

            return TimeSpan.MinValue;  // Se a hora não for válida
        }
    }
}
