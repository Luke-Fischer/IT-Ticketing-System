using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IT_Ticketing_System.Migrations
{
    /// <inheritdoc />
    public partial class addUserIdCol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserUniqueIdentfier",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserUniqueIdentfier",
                table: "Users");
        }
    }
}
