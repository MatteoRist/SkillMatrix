using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace skill_matrix_api.Migrations
{
    /// <inheritdoc />
    public partial class addedConstraintsToRecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_UniqueRecord",
                table: "Records",
                columns: new[] { "UserId", "SkillId", "QuestionId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UniqueRecord",
                table: "Records");
        }
    }
}
