
using System.ComponentModel.DataAnnotations;

namespace APIEvent.Core.Model
{
    public class CityEvent
    {
        [Key]
        public long IdEvent { get; set; }

        [Required(ErrorMessage = "Titulo Obrigatório")]
        [MinLength(5, ErrorMessage = "Campo {0} deve conter até 300 caracteres")]
        [MaxLength(300, ErrorMessage = "Campo {0} deve conter até 5 caracteres")]
        public string Title { get; set; }


        [MaxLength(300, ErrorMessage = "Campo {0} deve conter até 300 caracteres")]
        public string Description { get; set; }


        [Required(ErrorMessage = "Campo {0} Obrigatório")]
        [DataType(DataType.DateTime)]
        public DateTime DateHourEvent { get; set; }

        [Required(ErrorMessage = "Campo {0} Obrigatório", AllowEmptyStrings = false)]
        [MaxLength(300, ErrorMessage = "Campo {0} deve conter até 100 caracteres")]
        public string Local { get; set; }

        [MaxLength(300, ErrorMessage = "Campo {0} deve conter até 100 caracteres")]
        public string Adress { get; set; }

        
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public decimal Price { get; set; }

        [Required]
        public bool Status { get; set; }



    }

}