using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PoolMS.Core.Migrations
{
    /// <inheritdoc />
    public partial class reservationchanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Pools_PoolId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_PoolId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "PoolId",
                table: "Reservations");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PoolId",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_PoolId",
                table: "Reservations",
                column: "PoolId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Pools_PoolId",
                table: "Reservations",
                column: "PoolId",
                principalTable: "Pools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
