using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace disclone_api.Migrations
{
    public partial class EntitiesNamesUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Role_Server_ServerID",
                table: "Role");

            migrationBuilder.DropForeignKey(
                name: "FK_Server_User_OwnerID",
                table: "Server");

            migrationBuilder.DropIndex(
                name: "IX_Server_OwnerID",
                table: "Server");

            migrationBuilder.RenameColumn(
                name: "OwnerID",
                table: "Server",
                newName: "OwnerId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Server",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ServerID",
                table: "Role",
                newName: "ServerId");

            migrationBuilder.RenameIndex(
                name: "IX_Role_ServerID",
                table: "Role",
                newName: "IX_Role_ServerId");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Server",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "OwnerID",
                table: "Server",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Server_OwnerID",
                table: "Server",
                column: "OwnerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Role_Server_ServerId",
                table: "Role",
                column: "ServerId",
                principalTable: "Server",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Server_User_OwnerID",
                table: "Server",
                column: "OwnerID",
                principalTable: "User",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Role_Server_ServerId",
                table: "Role");

            migrationBuilder.DropForeignKey(
                name: "FK_Server_User_OwnerID",
                table: "Server");

            migrationBuilder.DropIndex(
                name: "IX_Server_OwnerID",
                table: "Server");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Server");

            migrationBuilder.DropColumn(
                name: "OwnerID",
                table: "Server");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "Server",
                newName: "OwnerID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Server",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "ServerId",
                table: "Role",
                newName: "ServerID");

            migrationBuilder.RenameIndex(
                name: "IX_Role_ServerId",
                table: "Role",
                newName: "IX_Role_ServerID");

            migrationBuilder.CreateIndex(
                name: "IX_Server_OwnerID",
                table: "Server",
                column: "OwnerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Role_Server_ServerID",
                table: "Role",
                column: "ServerID",
                principalTable: "Server",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Server_User_OwnerID",
                table: "Server",
                column: "OwnerID",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
