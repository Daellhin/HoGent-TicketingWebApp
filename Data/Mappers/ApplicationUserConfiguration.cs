using _2021_dotnet_g_04.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace _2021_dotnet_g_04.Data.Mappers {
	internal class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser> {
		public void Configure(EntityTypeBuilder<ApplicationUser> builder) {

			builder.HasOne(e => e.Klant);
			//builder.Property(e => e.Klant).hasRE
		}
	}
}
