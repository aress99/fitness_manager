using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessManagerApi.Migrations
{
    /// <inheritdoc />
    public partial class AddHeightToMembers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Height",
                table: "Members",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Height",
                table: "Members");
        }
    }
}
