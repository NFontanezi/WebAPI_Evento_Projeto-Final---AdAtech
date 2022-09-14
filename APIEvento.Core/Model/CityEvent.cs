using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace APIEvent.Core.Model
{
    public class CityEvent
    {
        [Editable(false)]
        [ScaffoldColumn(true)]
        [Key]
        
        public long IdEvent { get; set; }

        [Required(ErrorMessage = "Campo {0} Obrigatório", AllowEmptyStrings = false)]
        [Display(Name ="Título")]
        public string Title { get; set; }


        [Display(Name = "Descrição")]
        [MaxLength(300, ErrorMessage = "Campo {0} deve conter até 300 caracteres")]
        public string Description { get; set; }


        [Required(ErrorMessage = "Campo {0} Obrigatório")]
        [DataType(DataType.DateTime)]
       // [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy HH:mm}")]
        [Display(Name = "Data/Hora Evento")]
        public DateTime DateHourEvent { get; set; }

        [Required(ErrorMessage = "Campo {0} Obrigatório", AllowEmptyStrings = false)]
        [DisplayName("Local Evento")]
        public string Local { get; set; }

        [Display(Name = "Endereço")]
        public string Adress { get; set; }

        [Display(Name = "Preço Unitário")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "Status")]
        public bool Status { get; set; }

        public CityEvent()
        {

        }

        public static List<CityEvent> EventsList { get; set; } = new();

        public static void AddEvent(CityEvent events)
        {
            EventsList.Add(events);
        }
    }

}