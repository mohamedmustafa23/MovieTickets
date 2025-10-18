using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieTickets.Migrations
{
    /// <inheritdoc />
    public partial class AddDataToCategoryModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("insert into Categories (Name, Description) values ('Comedy|Sci-Fi|Thriller', 'Maecenas tristique, est et tempus semper, est quam pharetra magna, ac consequat metus sapien ut nunc. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Mauris viverra diam vitae quam. Suspendisse potenti. Nullam porttitor lacus at turpis. Donec posuere metus vitae ipsum. Aliquam non mauris. Morbi non lectus. Aliquam sit amet diam in magna bibendum imperdiet. Nullam orci pede, venenatis non, sodales sed, tincidunt eu, felis.');insert into Categories (Name, Description) values ('Comedy|Drama|Fantasy', 'Donec posuere metus vitae ipsum. Aliquam non mauris. Morbi non lectus. Aliquam sit amet diam in magna bibendum imperdiet. Nullam orci pede, venenatis non, sodales sed, tincidunt eu, felis. Fusce posuere felis sed lacus.');insert into Categories (Name, Description) values ('Action|Adventure|Sci-Fi|Thriller|IMAX', 'Phasellus in felis.');insert into Categories (Name, Description) values ('Drama', 'Ut tellus. Nulla ut erat id mauris vulputate elementum. Nullam varius. Nulla facilisi. Cras non velit nec nisi vulputate nonummy. Maecenas tincidunt lacus at velit.');insert into Categories (Name, Description) values ('Action|Adventure|Comedy|Horror', 'Vivamus tortor. Duis mattis egestas metus. Aenean fermentum.');");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Categories");
            migrationBuilder.Sql("DBCC CHECKIDENT ('Categories', RESEED, 0);");
        }
    }
}
