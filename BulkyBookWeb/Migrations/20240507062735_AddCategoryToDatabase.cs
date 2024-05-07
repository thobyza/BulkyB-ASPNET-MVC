using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BulkyBookWeb.Migrations
{
    public partial class AddCategoryToDatabase : Migration
    {
        // [!] 2 methods: Up and Down

        // Up: what needs to do inside the migration
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"), 
                        // bsc we set it as Key column => so they made it identity column  & "1, 1" => increment by one every time
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                // adding Primary Key
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });
        }

        // Down: if smth goes down, we need to rollback the changes
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
