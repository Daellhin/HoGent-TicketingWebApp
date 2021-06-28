using _2021_dotnet_g_04.Models.Domain;
using _2021_dotnet_g_04.Models.Domain.Enumerations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2021_dotnet_g_04.Data {
	public class DBInitializer {
		private readonly ApplicationDbContext _context;

		public DBInitializer(ApplicationDbContext context) {
			_context = context;
		}

		public void Run() {
			_context.Database.EnsureCreated();

			_context.SaveChanges();
		}

		public void RunMigratie() {
			_context.Database.Migrate();
		}

	}
}
