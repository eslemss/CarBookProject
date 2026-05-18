using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UdemyCarBook.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig_add_imageurl_fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AppUserId",
                table: "RentACarProcesses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "RentACarProcesses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            // ImageUrl KALSIN (Hata veren kolon buydu)
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "AppUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            // BURADAKİ UserName AddColumn BLOĞUNU SİLDİK

            migrationBuilder.CreateIndex(
                name: "IX_RentACarProcesses_AppUserId",
                table: "RentACarProcesses",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_RentACarProcesses_AppUsers_AppUserId",
                table: "RentACarProcesses",
                column: "AppUserId",
                principalTable: "AppUsers",
                principalColumn: "AppUserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentACarProcesses_AppUsers_AppUserId",
                table: "RentACarProcesses");

            migrationBuilder.DropIndex(
                name: "IX_RentACarProcesses_AppUserId",
                table: "RentACarProcesses");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "RentACarProcesses");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "RentACarProcesses");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "AppUsers");

       
        }
    }
}
