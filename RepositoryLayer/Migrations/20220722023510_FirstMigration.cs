using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserTable",
                columns: table => new
                {
                    UserID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTable", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "NotesTable",
                columns: table => new
                {
                    NoteID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Discription = table.Column<string>(nullable: true),
                    image = table.Column<string>(nullable: true),
                    color = table.Column<string>(nullable: true),
                    remainder = table.Column<DateTime>(nullable: false),
                    createdate = table.Column<DateTime>(nullable: false),
                    modifidedate = table.Column<DateTime>(nullable: false),
                    archieve = table.Column<bool>(nullable: false),
                    pin = table.Column<bool>(nullable: false),
                    trash = table.Column<bool>(nullable: false),
                    UserID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotesTable", x => x.NoteID);
                    table.ForeignKey(
                        name: "FK_NotesTable_UserTable_UserID",
                        column: x => x.UserID,
                        principalTable: "UserTable",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "CollabTable",
                columns: table => new
                {
                    CollabID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmailID = table.Column<string>(nullable: true),
                    UserID = table.Column<long>(nullable: false),
                    NoteID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollabTable", x => x.CollabID);
                    table.ForeignKey(
                        name: "FK_CollabTable_NotesTable_NoteID",
                        column: x => x.NoteID,
                        principalTable: "NotesTable",
                        principalColumn: "NoteID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_CollabTable_UserTable_UserID",
                        column: x => x.UserID,
                        principalTable: "UserTable",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CollabTable_NoteID",
                table: "CollabTable",
                column: "NoteID");

            migrationBuilder.CreateIndex(
                name: "IX_CollabTable_UserID",
                table: "CollabTable",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_NotesTable_UserID",
                table: "NotesTable",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CollabTable");

            migrationBuilder.DropTable(
                name: "NotesTable");

            migrationBuilder.DropTable(
                name: "UserTable");
        }
    }
}
