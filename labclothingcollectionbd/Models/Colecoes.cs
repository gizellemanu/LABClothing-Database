using System.ComponentModel.DataAnnotations;

namespace labclothingcollectionbd.Models
{
    public class Colecoes
    {

        [Key]
        public int IdColecaoRelacionada { get; set; }

        [Required(ErrorMessage = "O campo Nome da Coleção é de preenchimento obrigatório")]
        [MaxLength(200, ErrorMessage = "O campo Nome não pode exceder 200 caracteres")]
        public string NomeColecao { get; set; }

        [Required(ErrorMessage = "O campo Id Responsavel é de preenchimento obrigatório")]
        public int IdResponsavel { get; set; }

        [Required(ErrorMessage = "O campo Marca é de preenchimento obrigatório")]
        [MaxLength(30, ErrorMessage = "O campo Marca não pode exceder 30 caracteres")]
        public string Marca { get; set; }

        [Required(ErrorMessage = "O campo Orçamento é de preenchimento obrigatório")]
        public decimal Orcamento { get; set; }

        [Required(ErrorMessage = "O campo Ano de Lançameno é de preenchimento obrigatório")]
        [RegularExpression(@"^(19|20)\d{2}$", ErrorMessage = "O campo Ano de Lançamento deve estar entre 1900-2099.")]
        public int AnoLancamento { get; set; }

        [Required(ErrorMessage = "O campo Estação é de preenchimento obrigatório")]
        [MaxLength(20, ErrorMessage = "O campo Estação não pode exceder 20 caracteres")]
        [RegularExpression("^(Outono|Inverno|Primavera|Verão|outono|inverno|primavera|verão)$", ErrorMessage = "O campo Estação deve deve receber somentte as seguintes atribuições: Outono, Inverno, Primavera, Verao.")]
        public string Estacao { get; set; }

        [Required(ErrorMessage = "O campo Estato do Sistema é de preenchimento obrigatório")]
        [MaxLength(20, ErrorMessage = "O campo Estato do Sistema não pode exceder 20 caracteres")]
        [RegularExpression("^(Ativo|Inativo|ativo|inativo)$", ErrorMessage = "O campo Estado no Sistema deve receber somentte as seguintes atribuições: Ativo ou ativo, Inativo ou inativo")]
        public string EstadoSistema { get; set; }
    }
}