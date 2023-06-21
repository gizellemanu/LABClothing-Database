using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace labclothingcollection.DTO.UsuariosDTO.Request
{
    public class EstadoUsuarioRequestDTO
    {
        [Required(ErrorMessage = "O campo Estado Usuário é de preenchimento obrigatório")]
        [RegularExpression("^(Ativo|ativo|Inativo|inativo)$", ErrorMessage = "O campo Estado no Sistema deve receber somentte as seguintes atribuições: Ativo ou ativo, Inativo ou inativo")]
        public string EstadoUsuario { get; set; }
    }
}
