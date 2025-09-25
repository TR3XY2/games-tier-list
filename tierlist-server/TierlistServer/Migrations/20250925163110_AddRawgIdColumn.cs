﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TierlistServer.Migrations
{
    /// <inheritdoc />
    public partial class AddRawgIdColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "rawg_id",
                table: "games",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "rawg_id",
                table: "games");
        }
    }
}
