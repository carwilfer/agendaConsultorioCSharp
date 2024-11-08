using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaConsutorio.dto
{
    public class PacienteDTO
    {
        public string CPF { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public AgendamentoDTO AgendamentoFuturo { get; set; }
    }
}
