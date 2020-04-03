﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PugnaFighting.Data.Migrations
{
    public partial class FighterSkillRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Grappling",
                table: "Fighters");

            migrationBuilder.DropColumn(
                name: "Health",
                table: "Fighters");

            migrationBuilder.DropColumn(
                name: "Stamina",
                table: "Fighters");

            migrationBuilder.DropColumn(
                name: "Strenght",
                table: "Fighters");

            migrationBuilder.DropColumn(
                name: "Striking",
                table: "Fighters");

            migrationBuilder.DropColumn(
                name: "Wrestling",
                table: "Fighters");

            migrationBuilder.AddColumn<int>(
                name: "SkillId",
                table: "Fighters",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Striking = table.Column<int>(nullable: false),
                    Grappling = table.Column<int>(nullable: false),
                    Wrestling = table.Column<int>(nullable: false),
                    Health = table.Column<int>(nullable: false),
                    Strenght = table.Column<int>(nullable: false),
                    Stamina = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fighters_SkillId",
                table: "Fighters",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_Skills_IsDeleted",
                table: "Skills",
                column: "IsDeleted");

            migrationBuilder.AddForeignKey(
                name: "FK_Fighters_Skills_SkillId",
                table: "Fighters",
                column: "SkillId",
                principalTable: "Skills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fighters_Skills_SkillId",
                table: "Fighters");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropIndex(
                name: "IX_Fighters_SkillId",
                table: "Fighters");

            migrationBuilder.DropColumn(
                name: "SkillId",
                table: "Fighters");

            migrationBuilder.AddColumn<int>(
                name: "Grappling",
                table: "Fighters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Health",
                table: "Fighters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Stamina",
                table: "Fighters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Strenght",
                table: "Fighters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Striking",
                table: "Fighters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Wrestling",
                table: "Fighters",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
