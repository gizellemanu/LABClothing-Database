using System.ComponentModel.DataAnnotations;


namespace labclothingcollectionbd.Models
{
        public class Usuarios : Pessoas
        {
            [Required(ErrorMessage = "O campo Email é de preenchimento obrigatório")]
            [MaxLength(50, ErrorMessage = "O campo Email não pode exceder 50 caracteres")]
            [RegularExpression(@"^[\w\.-]+@[\w\.-]+\.\w+$", ErrorMessage = "O campo Email possui um formato inválido!")]
            public string Email { get; set; }

            [Required(ErrorMessage = "O campo Tipo Usuario é de preenchimento obrigatório")]
            [RegularExpression("^(Administrador|Gerente|Criador|Outro)$", ErrorMessage = "O campo Tipo Usuario deve receber somente as seguintes atribuições: Administrador, Gerente, Criador, Outro.")]
            public string TipoUsuario { get; set; }

            [Required(ErrorMessage = "O campo Estado Usuário é de preenchimento obrigatório")]
            [RegularExpression("^(Ativo|ativo|Inativo|inativo)$", ErrorMessage = "O campo Estado no Sistema deve receber somentte as seguintes atribuições: Ativo ou ativo, Inativo ou inativo")]
            public string EstadoUsuario { get; set; }
        }
    }