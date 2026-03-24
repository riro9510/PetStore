using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PetStore.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Category = table.Column<string>(type: "TEXT", nullable: false),
                    Breed = table.Column<string>(type: "TEXT", nullable: false),
                    Age = table.Column<int>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pets", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Pets",
                columns: new[] { "Id", "Age", "Breed", "Category", "Description", "ImageUrl", "Name" },
                values: new object[,]
                {
                    { 1, 3, "Golden Retriever", "Dog", "Friendly, loyal, and ready for a home.", "images/dog1.jpg", "Max" },
                    { 2, 2, "Siamese", "Cat", "Curious, calm, and full of personality.", "images/cat1.jpg", "Luna" },
                    { 3, 100, "Mini Flame", "Dragon", "Magical companion with fiery charm.", "images/dragon1.jpg", "Draco" },
                    { 4, 5, "Silver Mane", "Unicorn", "Rare, graceful, and full of wonder.", "images/unicorn1.jpg", "Sparkle" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pets");
        }
    }
}
