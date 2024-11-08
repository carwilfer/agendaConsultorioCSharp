using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaConsutorio.dto
{
    public class AgendamentoDTO
    {
        public int Id { get; set; }
        public string CPF { get; set; }
        public DateTime DataConsulta { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFim { get; set; }
        public PacienteDTO Paciente { get; set; }
    }

}
