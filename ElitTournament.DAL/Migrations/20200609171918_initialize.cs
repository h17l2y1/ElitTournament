using Microsoft.EntityFrameworkCore.Migrations;

namespace ElitTournament.DAL.Migrations
{
    public partial class initialize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ClientId = table.Column<int>(nullable: false),
                    IsViber = table.Column<bool>(nullable: false),
                    IsTelegram = table.Column<bool>(nullable: false),
                    PrimaryDeviceOS = table.Column<string>(nullable: true),
                    ViberVersion = table.Column<string>(nullable: true),
                    Mcc = table.Column<int>(nullable: false),
                    Mnc = table.Column<int>(nullable: false),
                    DeviceType = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    Language = table.Column<string>(nullable: true),
                    ApiVersion = table.Column<double>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Avatar = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
