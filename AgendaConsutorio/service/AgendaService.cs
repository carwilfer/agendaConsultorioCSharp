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
    }
}
