using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkoutLogs.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class EndedPropSession : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Ended",
                table: "Sessions",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ended",
                table: "Sessions");
        }
    }
}
