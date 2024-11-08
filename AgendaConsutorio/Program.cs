using System;
using System.Collections.Generic;
using System.Linq;
using AgendaConsutorio.service;
using AgendaConsutorio.dto;
using AgendaConsutorio.model;

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
                    Console.Write("CPF: ");
                    string cpf = Console.ReadLine();

                    // Verifica se o CPF já está cadastrado
                    if (service.PacienteExiste(cpf))
                    {
                        Console.WriteLine("Erro: CPF já cadastrado.");
                        break;
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
                    break;

                case "2":
                    Console.Write("Digite o CPF do paciente a ser excluído: ");
                    service.ExcluirPaciente(Console.ReadLine());
                    break;

                case "3":
                    Console.Write("Deseja ordenar por nome (S/N)? ");
                    bool ordenarPorNome = Console.ReadLine().ToUpper() == "S";
                    service.ListarPacientes(ordenarPorNome);
                    break;

                case "4":
                    return;

                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
            Console.ReadKey();
        }

        static void MenuAgenda(ConsultorioService service)
        {
            Console.Clear();
            Console.WriteLine("Agenda");
            Console.WriteLine("1-Agendar consulta");
            Console.WriteLine("2-Cancelar agendamento");
            Console.WriteLine("3-Listar agenda");
            Console.WriteLine("4-Voltar");
            Console.Write("Escolha uma opção: ");
            string opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    Console.Write("CPF: ");
                    string cpf = Console.ReadLine();

                    // Verifica se o CPF não está cadastrado
                    if (!service.PacienteExiste(cpf))
                    {
                        Console.WriteLine("Erro: paciente não cadastrado.");
                        break;
                    }

                    // Verifica se já existe um agendamento futuro para o CPF
                    if (service.PossuiAgendamentoFuturo(cpf))
                    {
                        Console.WriteLine("Erro: paciente já possui um agendamento futuro.");
                        break;
                    }

                    Console.Write("Data da consulta (dd/MM/yyyy): ");
                    DateTime dataConsulta;
                    if (!DateTime.TryParse(Console.ReadLine(), out dataConsulta))
                    {
                        Console.WriteLine("Data inválida.");
                        return;
                    }
                    Console.Write("Hora de início (HH:mm): ");
                    TimeSpan horaInicio;
                    if (!TimeSpan.TryParse(Console.ReadLine(), out horaInicio))
                    {
                        Console.WriteLine("Hora inválida.");
                        return;
                    }
                    Console.Write("Hora de fim (HH:mm): ");
                    TimeSpan horaFim;
                    if (!TimeSpan.TryParse(Console.ReadLine(), out horaFim))
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
                    break;

                case "2":
                    Console.Write("Digite o CPF do paciente: ");
                    cpf = Console.ReadLine();
                    Console.Write("Data da consulta (dd/MM/yyyy): ");
                    if (!DateTime.TryParse(Console.ReadLine(), out dataConsulta))
                    {
                        Console.WriteLine("Data inválida.");
                        return;
                    }
                    Console.Write("Hora de início (HH:mm): ");
                    if (!TimeSpan.TryParse(Console.ReadLine(), out horaInicio))
                    {
                        Console.WriteLine("Hora inválida.");
                        return;
                    }
                    service.CancelarAgendamento(cpf, dataConsulta, horaInicio);
                    break;

                case "3":
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
                    break;

                case "4":
                    return;

                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
            Console.ReadKey();
        }

    }
}
