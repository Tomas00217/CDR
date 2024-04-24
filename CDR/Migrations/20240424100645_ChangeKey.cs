using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CDR.Migrations
{
    /// <inheritdoc />
    public partial class ChangeKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CallDetailRecords",
                table: "CallDetailRecords");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "CallDetailRecords");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CallDetailRecords",
                table: "CallDetailRecords",
                column: "Reference");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CallDetailRecords",
                table: "CallDetailRecords");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "CallDetailRecords",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CallDetailRecords",
                table: "CallDetailRecords",
                column: "Id");
        }
    }
}
