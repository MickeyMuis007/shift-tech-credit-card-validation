using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CreditCardValidation.Persistence.Migrations
{
    public partial class AddedCreditCardStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CreditCardStatus",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    UserCreated = table.Column<string>(nullable: true),
                    LastUserUpdated = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    LastDateUpdated = table.Column<DateTime>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditCardStatus", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CreditCardStatus");
        }
    }
}
