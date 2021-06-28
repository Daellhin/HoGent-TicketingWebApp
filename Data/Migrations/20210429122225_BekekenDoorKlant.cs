using Microsoft.EntityFrameworkCore.Migrations;

namespace _2021_dotnet_g_04.Data.Migrations {
	public partial class BekekenDoorKlant : Migration {
		protected override void Up(MigrationBuilder migrationBuilder) {

			migrationBuilder.AddColumn<bool>(
				name: "BekekenDoorKlant",
				table: "TICKET",
				nullable: false,
				defaultValue: false);
		}

		protected override void Down(MigrationBuilder migrationBuilder) {
			migrationBuilder.DropColumn(
				name: "BekekenDoorKlant",
				table: "TICKET");
		}
	}
}
