using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PoolMS.Core.Migrations
{
    /// <inheritdoc />
    public partial class visitchanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Visits_Reservations_ReservationId",
                table: "Visits");

            migrationBuilder.DropIndex(
                name: "IX_Visits_ReservationId",
                table: "Visits");

            migrationBuilder.DropColumn(
                name: "ReservationId",
                table: "Visits");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReservationId",
                table: "Visits",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Visits_ReservationId",
                table: "Visits",
                column: "ReservationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Visits_Reservations_ReservationId",
                table: "Visits",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "Id");
        }
    }
}
