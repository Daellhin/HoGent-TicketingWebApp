using _2021_dotnet_g_04.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2021_dotnet_g_04.Data.Mappers {
	internal class TicketConfiguration : IEntityTypeConfiguration<Ticket> {
		public void Configure(EntityTypeBuilder<Ticket> builder) {
			builder.ToTable("TICKET");
			builder.HasKey(e => e.Id);

			builder.Property(e => e.Id)
				.HasColumnName("ID");

			builder.Property(e => e.BekekenDoorTechnieker)
				.HasColumnName("BEKEKENDOORTECHNIEKER");

			builder.Property(e => e.ContractNummer)
				.HasColumnName("CONTRACT_NUMMER");

			builder.Property(e => e.DatumAanmaak)
				.HasColumnName("DATUMAANMAAK");

			builder.Property(e => e.DatumAfgehandeld)
				.HasColumnName("DATUMAFGEHANDELD");

			builder.Property(e => e.Dienst)
				.HasColumnName("DIENST");

			builder.Property(e => e.Omschrijving)
				.HasColumnName("OMSCHRIJVING");

			builder.Property(e => e.Status)
				.HasColumnName("STATUS");

			builder.Property(e => e.Titel)
				.HasColumnName("TITEL");

			builder.Property(e => e.ToegewezenTechniekerId)
				.HasColumnName("TOEGEWEZENTECHNIEKER_ID");

			builder.Property(e => e.Urgency)
				.HasColumnName("TYPE");

			builder.Property(e => e.BekekenDoorKlant);

			builder.HasOne(d => d.Contract)
				.WithMany(p => p.Tickets)
				.HasForeignKey(d => d.ContractNummer);

			builder.HasOne(d => d.ToegewezenTechnieker)
				.WithMany(p => p.Tickets)
				.HasForeignKey(d => d.ToegewezenTechniekerId);

			builder.HasMany(e => e.Bijlages)
				.WithOne();
		}
	}
}
