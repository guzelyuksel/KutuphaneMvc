using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KutuphaneMvc.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tur",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ad = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tur", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "YayinEvi",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KurulusYili = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Adres = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Mail = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YayinEvi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Kitap",
                columns: table => new
                {
                    Isbn = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    Ad = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    BasimYili = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BasimSayisi = table.Column<int>(type: "int", nullable: false),
                    SayfaSayisi = table.Column<int>(type: "int", nullable: false),
                    YayinEviId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kitap", x => x.Isbn);
                    table.ForeignKey(
                        name: "FK_Kitap_YayinEvi_YayinEviId",
                        column: x => x.YayinEviId,
                        principalTable: "YayinEvi",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Yazar",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ad = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Soyad = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DogumTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Cinsiyet = table.Column<int>(type: "int", nullable: false),
                    Adres = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Mail = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    YayinEviId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Yazar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Yazar_YayinEvi_YayinEviId",
                        column: x => x.YayinEviId,
                        principalTable: "YayinEvi",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "KitapTur",
                columns: table => new
                {
                    KitaplarIsbn = table.Column<string>(type: "nvarchar(13)", nullable: false),
                    TurlerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KitapTur", x => new { x.KitaplarIsbn, x.TurlerId });
                    table.ForeignKey(
                        name: "FK_KitapTur_Kitap_KitaplarIsbn",
                        column: x => x.KitaplarIsbn,
                        principalTable: "Kitap",
                        principalColumn: "Isbn",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KitapTur_Tur_TurlerId",
                        column: x => x.TurlerId,
                        principalTable: "Tur",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KitapYazar",
                columns: table => new
                {
                    KitaplarIsbn = table.Column<string>(type: "nvarchar(13)", nullable: false),
                    YazarlarId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KitapYazar", x => new { x.KitaplarIsbn, x.YazarlarId });
                    table.ForeignKey(
                        name: "FK_KitapYazar_Kitap_KitaplarIsbn",
                        column: x => x.KitaplarIsbn,
                        principalTable: "Kitap",
                        principalColumn: "Isbn",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KitapYazar_Yazar_YazarlarId",
                        column: x => x.YazarlarId,
                        principalTable: "Yazar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Kitap_YayinEviId",
                table: "Kitap",
                column: "YayinEviId");

            migrationBuilder.CreateIndex(
                name: "IX_KitapTur_TurlerId",
                table: "KitapTur",
                column: "TurlerId");

            migrationBuilder.CreateIndex(
                name: "IX_KitapYazar_YazarlarId",
                table: "KitapYazar",
                column: "YazarlarId");

            migrationBuilder.CreateIndex(
                name: "IX_Yazar_YayinEviId",
                table: "Yazar",
                column: "YayinEviId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KitapTur");

            migrationBuilder.DropTable(
                name: "KitapYazar");

            migrationBuilder.DropTable(
                name: "Tur");

            migrationBuilder.DropTable(
                name: "Kitap");

            migrationBuilder.DropTable(
                name: "Yazar");

            migrationBuilder.DropTable(
                name: "YayinEvi");
        }
    }
}
