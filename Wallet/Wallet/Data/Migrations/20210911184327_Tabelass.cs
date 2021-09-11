using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Wallet.Data.Migrations
{
    public partial class Tabelass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carteiras_AspNetUsers_UsuarioId1",
                table: "Carteiras");

            migrationBuilder.DropIndex(
                name: "IX_Carteiras_UsuarioId1",
                table: "Carteiras");

            migrationBuilder.DropColumn(
                name: "UsuarioId1",
                table: "Carteiras");

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioId",
                table: "Carteiras",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_Carteiras_UsuarioId",
                table: "Carteiras",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Carteiras_AspNetUsers_UsuarioId",
                table: "Carteiras",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carteiras_AspNetUsers_UsuarioId",
                table: "Carteiras");

            migrationBuilder.DropIndex(
                name: "IX_Carteiras_UsuarioId",
                table: "Carteiras");

            migrationBuilder.AlterColumn<Guid>(
                name: "UsuarioId",
                table: "Carteiras",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioId1",
                table: "Carteiras",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Carteiras_UsuarioId1",
                table: "Carteiras",
                column: "UsuarioId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Carteiras_AspNetUsers_UsuarioId1",
                table: "Carteiras",
                column: "UsuarioId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
