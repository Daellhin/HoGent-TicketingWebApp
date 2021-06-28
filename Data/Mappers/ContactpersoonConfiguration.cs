using _2021_dotnet_g_04.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2021_dotnet_g_04.Data.Mappers {
	internal class ContactpersoonConfiguration : IEntityTypeConfiguration<Contactpersoon> {
		public void Configure(EntityTypeBuilder<Contactpersoon> builder) {
			builder.ToTable("CONTACTPERSOON");
			builder.HasKey(e => e.Email);

			builder.Property(e => e.Email)
				.HasColumnName("EMAIL");

			builder.Property(e => e.KlantId)
				.HasColumnName("KLANT_ID");

			builder.Property(e => e.Naam)
				.HasColumnName("NAAM");

			builder.Property(e => e.Voornaam)
				.HasColumnName("VOORNAAM");

			builder.HasOne(d => d.Klant)
				.WithMany(p => p.Contactpersonen)
				.HasForeignKey(d => d.KlantId);
		}
	}
}
