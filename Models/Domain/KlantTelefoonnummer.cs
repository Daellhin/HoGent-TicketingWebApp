using Newtonsoft.Json;

namespace _2021_dotnet_g_04.Models.Domain {
    public class KlantTelefoonnummer {
        public int KlantId { get; set; }
        public string Telefoonnummers { get; set; }

        public Klant Klant { get; set; }
    }
}
