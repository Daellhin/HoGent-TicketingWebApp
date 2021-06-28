using _2021_dotnet_g_04.Models.Domain;
using _2021_dotnet_g_04.Models.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace _2021_dotnet_g_04.Data.Repositories {
	public class TicketRepository : ITicketRepository {

		private readonly ApplicationDbContext _dbContext;
		private readonly DbSet<Ticket> _tickets;

		public TicketRepository(ApplicationDbContext dbContext) {
			_dbContext = dbContext;
			_tickets = dbContext.Ticketten;
		}

		public void Add(Ticket ticket) {
			_tickets.Add(ticket);
		}

		public IEnumerable<Ticket> GetAllFromCustomer(Klant klant) {
			return _tickets.Where(t => t.Contract.Klant.Equals(klant)).ToList();
		}

		public Ticket GetBy(int ticketId) {
			return _tickets
				.Include(ticket => ticket.Comments)
				.Include(ticket => ticket.Bijlages)
                .SingleOrDefault(ticket => ticket.Id.Equals(ticketId));
		}

		public void SaveChanges() {
			_dbContext.SaveChanges();
		}

		public IEnumerable<Ticket> GetAll() {
			return _tickets.ToList();
		}
	}

}
