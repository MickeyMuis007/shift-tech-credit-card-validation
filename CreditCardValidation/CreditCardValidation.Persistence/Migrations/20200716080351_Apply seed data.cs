using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CreditCardValidation.Persistence.Migrations
{
    public partial class Applyseeddata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CreditCards_CreditCardProvider_CreditCardProviderId",
                table: "CreditCards");

            migrationBuilder.DropForeignKey(
                name: "FK_CreditCards_CreditCardStatus_CreditCardStatusId",
                table: "CreditCards");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CreditCards",
                table: "CreditCards");

            migrationBuilder.RenameTable(
                name: "CreditCards",
                newName: "CreditCard");

            migrationBuilder.RenameIndex(
                name: "IX_CreditCards_CreditCardStatusId",
                table: "CreditCard",
                newName: "IX_CreditCard_CreditCardStatusId");

            migrationBuilder.RenameIndex(
                name: "IX_CreditCards_CreditCardProviderId",
                table: "CreditCard",
                newName: "IX_CreditCard_CreditCardProviderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CreditCard",
                table: "CreditCard",
                column: "Id");

            migrationBuilder.InsertData(
                table: "CreditCardProvider",
                columns: new[] { "Id", "Active", "Code", "DateCreated", "LastDateUpdated", "LastUserUpdated", "Length", "Name", "StartsWith", "UserCreated" },
                values: new object[,]
                {
                    { new Guid("969cfded-d569-402b-8237-d3a6ac5c3eb3"), false, "AMEX", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "15", "American Express", "34,37", null },
                    { new Guid("a7370c45-9429-4757-a45d-8fa1a2964474"), false, "VISA", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "13,16,19", "VISA", "4", null },
                    { new Guid("2216cb5d-5acf-4d84-9741-43031d705acd"), false, "MasterCard", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "16", "MasterCard", "51,52,53,54,55,222100-272099", null },
                    { new Guid("367d2e2c-95df-476d-92a8-2e7edc7e8e45"), false, "Discover", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "16,19", "Discover", "6011,622126-622925,644,645,646,647,648,649,65", null }
                });

            migrationBuilder.InsertData(
                table: "CreditCardStatus",
                columns: new[] { "Id", "Active", "DateCreated", "Description", "LastDateUpdated", "LastUserUpdated", "Status", "UserCreated" },
                values: new object[,]
                {
                    { new Guid("30eaec92-2531-4634-b14e-3492a578edb9"), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Valid approved credit cards, that's required to be processed.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Issued", null },
                    { new Guid("ec85b303-efa1-4bac-b2f5-8aa927f87df2"), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Credit cards that has been processed.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Processed", null }
                });

            migrationBuilder.InsertData(
                table: "CreditCard",
                columns: new[] { "Id", "Active", "CreditCardProviderId", "CreditCardStatusId", "DateCreated", "LastDateUpdated", "LastUserUpdated", "No", "UserCreated" },
                values: new object[,]
                {
                    { new Guid("d843d16c-79fa-477f-a08a-ed19ab3d96f1"), false, new Guid("969cfded-d569-402b-8237-d3a6ac5c3eb3"), new Guid("30eaec92-2531-4634-b14e-3492a578edb9"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "374245455400126", null },
                    { new Guid("0fcfd9be-fceb-4b48-810c-6b74003ab757"), false, new Guid("367d2e2c-95df-476d-92a8-2e7edc7e8e45"), new Guid("30eaec92-2531-4634-b14e-3492a578edb9"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "60115564485789458", null },
                    { new Guid("c18a7a12-aa81-4fe4-b0e9-55cc9b695a7c"), false, new Guid("2216cb5d-5acf-4d84-9741-43031d705acd"), new Guid("30eaec92-2531-4634-b14e-3492a578edb9"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "5425233430109903", null },
                    { new Guid("aa360156-4e11-44ea-b0ad-cc8376874c75"), false, new Guid("a7370c45-9429-4757-a45d-8fa1a2964474"), new Guid("30eaec92-2531-4634-b14e-3492a578edb9"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "4263982640269299", null }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_CreditCard_CreditCardProvider_CreditCardProviderId",
                table: "CreditCard",
                column: "CreditCardProviderId",
                principalTable: "CreditCardProvider",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CreditCard_CreditCardStatus_CreditCardStatusId",
                table: "CreditCard",
                column: "CreditCardStatusId",
                principalTable: "CreditCardStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CreditCard_CreditCardProvider_CreditCardProviderId",
                table: "CreditCard");

            migrationBuilder.DropForeignKey(
                name: "FK_CreditCard_CreditCardStatus_CreditCardStatusId",
                table: "CreditCard");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CreditCard",
                table: "CreditCard");

            migrationBuilder.DeleteData(
                table: "CreditCard",
                keyColumn: "Id",
                keyValue: new Guid("0fcfd9be-fceb-4b48-810c-6b74003ab757"));

            migrationBuilder.DeleteData(
                table: "CreditCard",
                keyColumn: "Id",
                keyValue: new Guid("aa360156-4e11-44ea-b0ad-cc8376874c75"));

            migrationBuilder.DeleteData(
                table: "CreditCard",
                keyColumn: "Id",
                keyValue: new Guid("c18a7a12-aa81-4fe4-b0e9-55cc9b695a7c"));

            migrationBuilder.DeleteData(
                table: "CreditCard",
                keyColumn: "Id",
                keyValue: new Guid("d843d16c-79fa-477f-a08a-ed19ab3d96f1"));

            migrationBuilder.DeleteData(
                table: "CreditCardStatus",
                keyColumn: "Id",
                keyValue: new Guid("ec85b303-efa1-4bac-b2f5-8aa927f87df2"));

            migrationBuilder.DeleteData(
                table: "CreditCardProvider",
                keyColumn: "Id",
                keyValue: new Guid("2216cb5d-5acf-4d84-9741-43031d705acd"));

            migrationBuilder.DeleteData(
                table: "CreditCardProvider",
                keyColumn: "Id",
                keyValue: new Guid("367d2e2c-95df-476d-92a8-2e7edc7e8e45"));

            migrationBuilder.DeleteData(
                table: "CreditCardProvider",
                keyColumn: "Id",
                keyValue: new Guid("969cfded-d569-402b-8237-d3a6ac5c3eb3"));

            migrationBuilder.DeleteData(
                table: "CreditCardProvider",
                keyColumn: "Id",
                keyValue: new Guid("a7370c45-9429-4757-a45d-8fa1a2964474"));

            migrationBuilder.DeleteData(
                table: "CreditCardStatus",
                keyColumn: "Id",
                keyValue: new Guid("30eaec92-2531-4634-b14e-3492a578edb9"));

            migrationBuilder.RenameTable(
                name: "CreditCard",
                newName: "CreditCards");

            migrationBuilder.RenameIndex(
                name: "IX_CreditCard_CreditCardStatusId",
                table: "CreditCards",
                newName: "IX_CreditCards_CreditCardStatusId");

            migrationBuilder.RenameIndex(
                name: "IX_CreditCard_CreditCardProviderId",
                table: "CreditCards",
                newName: "IX_CreditCards_CreditCardProviderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CreditCards",
                table: "CreditCards",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CreditCards_CreditCardProvider_CreditCardProviderId",
                table: "CreditCards",
                column: "CreditCardProviderId",
                principalTable: "CreditCardProvider",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CreditCards_CreditCardStatus_CreditCardStatusId",
                table: "CreditCards",
                column: "CreditCardStatusId",
                principalTable: "CreditCardStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
