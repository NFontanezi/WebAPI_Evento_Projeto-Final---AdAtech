

using System.ComponentModel.DataAnnotations;

namespace APIEvent.Core.Model
{
    public class EventReservation
    {
        [Editable(false)]
        [ScaffoldColumn(false)]
        [Key]
        public long IdReservation { get; set; }

        [Required(ErrorMessage = "Campo {0} Obrigatório")]
        [Display(Name = "Código do Evento")]
        public long IdEvent { get; set; }

        public CityEvent Event { get; set; }

        [Required(ErrorMessage = "Campo {0} Obrigatório",AllowEmptyStrings = false)]
        [Display(Name = "Nome")]
        public string PersonName { get; set; }

        [Required(ErrorMessage = "Campo {0} Obrigatório")]
        [Display(Name = "Quantidade")]
        public int Quantity { get; set; }

        public EventReservation()
        {
            
        }

        public static List<EventReservation> ReservationList { get; set; } = new();

        public static void AddReservation(EventReservation events)
        {
            ReservationList.Add(events);
        }

    }
   
}
