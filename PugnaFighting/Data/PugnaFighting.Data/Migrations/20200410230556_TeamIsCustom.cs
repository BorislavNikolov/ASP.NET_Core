namespace PugnaFighting.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class TeamIsCustom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCustom",
                table: "Managers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "HealthBonus",
                table: "Cutmen",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsCustom",
                table: "Cutmen",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsCustom",
                table: "Coaches",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "FightersCount",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCustom",
                table: "Managers");

            migrationBuilder.DropColumn(
                name: "HealthBonus",
                table: "Cutmen");

            migrationBuilder.DropColumn(
                name: "IsCustom",
                table: "Cutmen");

            migrationBuilder.DropColumn(
                name: "IsCustom",
                table: "Coaches");

            migrationBuilder.DropColumn(
                name: "FightersCount",
                table: "AspNetUsers");
        }
    }
}
