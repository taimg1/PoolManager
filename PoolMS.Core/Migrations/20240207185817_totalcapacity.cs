using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PoolMS.Core.Migrations
{
    /// <inheritdoc />
    public partial class totalcapacity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentCapacity",
                table: "Pools");

            migrationBuilder.RenameColumn(
                name: "MaxCapacity",
                table: "Pools",
                newName: "TotalCapacity");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalCapacity",
                table: "Pools",
                newName: "MaxCapacity");

            migrationBuilder.AddColumn<int>(
                name: "CurrentCapacity",
                table: "Pools",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
