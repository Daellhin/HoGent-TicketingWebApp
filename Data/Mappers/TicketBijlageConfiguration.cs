using _2021_dotnet_g_04.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2021_dotnet_g_04.Data.Mappers {
	internal class TicketBijlageConfiguration : IEntityTypeConfiguration<TicketBijlage> {
		public void Configure(EntityTypeBuilder<TicketBijlage> builder) {
			builder.ToTable("Ticket_BIJLAGES");
			builder.HasKey(e => e.Id);

			builder.Property(e => e.Bijlage)
				.HasColumnName("BIJLAGES");

			builder.Property(e => e.TicketId).HasColumnName("Ticket_ID");
		}
	}
}
