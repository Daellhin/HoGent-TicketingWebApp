using _2021_dotnet_g_04.Data;
using _2021_dotnet_g_04.Models.Domain.Enumerations;
using System.Collections.Generic;

namespace _2021_dotnet_g_04.Models.Domain.Repositories {
	public interface ITicketRepository {
		IEnumerable<Ticket> GetAll();
		IEnumerable<Ticket> GetAllFromCustomer(Klant klant);
		Ticket GetBy(int ticketId);
		void Add(Ticket ticket);
		void SaveChanges();
	}
}
