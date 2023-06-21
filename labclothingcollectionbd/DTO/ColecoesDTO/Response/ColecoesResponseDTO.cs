using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace labclothingcollection.DTO.ColecoesDTO.Response
{
    public class ColecoesResponseDTO
    {
        public int IdColecaoRelacionada { get; set; }

        public string NomeColecao { get; set; }

        public int IdResponsavel { get; set; }

        public string Marca { get; set; }

        public int Orcamento { get; set; }

        public int AnoLancamento { get; set; }

        public string Estacao { get; set; }

        public string EstadoSistema { get; set; }
    }
}
