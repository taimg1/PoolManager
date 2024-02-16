using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PoolMS.Core.Migrations
{
    /// <inheritdoc />
    public partial class subpaymentchanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Days",
                table: "SubTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<double>(
                name: "Amount",
                table: "Payments",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Days",
                table: "SubTypes");

            migrationBuilder.AlterColumn<int>(
                name: "Amount",
                table: "Payments",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
