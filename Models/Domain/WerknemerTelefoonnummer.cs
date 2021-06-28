using Newtonsoft.Json;

namespace _2021_dotnet_g_04.Models.Domain {
    public class WerknemerTelefoonnummer {
        public int WerknemerId { get; set; }
        public string Telefoonnummers { get; set; }

        public Werknemer Werknemer { get; set; }
    }
}
