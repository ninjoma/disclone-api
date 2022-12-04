using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace disclone_api.Migrations
{
    public partial class AddedMemberRoleRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoleLine_Member_MemberId",
                table: "RoleLine");

            migrationBuilder.DropForeignKey(
                name: "FK_RoleLine_Role_RoleId",
                table: "RoleLine");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoleLine",
                table: "RoleLine");

            migrationBuilder.RenameTable(
                name: "RoleLine",
                newName: "RoleLines");

            migrationBuilder.RenameIndex(
                name: "IX_RoleLine_RoleId",
                table: "RoleLines",
                newName: "IX_RoleLines_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_RoleLine_MemberId",
                table: "RoleLines",
                newName: "IX_RoleLines_MemberId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoleLines",
                table: "RoleLines",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RoleLines_Member_MemberId",
                table: "RoleLines",
                column: "MemberId",
                principalTable: "Member",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoleLines_Role_RoleId",
                table: "RoleLines",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoleLines_Member_MemberId",
                table: "RoleLines");

            migrationBuilder.DropForeignKey(
                name: "FK_RoleLines_Role_RoleId",
                table: "RoleLines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoleLines",
                table: "RoleLines");

            migrationBuilder.RenameTable(
                name: "RoleLines",
                newName: "RoleLine");

            migrationBuilder.RenameIndex(
                name: "IX_RoleLines_RoleId",
                table: "RoleLine",
                newName: "IX_RoleLine_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_RoleLines_MemberId",
                table: "RoleLine",
                newName: "IX_RoleLine_MemberId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoleLine",
                table: "RoleLine",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RoleLine_Member_MemberId",
                table: "RoleLine",
                column: "MemberId",
                principalTable: "Member",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoleLine_Role_RoleId",
                table: "RoleLine",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
