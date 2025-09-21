using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UdemyMicroservice.Order.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class update_order_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalPrice",
                table: "Orders",
                newName: "LastPrice");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastPrice",
                table: "Orders",
                newName: "TotalPrice");
        }
    }
}
