using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace labclothingcollectionbd.Models
{
    [Table("Pessoas")]
    public class Pessoas
    {
        [Key]
        public int IdPessoa { get; set; }

        [Required(ErrorMessage = "O campo Nome Completo é de preenchimento obrigatório")]
        [MaxLength(50, ErrorMessage = "O campo Nome Completo não pode exceder 50 caracteres")]
        public string NomeCompleto { get; set; }

        [Required(ErrorMessage = "O campo Genero é de preenchimento obrigatório")]
        [MaxLength(10, ErrorMessage = "O campo Genero não pode exceder 10 caracteres")]
        public string Genero { get; set; }

        [Required(ErrorMessage = "O campo Data Nascimento é de preenchimento obrigatório")]
        [RegularExpression(@"^(0[1-9]|1\d|2\d|3[01])-(0[1-9]|1[0-2])-\d{4}$", ErrorMessage = "O campo Data Nascimento possui um formato dd/mm/yyyyy!")]
        public string DataNascimento { get; set; }

        [Required(ErrorMessage = "O campo Cpf/CNPJ é de preenchimento obrigatório")]
        [MaxLength(20, ErrorMessage = "O campo Cpf/CNPJ não pode exceder 20 caracteres")]
        [RegularExpression(@"^\d{1,20}$", ErrorMessage = "O campo CPF/CNPJ possui um formato inválido")]
        public string CpfCnpj { get; set; }

        [Required(ErrorMessage = "O campo Telefone é de preenchimento obrigatório")]
        [MaxLength(15, ErrorMessage = "O campo Telefone não pode exceder 15 caracteres")]
        [RegularExpression(@"^\d{10,15}$", ErrorMessage = "O campo Telefone possui um formato inválido!")]
        public string Telefone { get; set; }

    }
}
