using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MushroomPocket.Migrations
{
    /// <inheritdoc />
    public partial class AddCharDiscriminator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Discriminator",
                table: "Characters",
                newName: "CharacterType");

            migrationBuilder.Sql("UPDATE Characters SET CharacterType = 'Waluigi' WHERE Name = 'Waluigi'");
            migrationBuilder.Sql("UPDATE Characters SET CharacterType = 'Daisy' WHERE Name = 'Daisy'");
            migrationBuilder.Sql("UPDATE Characters SET CharacterType = 'Wario' WHERE Name = 'Wario'");
            migrationBuilder.Sql("UPDATE Characters SET CharacterType = 'Luigi' WHERE Name = 'Luigi'");
            migrationBuilder.Sql("UPDATE Characters SET CharacterType = 'Peach' WHERE Name = 'Peach'");
            migrationBuilder.Sql("UPDATE Characters SET CharacterType = 'Mario' WHERE Name = 'Mario'");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CharacterType",
                table: "Characters",
                newName: "Discriminator");
        }
    }
}
