using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BoVoyage.Migrations
{
    public partial class Initiale : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Destination",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdParente = table.Column<int>(nullable: true),
                    Nom = table.Column<string>(maxLength: 100, nullable: false),
                    Niveau = table.Column<byte>(nullable: false, comment: @"1 : Continent
2 : Pays
3 : R?gion"),
                    Description = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Destination", x => x.Id);
                    table.ForeignKey(
                        name: "Destination_Destination_Fk",
                        column: x => x.IdParente,
                        principalTable: "Destination",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Etatdossier",
                columns: table => new
                {
                    Id = table.Column<byte>(nullable: false),
                    Libelle = table.Column<string>(maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Etatdossier", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Personne",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypePers = table.Column<byte>(nullable: false),
                    Civilite = table.Column<string>(unicode: false, maxLength: 3, nullable: false),
                    Nom = table.Column<string>(maxLength: 30, nullable: false),
                    Prenom = table.Column<string>(maxLength: 30, nullable: false),
                    Email = table.Column<string>(maxLength: 30, nullable: false),
                    Telephone = table.Column<string>(unicode: false, maxLength: 16, nullable: true),
                    Datenaissance = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personne", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Photo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomFichier = table.Column<string>(maxLength: 100, nullable: false),
                    IdDestination = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photo", x => x.Id);
                    table.ForeignKey(
                        name: "Photo_Destination_Fk",
                        column: x => x.IdDestination,
                        principalTable: "Destination",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Voyage",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdDestination = table.Column<int>(nullable: false),
                    DateDepart = table.Column<DateTime>(type: "date", nullable: false),
                    DateRetour = table.Column<DateTime>(type: "date", nullable: false),
                    PlacesDispo = table.Column<int>(nullable: false),
                    PrixHT = table.Column<decimal>(type: "decimal(16, 4)", nullable: false),
                    Reduction = table.Column<decimal>(type: "decimal(3, 2)", nullable: false),
                    Descriptif = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Voyage", x => x.Id);
                    table.ForeignKey(
                        name: "Voyage_Destination_Fk",
                        column: x => x.IdDestination,
                        principalTable: "Destination",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.Id);
                    table.ForeignKey(
                        name: "Client_Personne_Fk",
                        column: x => x.Id,
                        principalTable: "Personne",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Voyageur",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Idvoyage = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Voyageur_Pk", x => new { x.Id, x.Idvoyage });
                    table.ForeignKey(
                        name: "Voyageur_Personne_Fk",
                        column: x => x.Id,
                        principalTable: "Personne",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "Voyageur_Voyage_Fk",
                        column: x => x.Idvoyage,
                        principalTable: "Voyage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Dossierresa",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeroCB = table.Column<string>(unicode: false, maxLength: 16, nullable: true),
                    IdClient = table.Column<int>(nullable: false),
                    IdEtatDossier = table.Column<byte>(nullable: false),
                    IdVoyage = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dossierresa", x => x.Id);
                    table.ForeignKey(
                        name: "Dossierresa_Client_Fk",
                        column: x => x.IdClient,
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "Dossierresa_Etatdossier_Fk",
                        column: x => x.IdEtatDossier,
                        principalTable: "Etatdossier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "Dossierresa_Voyage_Fk",
                        column: x => x.IdVoyage,
                        principalTable: "Voyage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Destination_IdParente",
                table: "Destination",
                column: "IdParente");

            migrationBuilder.CreateIndex(
                name: "IX_Dossierresa_IdClient",
                table: "Dossierresa",
                column: "IdClient");

            migrationBuilder.CreateIndex(
                name: "IX_Dossierresa_IdEtatDossier",
                table: "Dossierresa",
                column: "IdEtatDossier");

            migrationBuilder.CreateIndex(
                name: "IX_Dossierresa_IdVoyage",
                table: "Dossierresa",
                column: "IdVoyage");

            migrationBuilder.CreateIndex(
                name: "IX_Photo_IdDestination",
                table: "Photo",
                column: "IdDestination");

            migrationBuilder.CreateIndex(
                name: "IX_Voyage_IdDestination",
                table: "Voyage",
                column: "IdDestination");

            migrationBuilder.CreateIndex(
                name: "IX_Voyageur_Idvoyage",
                table: "Voyageur",
                column: "Idvoyage");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dossierresa");

            migrationBuilder.DropTable(
                name: "Photo");

            migrationBuilder.DropTable(
                name: "Voyageur");

            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.DropTable(
                name: "Etatdossier");

            migrationBuilder.DropTable(
                name: "Voyage");

            migrationBuilder.DropTable(
                name: "Personne");

            migrationBuilder.DropTable(
                name: "Destination");
        }
    }
}
