using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace APIEvent.Core.Model
{
    public class CityEvent
    {
        [Key]
        public long IdEvent { get; set; }

        [Required(ErrorMessage = "Campo {0} Obrigatório", AllowEmptyStrings = false)]
        public string Title { get; set; }


        [MaxLength(300, ErrorMessage = "Campo {0} deve conter até 300 caracteres")]
        public string Description { get; set; }


        [Required(ErrorMessage = "Campo {0} Obrigatório")]
        [DataType(DataType.DateTime)]
        public DateTime DateHourEvent { get; set; }

        [Required(ErrorMessage = "Campo {0} Obrigatório", AllowEmptyStrings = false)]
        public string Local { get; set; }

        [Display(Name = "Endereço")]
        public string Adress { get; set; }

        [Display(Name = "Preço Unitário")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public decimal Price { get; set; }

        [Required]
        public bool Status { get; set; }



    }

}