using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addedExhibitions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Clients_ClientId",
                table: "Issues");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "Issues",
                newName: "ClientCardId");

            migrationBuilder.RenameIndex(
                name: "IX_Issues_ClientId",
                table: "Issues",
                newName: "IX_Issues_ClientCardId");

            migrationBuilder.AddColumn<int>(
                name: "ClientCardId1",
                table: "Issues",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClientCardId2",
                table: "Issues",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ExhibitionId",
                table: "Books",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Exhibitions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exhibitions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Issues_ClientCardId1",
                table: "Issues",
                column: "ClientCardId1");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_ClientCardId2",
                table: "Issues",
                column: "ClientCardId2");

            migrationBuilder.CreateIndex(
                name: "IX_Books_ExhibitionId",
                table: "Books",
                column: "ExhibitionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Exhibitions_ExhibitionId",
                table: "Books",
                column: "ExhibitionId",
                principalTable: "Exhibitions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Clients_ClientCardId",
                table: "Issues",
                column: "ClientCardId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Clients_ClientCardId1",
                table: "Issues",
                column: "ClientCardId1",
                principalTable: "Clients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Clients_ClientCardId2",
                table: "Issues",
                column: "ClientCardId2",
                principalTable: "Clients",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Exhibitions_ExhibitionId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Clients_ClientCardId",
                table: "Issues");

            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Clients_ClientCardId1",
                table: "Issues");

            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Clients_ClientCardId2",
                table: "Issues");

            migrationBuilder.DropTable(
                name: "Exhibitions");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Issues_ClientCardId1",
                table: "Issues");

            migrationBuilder.DropIndex(
                name: "IX_Issues_ClientCardId2",
                table: "Issues");

            migrationBuilder.DropIndex(
                name: "IX_Books_ExhibitionId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "ClientCardId1",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "ClientCardId2",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "ExhibitionId",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "ClientCardId",
                table: "Issues",
                newName: "ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_Issues_ClientCardId",
                table: "Issues",
                newName: "IX_Issues_ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Clients_ClientId",
                table: "Issues",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
