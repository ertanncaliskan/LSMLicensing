using Microsoft.EntityFrameworkCore.Migrations;

namespace LicenseSignatureService.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LicenseRegistries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CompanyName = table.Column<string>(nullable: true),
                    ContactPerson = table.Column<string>(nullable: true),
                    EMail = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    LicenseKey = table.Column<string>(nullable: true),
                    LicenseSecret = table.Column<string>(nullable: true),
                    LicenseSignature = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LicenseRegistries", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LicenseRegistries");
        }
    }
}
