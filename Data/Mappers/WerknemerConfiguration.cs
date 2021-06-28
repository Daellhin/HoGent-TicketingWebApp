using _2021_dotnet_g_04.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2021_dotnet_g_04.Data.Mappers {
	internal class WerknemerConfiguration : IEntityTypeConfiguration<Werknemer> {
		public void Configure(EntityTypeBuilder<Werknemer> builder) {
			builder.ToTable("WERKNEMER");
			builder.HasKey(e => e.Id);

			builder.Property(e => e.Id)
				.HasColumnName("ID");

			builder.Property(e => e.Busnr)
				.HasColumnName("BUSNR");

			builder.Property(e => e.DatumInDienstTreding)
				.HasColumnName("DATUMINDIENSTTREDING");

			builder.Property(e => e.Dienst)
				.HasColumnName("DIENST");

			builder.Property(e => e.Email)
				.HasColumnName("EMAIL");

			builder.Property(e => e.Gebruikersnaam)
				.HasColumnName("GEBRUIKERSNAAM");

			builder.Property(e => e.Huisnummer)
				.HasColumnName("HUISNUMMER");

			builder.Property(e => e.Land)
				.IsRequired()
				.HasColumnName("LAND");

			builder.Property(e => e.Naam)
				.HasColumnName("NAAM");

			builder.Property(e => e.Postcode)
				.IsRequired()
				.HasColumnName("POSTCODE");

			builder.Property(e => e.Status)
				.HasColumnName("STATUS");

			builder.Property(e => e.Straat)
				.IsRequired()
				.HasColumnName("STRAAT");

			builder.Property(e => e.Voornaam)
				.HasColumnName("VOORNAAM");

			builder.Property(e => e.Wachtwoord)
				.HasColumnName("WACHTWOORD");

			builder.Property(e => e.Werknemerstype)
				.HasColumnName("WERKNEMERSTYPE");

			builder.Property(e => e.Woonplaats)
				.IsRequired()
				.HasColumnName("WOONPLAATS");
		}
	}
}
