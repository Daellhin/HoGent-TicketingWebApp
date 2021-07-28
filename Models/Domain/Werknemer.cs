using _2021_dotnet_g_04.Models.Domain.Enumerations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace _2021_dotnet_g_04.Models.Domain {
    public class Werknemer {
        public int Id { get; set; }
        public DateTime DatumInDienstTreding { get; set; }
        public Dienst Dienst { get; set; }
        public string Email { get; set; }
        public string Gebruikersnaam { get; set; }
        public string Naam { get; set; }
        public GebruikersStatus Status { get; set; }
        public string Voornaam { get; set; }
        public string Wachtwoord { get; set; }
        public WerknemersType Werknemerstype { get; set; }
        public string Busnr { get; set; }
        public int Huisnummer { get; set; }
        public string Land { get; set; }
        public string Postcode { get; set; }
        public string Straat { get; set; }
        public string Woonplaats { get; set; }

        public ICollection<Ticket> Tickets { get; set; }

        public Werknemer() {
            Tickets = new HashSet<Ticket>();
        }

        public void AddTicket(Ticket ticket) {
            Tickets.Add(ticket);
        }
    }
}
