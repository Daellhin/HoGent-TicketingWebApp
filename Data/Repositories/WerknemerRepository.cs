using _2021_dotnet_g_04.Models.Domain;
using _2021_dotnet_g_04.Models.Domain.Enumerations;
using _2021_dotnet_g_04.Models.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2021_dotnet_g_04.Data.Repositories {
	public class WerknemerRepository : IWerknemerRepository {

		private readonly ApplicationDbContext _dbContext;
		private readonly DbSet<Werknemer> _werknemers;

		public WerknemerRepository(ApplicationDbContext dbContext) {
			_dbContext = dbContext;
			_werknemers = dbContext.Werknemers;
		}

		public Werknemer GetRandomTechnician() {
			return _werknemers.Where(e => e.Werknemerstype == WerknemersType.Technician)
						.OrderBy(e => Guid.NewGuid())
						.First();
		}
	}
}
