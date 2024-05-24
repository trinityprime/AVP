using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MushroomPocket.Migrations
{
    /// <inheritdoc />
    public partial class RemovedMushroomMasters : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MushroomMasters");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MushroomMasters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    NoToTransform = table.Column<int>(type: "INTEGER", nullable: false),
                    TransformTo = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MushroomMasters", x => x.Id);
                });
        }
    }
}
