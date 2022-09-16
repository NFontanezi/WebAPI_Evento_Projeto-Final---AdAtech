using System.ComponentModel.DataAnnotations;

namespace APIEvent.Core.Model
{
    public class EventReservation
    {

        [Key]
        public long IdReservation { get; set; }

        [Required(ErrorMessage = "Campo {0} Obrigatório")]
        public long IdEvent { get; set; }


        [Required(ErrorMessage = "Campo {0} Obrigatório",AllowEmptyStrings = false)]
        public string PersonName { get; set; }

        [Required(ErrorMessage = "Campo {0} Obrigatório")]
        public int Quantity { get; set; }




    }
   
}
