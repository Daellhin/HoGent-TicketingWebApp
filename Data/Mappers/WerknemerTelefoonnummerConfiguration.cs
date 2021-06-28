using _2021_dotnet_g_04.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2021_dotnet_g_04.Data.Mappers {
	internal class WerknemerTelefoonnummerConfiguration : IEntityTypeConfiguration<WerknemerTelefoonnummer> {
		public void Configure(EntityTypeBuilder<WerknemerTelefoonnummer> builder) {
			builder.ToTable("Werknemer_TELEFOONNUMMERS");
			builder.HasNoKey();

			builder.Property(e => e.Telefoonnummers)
				.HasColumnName("TELEFOONNUMMERS");

			builder.Property(e => e.WerknemerId)
				.HasColumnName("Werknemer_ID");

			builder.HasOne(d => d.Werknemer)
				.WithMany()
				.HasForeignKey(d => d.WerknemerId);
		}
	}
}
