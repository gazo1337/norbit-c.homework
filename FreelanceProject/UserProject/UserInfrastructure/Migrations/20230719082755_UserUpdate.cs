using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UserUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ComplTasks",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string[]>(
                name: "ComplTasks",
                table: "Users",
                type: "text[]",
                nullable: false,
                defaultValue: new string[0]);
        }
    }
}
