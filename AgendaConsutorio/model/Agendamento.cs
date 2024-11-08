using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaConsutorio.model
{
    public class Agendamento
    {
        public string CPF { get; private set; }
        public DateTime DataConsulta { get; private set; }
        public TimeSpan HoraInicio { get; private set; }
        public TimeSpan HoraFim { get; private set; }

        public Agendamento(string cpf, DateTime dataConsulta, TimeSpan horaInicial, TimeSpan horaFinal)
        {
            if (horaFinal <= horaInicial)
                throw new ArgumentException("A hora final deve ser maior que a hora inicial.");
            if (horaInicial.Minutes % 15 != 0 || horaFinal.Minutes % 15 != 0)
                throw new ArgumentException("Horas devem ser de 15 em 15 minutos.");
            if (horaInicial.Hours < 8 || horaFinal.Hours > 19 || horaFinal.Hours < 8)
                throw new ArgumentException("Horário fora do funcionamento do consultório.");

            CPF = cpf;
            DataConsulta = dataConsulta;
            HoraInicio = horaInicial;
            HoraFim = horaFinal;
        }

        public bool IsFuturo()
        {
            return DataConsulta > DateTime.Now.Date ||
                   (DataConsulta == DateTime.Now.Date && HoraInicio > DateTime.Now.TimeOfDay);
        }
    }
}
