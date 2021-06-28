using _2021_dotnet_g_04.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2021_dotnet_g_04.Data.Mappers {
	internal class ContractTypeConfiguration : IEntityTypeConfiguration<ContractType> {
		public void Configure(EntityTypeBuilder<ContractType> builder) {
			builder.ToTable("CONTRACTTYPE");
			builder.HasKey(e => e.Id);

			builder.Property(e => e.Id)
				.HasColumnName("ID");

			builder.Property(e => e.MinimaleDoorlooptijd)
				.HasColumnName("MINIMALEDOORLOOPTIJD");

			builder.Property(e => e.Naam)
				.HasColumnName("NAAM");

			builder.Property(e => e.Prijs)
				.HasColumnType("decimal(20, 4)")
				.HasColumnName("PRIJS");

			builder.Property(e => e.Status)
				.HasColumnName("STATUS");

			builder.Property(e => e.TicketAanmaaktijd)
				.HasColumnName("TICKETAANMAAKTIJD");

			builder.Property(e => e.TicketAfhandeltijd)
				.HasColumnName("TICKETAFHANDELTIJD");

			builder.Ignore(e => e.TicketAanmaakmanieren);
		}
	}
}
