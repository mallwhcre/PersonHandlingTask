﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PeopleAPI.Migrations
{
    /// <inheritdoc />
    public partial class MakeProfessionIdNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_People_Professions_ProfessionId",
                table: "People");

            migrationBuilder.AlterColumn<int>(
                name: "ProfessionId",
                table: "People",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_People_Professions_ProfessionId",
                table: "People",
                column: "ProfessionId",
                principalTable: "Professions",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_People_Professions_ProfessionId",
                table: "People");

            migrationBuilder.AlterColumn<int>(
                name: "ProfessionId",
                table: "People",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_People_Professions_ProfessionId",
                table: "People",
                column: "ProfessionId",
                principalTable: "Professions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
