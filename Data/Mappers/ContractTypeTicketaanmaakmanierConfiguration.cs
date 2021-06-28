using _2021_dotnet_g_04.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2021_dotnet_g_04.Data.Mappers {
	internal class ContractTypeTicketaanmaakmanierConfiguration : IEntityTypeConfiguration<ContractTypeTicketAanmaakmanier> {
		public void Configure(EntityTypeBuilder<ContractTypeTicketAanmaakmanier> builder) {
			builder.ToTable("ContractType_TICKETAANMAAKMANIER");
			builder.HasNoKey();

			builder.Property(e => e.ContractTypeId)
				.HasColumnName("ContractType_ID");

			builder.Property(e => e.TicketAanmaakmanier)
				.HasColumnType("varchar(16)")
				.HasColumnName("TICKETAANMAAKMANIER");

			builder.HasOne(d => d.ContractType)
				.WithMany()
				.HasForeignKey(d => d.ContractTypeId)
				.HasConstraintName("CntrctTypTCKCntrctTypD");
		}
	}
}
