using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace labclothingcollectionbd.Models
{
    public class Modelos
    {
        [Key]
        public int IdModelo { get; set; }

        [Required(ErrorMessage = "O campo Nome Modelo é de preenchimento obrigatório")]
        [MaxLength(80, ErrorMessage = "O campo Nome Modelo não pode exceder 80 caracteres")]
        public string NomeModelo { get; set; }

        [ForeignKey(nameof(IdColecaoRelacionada))]
        [Required(ErrorMessage = "O campo Id Coleção Relacionada é de preenchimento obrigatório")]
        public int IdColecaoRelacionada { get; set; }

        [Required(ErrorMessage = "O campo Tipo é de preenchimento obrigatório")]
        [MaxLength(15, ErrorMessage = "O campo Estação deve receber somente as seguintes atribuições: Bermuda, Biquini, Bolsa, Bone, Calça, Calçados, Camisa, Chapéu, Saia.")]
        [RegularExpression("^Bermuda|bermuda|Biquini|biquini|Bolsa|bolsa|Bone|bone|Calça|calça|Calçados|calçados|Camisa|camisa|Chapéu|chapeu|Saia$", ErrorMessage = "O campo Estação deve receber somente as seguintes atribuições: Bermuda, Biquini, Bolsa, Bone, Calça, Calçados, Camisa, Chapéu, Saia.")]
        public string Tipo { get; set; }

        [Required(ErrorMessage = "O campo Layout é de preenchimento obrigatório")]
        [MaxLength(15, ErrorMessage = "O campo Layout deve receber somente as seguintes atribuições: Bordado, Estampa, Liso.")]
        [RegularExpression("^Bordado|bordado|Estampa|estampa|Liso|liso$", ErrorMessage = "O campo Layout deve receber somente as seguintes atribuições: Bordado, Estampa, Liso.")]
        public string Layout { get; set; }
    }
}
