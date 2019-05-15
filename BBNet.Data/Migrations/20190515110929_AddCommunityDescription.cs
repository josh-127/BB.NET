﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace BBNet.Data.Migrations
{
    public partial class AddCommunityDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Communities",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Communities");
        }
    }
}
