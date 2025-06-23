using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ImageGallery.IDP.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Subject = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Password = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Active = table.Column<bool>(type: "INTEGER", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Type = table.Column<string>(type: "TEXT", maxLength: 250, nullable: false),
                    Value = table.Column<string>(type: "TEXT", maxLength: 250, nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Active", "ConcurrencyStamp", "Password", "Subject", "UserName" },
                values: new object[,]
                {
                    { new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"), true, "6dde9fff-81dd-430d-a49e-8f7ec96f4e9c", "password", "d860efca-22d9-47fd-8249-791ba61b07c7", "David" },
                    { new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"), true, "47660204-70ae-48c7-85e9-5832b8fe3e18", "password", "b7539694-97e7-4dfe-84da-b4256e1ff5c7", "Emma" }
                });

            migrationBuilder.InsertData(
                table: "UserClaims",
                columns: new[] { "Id", "ConcurrencyStamp", "Type", "UserId", "Value" },
                values: new object[,]
                {
                    { new Guid("085b59ce-e872-4c52-839d-5a6b36bfb51f"), "b5778a61-3257-456b-aec4-735b9c830a25", "family_name", new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"), "Flagg" },
                    { new Guid("42244513-0781-4625-9dc6-ffd42186b2d3"), "1c8185a1-3c47-4ac9-98fa-5dcd48ee9158", "country", new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"), "nl" },
                    { new Guid("5865a6ed-14fa-48b1-86fa-14ea0e27194c"), "459d426e-d8b1-4b6d-bd65-bcb587d3529a", "country", new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"), "be" },
                    { new Guid("69cf35a2-9663-4dac-85c7-b6f0bf647490"), "3fbe8a0b-5e10-4088-9bf1-7c4af4d3cd42", "role", new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"), "FreeUser" },
                    { new Guid("914b4a5e-a66e-46b7-8d34-5810524630bb"), "fe026125-56b4-417a-a626-15c0cb503c27", "given_name", new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"), "David" },
                    { new Guid("9b0bdd6c-9c76-4847-9545-f8c467782df5"), "fcd56b90-98eb-4bb4-ae7b-ba8033e8ef4b", "role", new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"), "PayingUser" },
                    { new Guid("9e7274b1-554f-4a21-9ee3-2b0d4cc951ba"), "cc5f2d2f-6bac-4b68-949d-d24a9d4fa5e4", "given_name", new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"), "Emma" },
                    { new Guid("f4d680e6-6432-4b02-bd28-b360cb9fb97a"), "93fcdddb-82f6-4c1c-9ac9-12ca0c2ae179", "family_name", new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"), "Flagg" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Subject",
                table: "Users",
                column: "Subject",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserName",
                table: "Users",
                column: "UserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
