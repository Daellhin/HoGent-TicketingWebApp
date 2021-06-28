using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace _2021_dotnet_g_04.Data.Migrations
{
    public partial class bijlage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Ticket_BIJLAGES",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "TicketId1",
                table: "Ticket_BIJLAGES",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ticket_BIJLAGES",
                table: "Ticket_BIJLAGES",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_BIJLAGES_TicketId1",
                table: "Ticket_BIJLAGES",
                column: "TicketId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_BIJLAGES_TICKET_Ticket_ID",
                table: "Ticket_BIJLAGES",
                column: "Ticket_ID",
                principalTable: "TICKET",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_BIJLAGES_TICKET_TicketId1",
                table: "Ticket_BIJLAGES",
                column: "TicketId1",
                principalTable: "TICKET",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_BIJLAGES_TICKET_Ticket_ID",
                table: "Ticket_BIJLAGES");

            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_BIJLAGES_TICKET_TicketId1",
                table: "Ticket_BIJLAGES");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ticket_BIJLAGES",
                table: "Ticket_BIJLAGES");

            migrationBuilder.DropIndex(
                name: "IX_Ticket_BIJLAGES_TicketId1",
                table: "Ticket_BIJLAGES");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Ticket_BIJLAGES");

            migrationBuilder.DropColumn(
                name: "TicketId1",
                table: "Ticket_BIJLAGES");

            migrationBuilder.AlterColumn<string>(
                name: "TICKETAANMAAKMANIER",
                table: "ContractType_TICKETAANMAAKMANIER",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "TICKETAANMAAKTIJD",
                table: "CONTRACTTYPE",
                type: "nvarchar(255)",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.CreateTable(
                name: "AANMELDPOGING",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GEBRUIKERSNAAM = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GELUKT = table.Column<int>(type: "int", nullable: false),
                    TIMESTAMP = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AANMELDPOGING", x => x.ID);
                });

            migrationBuilder.AddForeignKey(
                name: "TicketBIJLAGESTicketID",
                table: "Ticket_BIJLAGES",
                column: "Ticket_ID",
                principalTable: "TICKET",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
