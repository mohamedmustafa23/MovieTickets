using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieTickets.Migrations
{
    /// <inheritdoc />
    public partial class AddDataToActorModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("insert into Actors (Name) values ('Maynord Capes');insert into Actors (Name) values ('Caryl Seel');insert into Actors (Name) values ('Jayson Cottrill');insert into Actors (Name) values ('Avie Dallimare');insert into Actors (Name) values ('Deborah Juan');");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete From Actors");
            migrationBuilder.Sql("DBCC CHECKIDENT ('Actors', RESEED, 0);");
        }
    }
}
