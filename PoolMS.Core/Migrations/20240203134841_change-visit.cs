using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PoolMS.Core.Migrations
{
    /// <inheritdoc />
    public partial class changevisit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Visits_Reservations_ReservationId",
                table: "Visits");

            migrationBuilder.AlterColumn<int>(
                name: "ReservationId",
                table: "Visits",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Visits_Reservations_ReservationId",
                table: "Visits",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Visits_Reservations_ReservationId",
                table: "Visits");

            migrationBuilder.AlterColumn<int>(
                name: "ReservationId",
                table: "Visits",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Visits_Reservations_ReservationId",
                table: "Visits",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
