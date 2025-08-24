using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PeopleAPI.Migrations
{
    /// <inheritdoc />
    public partial class RenameJoinTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HobbyModelPersonModel_Hobbies_HobbyModelId",
                table: "HobbyModelPersonModel");

            migrationBuilder.DropForeignKey(
                name: "FK_HobbyModelPersonModel_People_PersonModelId",
                table: "HobbyModelPersonModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HobbyModelPersonModel",
                table: "HobbyModelPersonModel");

            migrationBuilder.RenameTable(
                name: "HobbyModelPersonModel",
                newName: "PersonHobbies");

            migrationBuilder.RenameIndex(
                name: "IX_HobbyModelPersonModel_PersonModelId",
                table: "PersonHobbies",
                newName: "IX_PersonHobbies_PersonModelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonHobbies",
                table: "PersonHobbies",
                columns: new[] { "HobbyModelId", "PersonModelId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PersonHobbies_Hobbies_HobbyModelId",
                table: "PersonHobbies",
                column: "HobbyModelId",
                principalTable: "Hobbies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_PersonHobbies_Hobbies_HobbyModelId",
                table: "PersonHobbies");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonHobbies_People_PersonModelId",
                table: "PersonHobbies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonHobbies",
                table: "PersonHobbies");

            migrationBuilder.RenameTable(
                name: "PersonHobbies",
                newName: "HobbyModelPersonModel");

            migrationBuilder.RenameIndex(
                name: "IX_PersonHobbies_PersonModelId",
                table: "HobbyModelPersonModel",
                newName: "IX_HobbyModelPersonModel_PersonModelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HobbyModelPersonModel",
                table: "HobbyModelPersonModel",
                columns: new[] { "HobbyModelId", "PersonModelId" });

            migrationBuilder.AddForeignKey(
                name: "FK_HobbyModelPersonModel_Hobbies_HobbyModelId",
                table: "HobbyModelPersonModel",
                column: "HobbyModelId",
                principalTable: "Hobbies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HobbyModelPersonModel_People_PersonModelId",
                table: "HobbyModelPersonModel",
                column: "PersonModelId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
