using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace disclone_api.Migrations
{
    public partial class EntitiesAndRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ServerID",
                table: "Role",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Invitation",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RoleLine",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MemberId = table.Column<int>(type: "integer", nullable: false),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleLine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleLine_Member_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Member",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleLine_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Server",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OwnerID = table.Column<int>(type: "integer", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Server", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Server_User_OwnerID",
                        column: x => x.OwnerID,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Channel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ServerId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Channel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Channel_Server_ServerId",
                        column: x => x.ServerId,
                        principalTable: "Server",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    ChannelId = table.Column<int>(type: "integer", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Message_Channel_ChannelId",
                        column: x => x.ChannelId,
                        principalTable: "Channel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Message_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Role_ServerID",
                table: "Role",
                column: "ServerID");

            migrationBuilder.CreateIndex(
                name: "IX_Member_ServerId",
                table: "Member",
                column: "ServerId");

            migrationBuilder.CreateIndex(
                name: "IX_Member_UserId",
                table: "Member",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Invitation_ServerId",
                table: "Invitation",
                column: "ServerId");

            migrationBuilder.CreateIndex(
                name: "IX_Invitation_UserId",
                table: "Invitation",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Channel_ServerId",
                table: "Channel",
                column: "ServerId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_ChannelId",
                table: "Message",
                column: "ChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_UserId",
                table: "Message",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleLine_MemberId",
                table: "RoleLine",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleLine_RoleId",
                table: "RoleLine",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Server_OwnerID",
                table: "Server",
                column: "OwnerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Invitation_Server_ServerId",
                table: "Invitation",
                column: "ServerId",
                principalTable: "Server",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Invitation_User_UserId",
                table: "Invitation",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Member_Server_ServerId",
                table: "Member",
                column: "ServerId",
                principalTable: "Server",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Member_User_UserId",
                table: "Member",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Role_Server_ServerID",
                table: "Role",
                column: "ServerID",
                principalTable: "Server",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invitation_Server_ServerId",
                table: "Invitation");

            migrationBuilder.DropForeignKey(
                name: "FK_Invitation_User_UserId",
                table: "Invitation");

            migrationBuilder.DropForeignKey(
                name: "FK_Member_Server_ServerId",
                table: "Member");

            migrationBuilder.DropForeignKey(
                name: "FK_Member_User_UserId",
                table: "Member");

            migrationBuilder.DropForeignKey(
                name: "FK_Role_Server_ServerID",
                table: "Role");

            migrationBuilder.DropTable(
                name: "Message");

            migrationBuilder.DropTable(
                name: "RoleLine");

            migrationBuilder.DropTable(
                name: "Channel");

            migrationBuilder.DropTable(
                name: "Server");

            migrationBuilder.DropIndex(
                name: "IX_Role_ServerID",
                table: "Role");

            migrationBuilder.DropIndex(
                name: "IX_Member_ServerId",
                table: "Member");

            migrationBuilder.DropIndex(
                name: "IX_Member_UserId",
                table: "Member");

            migrationBuilder.DropIndex(
                name: "IX_Invitation_ServerId",
                table: "Invitation");

            migrationBuilder.DropIndex(
                name: "IX_Invitation_UserId",
                table: "Invitation");

            migrationBuilder.DropColumn(
                name: "ServerID",
                table: "Role");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Invitation");
        }
    }
}
