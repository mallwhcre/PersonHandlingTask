using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PeopleAPI.Migrations
{
    /// <inheritdoc />
    public partial class FixJoinTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonHobbies_Hobbies_HobbyModelId",
                table: "PersonHobbies");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonHobbies_People_PersonModelId",
                table: "PersonHobbies");

            migrationBuilder.DropTable(
                name: "HobbyModelPersonModel");

            migrationBuilder.RenameColumn(
                name: "PersonModelId",
                table: "PersonHobbies",
                newName: "PeopleId");

            migrationBuilder.RenameColumn(
                name: "HobbyModelId",
                table: "PersonHobbies",
                newName: "HobbiesId");

            migrationBuilder.RenameIndex(
                name: "IX_PersonHobbies_PersonModelId",
                table: "PersonHobbies",
                newName: "IX_PersonHobbies_PeopleId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonHobbies_Hobbies_HobbiesId",
                table: "PersonHobbies",
                column: "HobbiesId",
                principalTable: "Hobbies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonHobbies_People_PeopleId",
                table: "PersonHobbies",
                column: "PeopleId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonHobbies_Hobbies_HobbiesId",
                table: "PersonHobbies");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonHobbies_People_PeopleId",
                table: "PersonHobbies");

            migrationBuilder.RenameColumn(
                name: "PeopleId",
                table: "PersonHobbies",
                newName: "PersonModelId");

            migrationBuilder.RenameColumn(
                name: "HobbiesId",
                table: "PersonHobbies",
                newName: "HobbyModelId");

            migrationBuilder.RenameIndex(
                name: "IX_PersonHobbies_PeopleId",
                table: "PersonHobbies",
                newName: "IX_PersonHobbies_PersonModelId");

            migrationBuilder.CreateTable(
                name: "HobbyModelPersonModel",
                columns: table => new
                {
                    HobbiesId = table.Column<int>(type: "int", nullable: false),
                    PeopleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HobbyModelPersonModel", x => new { x.HobbiesId, x.PeopleId });
                    table.ForeignKey(
                        name: "FK_HobbyModelPersonModel_Hobbies_HobbiesId",
                        column: x => x.HobbiesId,
                        principalTable: "Hobbies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HobbyModelPersonModel_People_PeopleId",
                        column: x => x.PeopleId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_HobbyModelPersonModel_PeopleId",
                table: "HobbyModelPersonModel",
                column: "PeopleId");

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
    }
}
