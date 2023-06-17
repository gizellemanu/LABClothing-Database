using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace labclothingcollection.DTO.ModelosDTO.Request
{
    public class ModelosRequestDTO
    {
        [Key]
        public int IdColecaoRelacionada { get; set; }

        [Required(ErrorMessage = "O campo IdModelo é de preenchimento obrigatório")]
        public int IdModelo { get; set; }

        [Required(ErrorMessage = "O campo Nome do Modelo é de preenchimento obrigatório")]
        [MaxLength(80, ErrorMessage = "O campo Nome do Modelo não pode exceder 80 caracteres")]
        public string NomeModelo { get; set; }

        [Required(ErrorMessage = "O campo Tipo é de preenchimento obrigatório. O campo Tipo deve receber somente as seguintes atribuições: Bermuda, Biquini, Bolsa, Bone, Calça, Calçados, Camisa, Chapéu, Saia.")]
        [MaxLength(80, ErrorMessage = "O campo Tipo do Modelo não pode exceder 80 caracteres")]
        [RegularExpression("^(Bermuda|bermuda|Biquini|biquini|Bolsa|bolsa|Bone|bone|Calça|calça|Calçados|calçados|Camisa|camisa|Chapéu|chapeu|Saia)$", ErrorMessage = "O campo Tipo deve receber somente as seguintes atribuições: Bermuda, Biquini, Bolsa, Bone, Calça, Calçados, Camisa, Chapéu, Saia.")]
        public string Tipo { get; set; }

        [Required(ErrorMessage = "O campo Layout é de preenchimento obrigatório. O campo Layout deve receber somente as seguintes atribuições: Bordado, Estampa, Liso. ")]
        [MaxLength(15, ErrorMessage = "O campo Layout do Modelo não pode exceder 80 caracteres")]
        [RegularExpression("^(Bordado|bordado|Estampa|estampa|Liso|liso)$", ErrorMessage = "O campo Layout deve receber somente as seguintes atribuições: Bordado, Estampa, Liso.")]
        public string Layout { get; set; }
    }
}