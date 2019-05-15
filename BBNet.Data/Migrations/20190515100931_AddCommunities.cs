using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BBNet.Data.Migrations
{
    public partial class AddCommunities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CommunityId",
                table: "Forums",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CommunityId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Communities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Communities", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Forums_CommunityId",
                table: "Forums",
                column: "CommunityId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CommunityId",
                table: "AspNetUsers",
                column: "CommunityId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Communities_CommunityId",
                table: "AspNetUsers",
                column: "CommunityId",
                principalTable: "Communities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Forums_Communities_CommunityId",
                table: "Forums",
                column: "CommunityId",
                principalTable: "Communities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Communities_CommunityId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Forums_Communities_CommunityId",
                table: "Forums");

            migrationBuilder.DropTable(
                name: "Communities");

            migrationBuilder.DropIndex(
                name: "IX_Forums_CommunityId",
                table: "Forums");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CommunityId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CommunityId",
                table: "Forums");

            migrationBuilder.DropColumn(
                name: "CommunityId",
                table: "AspNetUsers");
        }
    }
}
