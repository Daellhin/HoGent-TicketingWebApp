using Newtonsoft.Json;

namespace _2021_dotnet_g_04.Models.Domain {
	public class Contactpersoon {
		public string Email { get; set; }
		public string Naam { get; set; }
		public string Voornaam { get; set; }
		public int KlantId { get; set; }

		public Klant Klant { get; set; }

	}
}
