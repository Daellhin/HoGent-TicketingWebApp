using Newtonsoft.Json;
using System;

namespace _2021_dotnet_g_04.Models.Domain {
	public class Comment {
		public int Id { get; set; }
		public string Opmerking { get; set; }
		public string PersoonDieOpmerkingToevoegt { get; set; }
		public DateTime Tijdstip { get; set; }
		public int TicketId { get; set; }

		public Ticket Ticket { get; set; }

        public Comment(){
		}

        public Comment(string opmerking, string persoonDieOpmerkingToevoegt){
			Opmerking = opmerking;
			PersoonDieOpmerkingToevoegt = persoonDieOpmerkingToevoegt;
			Tijdstip = DateTime.Now;
		}
	}
}
