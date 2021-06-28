using _2021_dotnet_g_04.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2021_dotnet_g_04.Data.Mappers {
	internal class KlantTelefoonnummerConfiguration : IEntityTypeConfiguration<KlantTelefoonnummer> {
		public void Configure(EntityTypeBuilder<KlantTelefoonnummer> builder) {
			builder.ToTable("Klant_TELEFOONNUMMERS");
			builder.HasNoKey();

			builder.Property(e => e.KlantId)
				.HasColumnName("Klant_ID");

			builder.Property(e => e.Telefoonnummers)
				.HasColumnName("TELEFOONNUMMERS");

			builder.HasOne(d => d.Klant)
				.WithMany()
				.HasForeignKey(d => d.KlantId);
		}
	}
}
