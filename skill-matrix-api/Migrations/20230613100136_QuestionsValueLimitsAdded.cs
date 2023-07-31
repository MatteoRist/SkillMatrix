using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace skill_matrix_api.Migrations
{
    /// <inheritdoc />
    public partial class QuestionsValueLimitsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaxValue",
                table: "Questions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MinValue",
                table: "Questions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxValue",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "MinValue",
                table: "Questions");
        }
    }
}
