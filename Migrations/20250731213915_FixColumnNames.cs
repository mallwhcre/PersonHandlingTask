using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PeopleAPI.Migrations
{
    /// <inheritdoc />
    public partial class FixColumnNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonHobbies_People_PeopleId",
                table: "PersonHobbies");

            migrationBuilder.RenameColumn(
                name: "PeopleId",
                table: "PersonHobbies",
                newName: "PersonModelId");

            migrationBuilder.RenameIndex(
                name: "IX_PersonHobbies_PeopleId",
                table: "PersonHobbies",
                newName: "IX_PersonHobbies_PersonModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonHobbies_People_PersonModelId",
                table: "PersonHobbies",
                column: "PersonModelId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonHobbies_People_PersonModelId",
                table: "PersonHobbies");

            migrationBuilder.RenameColumn(
                name: "PersonModelId",
                table: "PersonHobbies",
                newName: "PeopleId");

            migrationBuilder.RenameIndex(
                name: "IX_PersonHobbies_PersonModelId",
                table: "PersonHobbies",
                newName: "IX_PersonHobbies_PeopleId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonHobbies_People_PeopleId",
                table: "PersonHobbies",
                column: "PeopleId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
