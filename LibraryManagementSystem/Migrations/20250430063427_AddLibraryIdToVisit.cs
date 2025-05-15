using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagementSystem.Migrations
{
    public partial class AddLibraryIdToVisit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Library_Id",
                table: "Visits",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Visits_Library_Id",
                table: "Visits",
                column: "Library_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Visits_Library_Library_Id",
                table: "Visits",
                column: "Library_Id",
                principalTable: "Library",
                principalColumn: "LibraryID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Visits_Library_Library_Id",
                table: "Visits");

            migrationBuilder.DropIndex(
                name: "IX_Visits_Library_Id",
                table: "Visits");

            migrationBuilder.DropColumn(
                name: "Library_Id",
                table: "Visits");
        }
    }
}
