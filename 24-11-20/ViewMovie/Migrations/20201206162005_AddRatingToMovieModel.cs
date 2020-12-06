using Microsoft.EntityFrameworkCore.Migrations;

namespace ViewMovie.Migrations
{
    public partial class AddRatingToMovieModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Raitng",
                table: "Movie",
                newName: "Rating");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Rating",
                table: "Movie",
                newName: "Raitng");
        }
    }
}
