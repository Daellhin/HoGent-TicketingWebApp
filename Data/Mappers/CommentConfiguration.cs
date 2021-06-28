using _2021_dotnet_g_04.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2021_dotnet_g_04.Data.Mappers {
	internal class CommentConfiguration : IEntityTypeConfiguration<Comment> {
		public void Configure(EntityTypeBuilder<Comment> builder) {
			builder.ToTable("COMMENT");
			builder.HasKey(e => e.Id);

			builder.Property(e => e.Id)
				.HasColumnName("ID");

			builder.Property(e => e.Opmerking)
				.HasColumnName("OPMERKING");

			builder.Property(e => e.PersoonDieOpmerkingToevoegt)
				.HasColumnName("PERSOONDIEOPMERKINGTOEVOEGT");

			builder.Property(e => e.TicketId)
				.HasColumnName("TICKET_ID");

			builder.Property(e => e.Tijdstip)
				.HasColumnName("TIJDSTIP");

			builder.HasOne(d => d.Ticket)
				.WithMany(p => p.Comments)
				.HasForeignKey(d => d.TicketId);
		}
	}
}
