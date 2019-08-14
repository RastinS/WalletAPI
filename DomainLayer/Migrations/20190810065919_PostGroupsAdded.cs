using Microsoft.EntityFrameworkCore.Migrations;

namespace DomainLayer.Migrations
{
    public partial class PostGroupsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostGroup_PostGroup_ParentId",
                table: "PostGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_PostGroup_PostGroupId",
                table: "Posts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostGroup",
                table: "PostGroup");

            migrationBuilder.RenameTable(
                name: "PostGroup",
                newName: "PostGroups");

            migrationBuilder.RenameIndex(
                name: "IX_PostGroup_ParentId",
                table: "PostGroups",
                newName: "IX_PostGroups_ParentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostGroups",
                table: "PostGroups",
                column: "PostGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostGroups_PostGroups_ParentId",
                table: "PostGroups",
                column: "ParentId",
                principalTable: "PostGroups",
                principalColumn: "PostGroupId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_PostGroups_PostGroupId",
                table: "Posts",
                column: "PostGroupId",
                principalTable: "PostGroups",
                principalColumn: "PostGroupId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostGroups_PostGroups_ParentId",
                table: "PostGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_PostGroups_PostGroupId",
                table: "Posts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostGroups",
                table: "PostGroups");

            migrationBuilder.RenameTable(
                name: "PostGroups",
                newName: "PostGroup");

            migrationBuilder.RenameIndex(
                name: "IX_PostGroups_ParentId",
                table: "PostGroup",
                newName: "IX_PostGroup_ParentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostGroup",
                table: "PostGroup",
                column: "PostGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostGroup_PostGroup_ParentId",
                table: "PostGroup",
                column: "ParentId",
                principalTable: "PostGroup",
                principalColumn: "PostGroupId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_PostGroup_PostGroupId",
                table: "Posts",
                column: "PostGroupId",
                principalTable: "PostGroup",
                principalColumn: "PostGroupId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
