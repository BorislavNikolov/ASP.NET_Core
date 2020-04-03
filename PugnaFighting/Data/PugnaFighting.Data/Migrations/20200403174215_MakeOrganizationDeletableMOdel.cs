namespace PugnaFighting.Data.Migrations
{
    using System;

    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class MakeOrganizationDeletableMOdel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Organizations",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Organizations",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Organizations",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Organizations",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_IsDeleted",
                table: "Organizations",
                column: "IsDeleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Organizations_IsDeleted",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Organizations");
        }
    }
}
