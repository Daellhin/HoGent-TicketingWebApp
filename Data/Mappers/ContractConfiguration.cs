using _2021_dotnet_g_04.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2021_dotnet_g_04.Data.Mappers {
	internal class ContractConfiguration : IEntityTypeConfiguration<Contract> {
		public void Configure(EntityTypeBuilder<Contract> builder) {
			builder.ToTable("CONTRACT");
			builder.HasKey(e => e.Nummer);

			builder.Property(e => e.Nummer)
				.HasColumnName("NUMMER");

			builder.Property(e => e.ContracttypeId)
				.HasColumnName("CONTRACTTYPE_ID");

			builder.Property(e => e.Einddatum)
				.HasColumnName("EINDDATUM");

			builder.Property(e => e.KlantId)
				.HasColumnName("KLANT_ID");

			builder.Property(e => e.Startdatum)
				.HasColumnName("STARTDATUM");

			builder.Property(e => e.Status)
				.HasColumnName("STATUS");

			builder.HasOne(d => d.ContractType)
				.WithMany(p => p.Contracts)
				.HasForeignKey(d => d.ContracttypeId);

			builder.HasOne(d => d.Klant)
				.WithMany(p => p.Contracten)
				.HasForeignKey(d => d.KlantId);
		}
	}
}
