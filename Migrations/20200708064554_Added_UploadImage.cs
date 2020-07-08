using Microsoft.EntityFrameworkCore.Migrations;

namespace ResearchTube.Migrations
{
    public partial class Added_UploadImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UploadImage",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UploadImage",
                table: "AspNetUsers");
        }
    }
}
