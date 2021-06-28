
namespace _2021_dotnet_g_04.Models.Domain {
	public class TicketBijlage {
		public int TicketId { get; set; }
		public string Bijlage { get; set; }
        public int Id { get; set; }

		public TicketBijlage() {
		}

		public TicketBijlage(string bijlage) {
			Bijlage = bijlage;
		}
	}
}
