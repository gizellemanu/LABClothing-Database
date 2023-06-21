using System.ComponentModel.DataAnnotations;


namespace labclothingcollection.DTO.ColecoesDTO.Request
{
    public class EstadoSistemaColecaoRequestDTO
    {
        [Required(ErrorMessage = "O campo Estato do Sistema é de preenchimento obrigatório")]
        [MaxLength(20, ErrorMessage = "O campo Estato do Sistema não pode exceder 20 caracteres")]
        [RegularExpression("^(Ativo|Inativo)$", ErrorMessage = "O campo Estado no Sistema deve receber somentte as seguintes atribuições: Ativo ou Inativo")]
        public string EstadoSistema { get; set; }
    }
}
