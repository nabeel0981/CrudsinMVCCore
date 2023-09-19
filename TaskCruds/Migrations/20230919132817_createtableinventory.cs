using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskCruds.Migrations
{
    /// <inheritdoc />
    public partial class createtableinventory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Inventories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItemUnit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItemBalanceInSystem = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ItemBalanceInStore = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DamageMaterial = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ExpiredMaterial = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NetMaterial = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventories", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Inventories");
        }
    }
}
