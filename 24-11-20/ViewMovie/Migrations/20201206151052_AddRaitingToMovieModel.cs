using Microsoft.EntityFrameworkCore.Migrations;

namespace ViewMovie.Migrations
{
    public partial class AddRaitingToMovieModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Raitng",
                table: "Movie",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Raitng",
                table: "Movie");
        }
    }
}
