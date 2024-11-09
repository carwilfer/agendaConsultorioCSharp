
# Agenda Consutório


Esta aplicação permite o gerenciamento de pacientes e agendamentos de consultas em um para uma futura consulta em um consultório. Através de um menu interativo, você pode cadastrar pacientes, agendar consultas, alterar, excluir e listar informações sobre pacientes e agendamentos.

Funcionalidades
A aplicação oferece os seguintes menus e funcionalidades:

Menu Principal
Cadastro de Pacientes

Incluir Paciente: Adiciona um novo paciente ao sistema, solicitando o CPF, nome e data de nascimento.
Excluir Paciente: Permite excluir um paciente baseado no CPF.
Listar Pacientes: Exibe a lista de pacientes cadastrados, com a opção de ordená-los por nome.
Agenda

Agendar Consulta: Agenda uma consulta para um paciente, solicitando o CPF, data e horário da consulta.
Cancelar Agendamento: Cancela um agendamento existente, solicitando o CPF e os dados do agendamento.
Listar Agenda: Exibe os agendamentos no sistema, com a opção de filtrar por intervalo de datas.
Alterar Agendamento: Permite alterar um agendamento, incluindo a mudança de data e hora.
Fim: Encerra a aplicação.

O projeto é dividido em dois serviços principais que gerenciam as funcionalidades de uma agenda de consultas médicas e a administração de pacientes em um consultório.

1. AgendaService
A classe AgendaService é responsável por gerenciar agendamentos e inclui métodos para verificar conflitos, agendar, alterar, cancelar e listar consultas.

Métodos Principais:
VerificarConflito: Verifica se há conflito de horário entre um novo agendamento e os existentes.

AgendarConsulta: Adiciona um novo agendamento, se não houver conflito.

AlterarAgendamento: Permite alterar um agendamento existente, verificando conflitos antes da modificação.

CancelarAgendamento: Remove um agendamento específico, se encontrado.

ListarAgenda: Exibe todos os agendamentos ou filtra por intervalo de datas.


2. ConsultorioService
A classe ConsultorioService expande a funcionalidade de AgendaService, incorporando o gerenciamento de pacientes. Ela mantém listas de pacientes e agendamentos, possibilitando operações como cadastro e exclusão de pacientes, além da manipulação de consultas.

Métodos Principais:
AdicionarPaciente: Adiciona um novo paciente, verificando duplicação de CPF.
ExcluirPaciente: Remove um paciente e seus agendamentos futuros, desde que não haja conflitos.

AgendarConsulta: Agenda uma consulta para um paciente, validando a existência do paciente e conflitos de horário.

CancelarAgendamento: Cancela uma consulta específica de um paciente.

AlterarAgendamento: Modifica detalhes de um agendamento existente, garantindo que não haja conflito com outros horários.

ListarPacientes: Lista todos os pacientes, com opção de ordenação por nome ou CPF.

ListarAgenda: Lista todos os agendamentos em um intervalo de datas.

Verificações e Validações:
VerificarConflito: Confirma se o novo horário de agendamento não se sobrepõe a outro existente.

PacienteExiste: Verifica a existência de um paciente pelo CPF.

PossuiAgendamentoFuturo: Checa se um paciente tem agendamentos futuros.

Funcionalidades Específicas:
Gerenciamento de Conflitos: Ambas as classes possuem métodos que asseguram que agendamentos não se sobreponham, protegendo a integridade da agenda.

Listagem de Pacientes e Consultas: O serviço permite a visualização detalhada dos pacientes e suas consultas, incluindo a idade e as datas de consultas agendadas.

Operações de CRUD: Os métodos oferecem um conjunto robusto de operações CRUD (Create, Read, Update, Delete) para pacientes e agendamentos.

Classe Paciente
A classe Paciente representa um paciente do consultório e possui as seguintes propriedades e métodos:

Propriedades:
CPF: string que armazena o CPF do paciente. Esta propriedade é privada e só pode ser definida através do construtor.
Nome: string que armazena o nome do paciente. Também é privada e definida pelo construtor.
DataNascimento: DateTime que armazena a data de nascimento do paciente. É privada e definida no momento da criação da instância.
Construtor: O construtor da classe recebe três parâmetros (cpf, nome, dataNascimento) e valida:

O CPF usando o método ValidacaoCPF().
O nome deve ter pelo menos 5 caracteres.
A idade calculada pelo método CalcularIdade() deve ser de no mínimo 13 anos.

Métodos:
CalcularIdade(DateTime dataNascimento): calcula a idade do paciente com base na data de nascimento fornecida.

ValidacaoCPF(string cpf): realiza a validação do CPF, removendo caracteres não numéricos e verificando os dígitos verificadores. Retorna true se o CPF for válido; caso contrário, false.

Exceções:
ArgumentException é lançada caso o CPF seja inválido, o nome tenha menos de 5 caracteres ou a idade do paciente seja menor que 13 anos.

Classe Agendamento
A classe Agendamento é responsável por representar um agendamento de consulta e inclui:

Propriedades:
CPF: string que identifica o paciente associado à consulta.
DataConsulta: DateTime que representa a data da consulta.
HoraInicio: TimeSpan que indica o horário de início da consulta.
HoraFim: TimeSpan que indica o horário de término da consulta.
Construtor: Recebe os parâmetros cpf, dataConsulta, horaInicial e horaFinal, validando:

horaFinal deve ser maior que horaInicial.
Os minutos de horaInicial e horaFinal devem ser múltiplos de 15, garantindo que os horários estejam alinhados com intervalos de 15 minutos.
O horário deve estar dentro do período de funcionamento do consultório (entre 8:00 e 19:00).

Métodos:
IsFuturo(): verifica se a consulta está agendada para uma data e horário futuros. Retorna true se for o caso; caso contrário, false.

Exceções:
ArgumentException é lançada se o horário final for menor ou igual ao inicial, se os minutos não estiverem em intervalos de 15 minutos ou se o horário estiver fora do período de funcionamento.
Regras de Negócio Implementadas
Validação de CPF: Os CPFs são validados utilizando um método que elimina caracteres não numéricos e verifica a validade dos dígitos verificadores com base no algoritmo de CPF.
Regras de Agendamento:
Os horários de consulta devem respeitar intervalos de 15 minutos.
Consultas devem ser realizadas dentro do horário de funcionamento, entre 8:00 e 19:00.
A hora de término da consulta deve ser maior que a de início.
Cadastro de Pacientes:
Nomes de pacientes devem ter no mínimo 5 caracteres.
A idade mínima permitida para pacientes é de 13 anos.

Como Usar
Clone ou baixe o repositório.
Abra o projeto na IDE de sua preferência (recomenda-se o Visual Studio).
Compile e execute o programa.
No menu principal, escolha a opção desejada para gerenciar pacientes ou agendamentos.
Exemplo de Execução
bash
Copiar código
Menu Principal
1-Cadastro de pacientes
2-Agenda
3-Fim
Escolha uma opção: 1
Cadastro de pacientes
1-Incluir paciente
2-Excluir paciente
3-Listar pacientes
4-Voltar
Escolha uma opção: 1
CPF: 12345678900
Nome: João da Silva
Data de nascimento (dd/MM/yyyy): 15/08/1990
Estrutura do Código
A aplicação é estruturada da seguinte forma:

Program.cs: Contém a lógica principal da aplicação, incluindo a interação com o usuário através do console.
ConsultorioService.cs: Serviço responsável pela lógica de negócios, como cadastro de pacientes e agendamento de consultas.
Paciente.cs: Representa um paciente, com informações como CPF, nome e data de nascimento.
Agendamento.cs: Representa um agendamento de consulta, com dados do paciente, data e horário.
Pré-requisitos
.NET 6.0 ou superior
IDE de sua escolha (Visual Studio, Visual Studio Code, etc.)