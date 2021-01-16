using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Chat.DataAccess.Migrations
{
    public partial class updatedata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    PasswordSalt = table.Column<byte[]>(nullable: true),
                    PasswordHash = table.Column<byte[]>(nullable: true),
                    AccountType = table.Column<int>(nullable: false),
                    RefreshToken = table.Column<string>(nullable: true),
                    RefreshTokenExpiredDate = table.Column<DateTime>(nullable: false),
                    RoomId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accounts_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    AccountId = table.Column<int>(nullable: false),
                    RoomId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Messages_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "AccountType", "Email", "PasswordHash", "PasswordSalt", "RefreshToken", "RefreshTokenExpiredDate", "RoomId", "UserName" },
                values: new object[] { 1, 21, "lutfikotann@gmail.com", new byte[] { 183, 63, 97, 47, 77, 37, 46, 175, 179, 95, 221, 126, 159, 67, 129, 194, 93, 248, 195, 0, 56, 85, 64, 15, 119, 83, 14, 250, 164, 2, 193, 136, 21, 37, 144, 62, 252, 221, 62, 97, 7, 16, 38, 110, 61, 16, 160, 119, 190, 66, 18, 79, 101, 169, 72, 106, 114, 112, 162, 116, 187, 8, 178, 36 }, new byte[] { 239, 230, 135, 134, 67, 66, 151, 2, 193, 75, 219, 44, 236, 204, 122, 244, 139, 161, 129, 167, 196, 142, 7, 102, 248, 117, 229, 90, 201, 202, 23, 252, 180, 229, 152, 186, 89, 165, 8, 112, 55, 233, 158, 109, 17, 184, 130, 25, 107, 125, 158, 56, 180, 101, 133, 94, 244, 203, 89, 221, 10, 35, 232, 37, 213, 168, 61, 9, 79, 220, 38, 94, 52, 44, 17, 88, 48, 71, 247, 188, 98, 182, 206, 7, 72, 115, 125, 62, 200, 90, 0, 84, 138, 49, 126, 101, 54, 158, 193, 18, 26, 186, 113, 225, 48, 134, 50, 15, 157, 231, 148, 120, 206, 8, 182, 191, 112, 9, 81, 78, 198, 98, 32, 101, 16, 72, 96, 185 }, "IODEWTLD3TLK6YLYUH8AZKTAMTJXHMAWXGRPKKK", new DateTime(2021, 1, 13, 0, 32, 52, 936, DateTimeKind.Local).AddTicks(111), null, "lKotan" });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_RoomId",
                table: "Accounts",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_AccountId",
                table: "Messages",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_RoomId",
                table: "Messages",
                column: "RoomId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Rooms");
        }
    }
}
