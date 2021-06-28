using _2021_dotnet_g_04.Data.Mappers;
using _2021_dotnet_g_04.Models.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace _2021_dotnet_g_04.Data {
	public partial class ApplicationDbContext : IdentityDbContext {
		public ApplicationDbContext() {
		}

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options) {
		}

		public DbSet<ApplicationUser> ApplicationUsers { get; set; }
		public DbSet<Klant> Klanten { get; set; }
		public DbSet<Ticket> Ticketten { get; set; }
		public DbSet<Contract> Contracten { get; set; }
		public DbSet<ContractType> ContractTypes { get; set; }
		public DbSet<Werknemer> Werknemers { get; set; }
		public DbSet<ContractTypeTicketAanmaakmanier> ContractTypeTicketAanmaakManieren { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder) {
			base.OnModelCreating(modelBuilder);
			modelBuilder.ApplyConfiguration(new CommentConfiguration());
			modelBuilder.ApplyConfiguration(new ContactpersoonConfiguration());
			modelBuilder.ApplyConfiguration(new ContractConfiguration());
			modelBuilder.ApplyConfiguration(new ContractTypeConfiguration());
			modelBuilder.ApplyConfiguration(new ContractTypeTicketaanmaakmanierConfiguration());
			modelBuilder.ApplyConfiguration(new KlantConfiguration());
			modelBuilder.ApplyConfiguration(new KlantTelefoonnummerConfiguration());
			modelBuilder.ApplyConfiguration(new TicketBijlageConfiguration());
			modelBuilder.ApplyConfiguration(new TicketConfiguration());
			modelBuilder.ApplyConfiguration(new WerknemerConfiguration());
			modelBuilder.ApplyConfiguration(new WerknemerTelefoonnummerConfiguration());
			modelBuilder.ApplyConfiguration(new ApplicationUserConfiguration());
		}
	}
}