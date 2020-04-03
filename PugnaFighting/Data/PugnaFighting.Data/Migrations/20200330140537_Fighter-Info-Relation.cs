﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PugnaFighting.Data.Migrations
{
    public partial class FighterInfoRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "Fighters");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "Fighters");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Fighters");

            migrationBuilder.DropColumn(
                name: "Nationality",
                table: "Fighters");

            migrationBuilder.DropColumn(
                name: "PictureUrl",
                table: "Fighters");

            migrationBuilder.AddColumn<int>(
                name: "PersonalInfoId",
                table: "Fighters",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PersonalInfo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    Nickname = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    Category = table.Column<int>(nullable: false),
                    Nationality = table.Column<string>(maxLength: 30, nullable: false),
                    Age = table.Column<int>(nullable: false),
                    PictureUrl = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalInfo", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fighters_PersonalInfoId",
                table: "Fighters",
                column: "PersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalInfo_IsDeleted",
                table: "PersonalInfo",
                column: "IsDeleted");

            migrationBuilder.AddForeignKey(
                name: "FK_Fighters_PersonalInfo_PersonalInfoId",
                table: "Fighters",
                column: "PersonalInfoId",
                principalTable: "PersonalInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fighters_PersonalInfo_PersonalInfoId",
                table: "Fighters");

            migrationBuilder.DropTable(
                name: "PersonalInfo");

            migrationBuilder.DropIndex(
                name: "IX_Fighters_PersonalInfoId",
                table: "Fighters");

            migrationBuilder.DropColumn(
                name: "PersonalInfoId",
                table: "Fighters");

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Fighters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Category",
                table: "Fighters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Fighters",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Nationality",
                table: "Fighters",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PictureUrl",
                table: "Fighters",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
