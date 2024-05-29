using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Books.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddforeignKeytoestablishrelationshipbetweenBookssTableandCategoriessTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Bookss",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Bookss",
                keyColumn: "Id",
                keyValue: 1,
                column: "CategoryId",
                value: 1002);

            migrationBuilder.UpdateData(
                table: "Bookss",
                keyColumn: "Id",
                keyValue: 2,
                column: "CategoryId",
                value: 1003);

            migrationBuilder.UpdateData(
                table: "Bookss",
                keyColumn: "Id",
                keyValue: 3,
                column: "CategoryId",
                value: 1002);

            migrationBuilder.UpdateData(
                table: "Bookss",
                keyColumn: "Id",
                keyValue: 4,
                column: "CategoryId",
                value: 1001);

            migrationBuilder.UpdateData(
                table: "Bookss",
                keyColumn: "Id",
                keyValue: 5,
                column: "CategoryId",
                value: 1001);

            migrationBuilder.UpdateData(
                table: "Bookss",
                keyColumn: "Id",
                keyValue: 6,
                column: "CategoryId",
                value: 1003);

            migrationBuilder.CreateIndex(
                name: "IX_Bookss_CategoryId",
                table: "Bookss",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookss_Categoriess_CategoryId",
                table: "Bookss",
                column: "CategoryId",
                principalTable: "Categoriess",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookss_Categoriess_CategoryId",
                table: "Bookss");

            migrationBuilder.DropIndex(
                name: "IX_Bookss_CategoryId",
                table: "Bookss");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Bookss");
        }
    }
}
