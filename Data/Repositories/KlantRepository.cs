using _2021_dotnet_g_04.Models.Domain;
using _2021_dotnet_g_04.Models.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace _2021_dotnet_g_04.Data.Repositories {
	public class KlantRepository : IKlantRepository {
		private readonly ApplicationDbContext _dbContext;
		private readonly DbSet<Klant> _klanten;

		public KlantRepository(ApplicationDbContext dbContext) {
			_dbContext = dbContext;
			_klanten = dbContext.Klanten;
		}
		public bool Exists(string gebruikersnaam, string wachtwoord) {
			return _klanten.Any(x => x.Gebruikersnaam == gebruikersnaam && x.Wachtwoord == wachtwoord);
		}

		public Klant GetBy(string gebruikersnaam, string wachtwoord) {
			return _klanten.Single(x => x.Gebruikersnaam == gebruikersnaam && x.Wachtwoord == wachtwoord);
		}

		public Klant GetKlantFromUsername(string gebruikersnaam) {
			return _klanten.Include(e => e.Contracten)
						.ThenInclude(e => e.Tickets)
						.ThenInclude(e => e.ToegewezenTechnieker)
						.Include(e => e.Contracten)
						.ThenInclude(e=>e.Tickets)
						.ThenInclude(e=>e.Bijlages)
						.Include(e => e.Contracten)
						.ThenInclude(e => e.ContractType)
						.Include(e => e.Contracten)
						.ThenInclude(e => e.Tickets)
						.ThenInclude(e => e.Comments)
						.Single(x => x.Gebruikersnaam == gebruikersnaam);
		}

		public void SaveChanges() {
			_dbContext.SaveChanges();
		}
	}
}
