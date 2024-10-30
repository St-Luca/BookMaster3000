using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class changeClientToClientCard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookAuthor_Author_AuthorKey",
                table: "BookAuthor");

            migrationBuilder.DropForeignKey(
                name: "FK_BookAuthor_Books_BookKey",
                table: "BookAuthor");

            migrationBuilder.DropForeignKey(
                name: "FK_BookSubject_Books_BookKey",
                table: "BookSubject");

            migrationBuilder.DropForeignKey(
                name: "FK_BookSubject_Subject_SubjectId",
                table: "BookSubject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subject",
                table: "Subject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookSubject",
                table: "BookSubject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookAuthor",
                table: "BookAuthor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Author",
                table: "Author");

            migrationBuilder.DropColumn(
                name: "BookKey",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "IsRented",
                table: "Books");

            migrationBuilder.RenameTable(
                name: "Subject",
                newName: "Subjects");

            migrationBuilder.RenameTable(
                name: "BookSubject",
                newName: "BookSubjects");

            migrationBuilder.RenameTable(
                name: "BookAuthor",
                newName: "BookAuthors");

            migrationBuilder.RenameTable(
                name: "Author",
                newName: "Authors");

            migrationBuilder.RenameColumn(
                name: "IssueDate",
                table: "Issues",
                newName: "IssueTo");

            migrationBuilder.RenameColumn(
                name: "BookKey",
                table: "BookSubjects",
                newName: "BookId");

            migrationBuilder.RenameIndex(
                name: "IX_BookSubject_SubjectId",
                table: "BookSubjects",
                newName: "IX_BookSubjects_SubjectId");

            migrationBuilder.RenameColumn(
                name: "AuthorKey",
                table: "BookAuthors",
                newName: "AuthorId");

            migrationBuilder.RenameColumn(
                name: "BookKey",
                table: "BookAuthors",
                newName: "BookId");

            migrationBuilder.RenameIndex(
                name: "IX_BookAuthor_AuthorKey",
                table: "BookAuthors",
                newName: "IX_BookAuthors_AuthorId");

            migrationBuilder.RenameColumn(
                name: "Key",
                table: "Authors",
                newName: "Id");

            migrationBuilder.AddColumn<DateTime>(
                name: "IssueFrom",
                table: "Issues",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "PublicationDate",
                table: "Books",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeathDate",
                table: "Authors",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<DateTime>(
                name: "BirthDate",
                table: "Authors",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subjects",
                table: "Subjects",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookSubjects",
                table: "BookSubjects",
                columns: new[] { "BookId", "SubjectId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookAuthors",
                table: "BookAuthors",
                columns: new[] { "BookId", "AuthorId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Authors",
                table: "Authors",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Administrators",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Login = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administrators", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Covers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "text", nullable: false),
                    BookId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Covers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Covers_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Covers_BookId",
                table: "Covers",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookAuthors_Authors_AuthorId",
                table: "BookAuthors",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookAuthors_Books_BookId",
                table: "BookAuthors",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookSubjects_Books_BookId",
                table: "BookSubjects",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookSubjects_Subjects_SubjectId",
                table: "BookSubjects",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookAuthors_Authors_AuthorId",
                table: "BookAuthors");

            migrationBuilder.DropForeignKey(
                name: "FK_BookAuthors_Books_BookId",
                table: "BookAuthors");

            migrationBuilder.DropForeignKey(
                name: "FK_BookSubjects_Books_BookId",
                table: "BookSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_BookSubjects_Subjects_SubjectId",
                table: "BookSubjects");

            migrationBuilder.DropTable(
                name: "Administrators");

            migrationBuilder.DropTable(
                name: "Covers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subjects",
                table: "Subjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookSubjects",
                table: "BookSubjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookAuthors",
                table: "BookAuthors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Authors",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "IssueFrom",
                table: "Issues");

            migrationBuilder.RenameTable(
                name: "Subjects",
                newName: "Subject");

            migrationBuilder.RenameTable(
                name: "BookSubjects",
                newName: "BookSubject");

            migrationBuilder.RenameTable(
                name: "BookAuthors",
                newName: "BookAuthor");

            migrationBuilder.RenameTable(
                name: "Authors",
                newName: "Author");

            migrationBuilder.RenameColumn(
                name: "IssueTo",
                table: "Issues",
                newName: "IssueDate");

            migrationBuilder.RenameColumn(
                name: "BookId",
                table: "BookSubject",
                newName: "BookKey");

            migrationBuilder.RenameIndex(
                name: "IX_BookSubjects_SubjectId",
                table: "BookSubject",
                newName: "IX_BookSubject_SubjectId");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "BookAuthor",
                newName: "AuthorKey");

            migrationBuilder.RenameColumn(
                name: "BookId",
                table: "BookAuthor",
                newName: "BookKey");

            migrationBuilder.RenameIndex(
                name: "IX_BookAuthors_AuthorId",
                table: "BookAuthor",
                newName: "IX_BookAuthor_AuthorKey");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Author",
                newName: "Key");

            migrationBuilder.AddColumn<int>(
                name: "BookKey",
                table: "Issues",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "PublicationDate",
                table: "Books",
                type: "text",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<bool>(
                name: "IsRented",
                table: "Books",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "DeathDate",
                table: "Author",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BirthDate",
                table: "Author",
                type: "text",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subject",
                table: "Subject",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookSubject",
                table: "BookSubject",
                columns: new[] { "BookKey", "SubjectId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookAuthor",
                table: "BookAuthor",
                columns: new[] { "BookKey", "AuthorKey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Author",
                table: "Author",
                column: "Key");

            migrationBuilder.AddForeignKey(
                name: "FK_BookAuthor_Author_AuthorKey",
                table: "BookAuthor",
                column: "AuthorKey",
                principalTable: "Author",
                principalColumn: "Key",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookAuthor_Books_BookKey",
                table: "BookAuthor",
                column: "BookKey",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookSubject_Books_BookKey",
                table: "BookSubject",
                column: "BookKey",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookSubject_Subject_SubjectId",
                table: "BookSubject",
                column: "SubjectId",
                principalTable: "Subject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
