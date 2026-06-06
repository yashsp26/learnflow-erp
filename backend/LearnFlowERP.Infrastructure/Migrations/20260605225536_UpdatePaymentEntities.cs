using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearnFlowERP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePaymentEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "FeeId",
                table: "StudentScholarships",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_StudentScholarships_FeeId",
                table: "StudentScholarships",
                column: "FeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentScholarships_Fees_FeeId",
                table: "StudentScholarships",
                column: "FeeId",
                principalTable: "Fees",
                principalColumn: "FeeId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentScholarships_Fees_FeeId",
                table: "StudentScholarships");

            migrationBuilder.DropIndex(
                name: "IX_StudentScholarships_FeeId",
                table: "StudentScholarships");

            migrationBuilder.DropColumn(
                name: "FeeId",
                table: "StudentScholarships");
        }
    }
}
