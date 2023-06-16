using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace skill_matrix_api.Migrations
{
    /// <inheritdoc />
    public partial class FieldChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Records_Questions_QuestionId",
                table: "Records");

            migrationBuilder.DropForeignKey(
                name: "FK_Records_Skills_SkillId",
                table: "Records");

            migrationBuilder.DropForeignKey(
                name: "FK_Records_Users_UserId",
                table: "Records");

            migrationBuilder.DropIndex(
                name: "IX_Records_QuestionId",
                table: "Records");

            migrationBuilder.DropIndex(
                name: "IX_Records_SkillId",
                table: "Records");

            migrationBuilder.DropIndex(
                name: "IX_Records_UserId",
                table: "Records");

            migrationBuilder.RenameColumn(
                name: "value",
                table: "Records",
                newName: "Value");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Category",
                newName: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Value",
                table: "Records",
                newName: "value");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Category",
                newName: "name");

            migrationBuilder.CreateIndex(
                name: "IX_Records_QuestionId",
                table: "Records",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Records_SkillId",
                table: "Records",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_Records_UserId",
                table: "Records",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Records_Questions_QuestionId",
                table: "Records",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "QuestionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Records_Skills_SkillId",
                table: "Records",
                column: "SkillId",
                principalTable: "Skills",
                principalColumn: "SkillId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Records_Users_UserId",
                table: "Records",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
