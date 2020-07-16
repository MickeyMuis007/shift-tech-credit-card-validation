using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CreditCardValidation.Persistence.Migrations
{
    public partial class AddedCreditCard : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CreditCards",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    UserCreated = table.Column<string>(nullable: true),
                    LastUserUpdated = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    LastDateUpdated = table.Column<DateTime>(nullable: false),
                    No = table.Column<string>(nullable: true),
                    CreditCardStatusId = table.Column<Guid>(nullable: false),
                    CreditCardProviderId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CreditCards_CreditCardProvider_CreditCardProviderId",
                        column: x => x.CreditCardProviderId,
                        principalTable: "CreditCardProvider",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CreditCards_CreditCardStatus_CreditCardStatusId",
                        column: x => x.CreditCardStatusId,
                        principalTable: "CreditCardStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CreditCards_CreditCardProviderId",
                table: "CreditCards",
                column: "CreditCardProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditCards_CreditCardStatusId",
                table: "CreditCards",
                column: "CreditCardStatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CreditCards");
        }
    }
}
