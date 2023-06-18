using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace labclothingcollection.DTO.ModelosDTO.Response
{
    public class ModelosResponseDTO
    {
        public int IdModelo { get; set; }
        public int IdColecaoRelacionada { get; set; }
        public string NomeModelo { get; set; }
        public string Tipo { get; set; }
        public string Layout { get; set; }
    }
}
