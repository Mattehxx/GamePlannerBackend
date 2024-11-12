using Microsoft.Extensions.Hosting;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Runtime.ConstrainedExecution;

namespace GamePlanner.DTO.OutputDTO
{
    public class EventDetailsDTO
    {
        public int EventId { get; set; }
        public DateTime EventDate { get; set; }
        public required string EventName { get; set; }
        public required string EventDescription { get; set; } 
        public int AvailableTables { get { return GameSessionsDetails != null ? GameSessionsDetails.Count() : 0; } }
        public bool IsPublic { get; set; }
        //DETTAGLI TAVOLI
        public List<GameSessionDetailsDTO>? GameSessionsDetails { get; set; }

        //-data
        //-Nome
        //-descrizione 
        //-Numero di posti disponibili  **calcolare
        //-IsPublic** Iconografia per capire se l’evento è prenotabile oppure no(è possibile entrare in lista di attesa)


        //DETTAGLIO EVENTO
        //tavoli a disposizione con info:
        /*
        nome del Master (se presente) 
        o Numero di posti a disposizione 
        o Numero di posti totali 
        o Stato (completo, prenotabile) 
        */
        //stato della coda
        /*
        numero persone in coda
        */
    }
}
