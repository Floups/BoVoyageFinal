﻿// <auto-generated />
using System;
using BoVoyage.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BoVoyage.Migrations
{
    [DbContext(typeof(BoVoyageContext))]
    [Migration("20200228184229_Initiale")]
    partial class Initiale
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BoVoyage.Models.Client", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Client");
                });

            modelBuilder.Entity("BoVoyage.Models.Destination", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<int?>("IdParente")
                        .HasColumnType("int");

                    b.Property<byte>("Niveau")
                        .HasColumnType("tinyint")
                        .HasComment(@"1 : Continent
2 : Pays
3 : R?gion");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("IdParente");

                    b.ToTable("Destination");
                });

            modelBuilder.Entity("BoVoyage.Models.Dossierresa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("IdClient")
                        .HasColumnType("int");

                    b.Property<byte>("IdEtatDossier")
                        .HasColumnType("tinyint");

                    b.Property<int>("IdVoyage")
                        .HasColumnType("int");

                    b.Property<string>("NumeroCb")
                        .HasColumnName("NumeroCB")
                        .HasColumnType("varchar(16)")
                        .HasMaxLength(16)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.HasIndex("IdClient");

                    b.HasIndex("IdEtatDossier");

                    b.HasIndex("IdVoyage");

                    b.ToTable("Dossierresa");
                });

            modelBuilder.Entity("BoVoyage.Models.Etatdossier", b =>
                {
                    b.Property<byte>("Id")
                        .HasColumnType("tinyint");

                    b.Property<string>("Libelle")
                        .IsRequired()
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.HasKey("Id");

                    b.ToTable("Etatdossier");
                });

            modelBuilder.Entity("BoVoyage.Models.Personne", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Civilite")
                        .IsRequired()
                        .HasColumnType("varchar(3)")
                        .HasMaxLength(3)
                        .IsUnicode(false);

                    b.Property<DateTime?>("Datenaissance")
                        .HasColumnType("date");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.Property<string>("Prenom")
                        .IsRequired()
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.Property<string>("Telephone")
                        .HasColumnType("varchar(16)")
                        .HasMaxLength(16)
                        .IsUnicode(false);

                    b.Property<byte>("TypePers")
                        .HasColumnType("tinyint");

                    b.HasKey("Id");

                    b.ToTable("Personne");
                });

            modelBuilder.Entity("BoVoyage.Models.Photo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("IdDestination")
                        .HasColumnType("int");

                    b.Property<string>("NomFichier")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("IdDestination");

                    b.ToTable("Photo");
                });

            modelBuilder.Entity("BoVoyage.Models.Voyage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateDepart")
                        .HasColumnType("date");

                    b.Property<DateTime>("DateRetour")
                        .HasColumnType("date");

                    b.Property<string>("Descriptif")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<int>("IdDestination")
                        .HasColumnType("int");

                    b.Property<int>("PlacesDispo")
                        .HasColumnType("int");

                    b.Property<decimal>("PrixHt")
                        .HasColumnName("PrixHT")
                        .HasColumnType("decimal(16, 4)");

                    b.Property<decimal>("Reduction")
                        .HasColumnType("decimal(3, 2)");

                    b.HasKey("Id");

                    b.HasIndex("IdDestination");

                    b.ToTable("Voyage");
                });

            modelBuilder.Entity("BoVoyage.Models.Voyageur", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int>("Idvoyage")
                        .HasColumnType("int");

                    b.HasKey("Id", "Idvoyage")
                        .HasName("Voyageur_Pk");

                    b.HasIndex("Idvoyage");

                    b.ToTable("Voyageur");
                });

            modelBuilder.Entity("BoVoyage.Models.Client", b =>
                {
                    b.HasOne("BoVoyage.Models.Personne", "IdNavigation")
                        .WithOne("Client")
                        .HasForeignKey("BoVoyage.Models.Client", "Id")
                        .HasConstraintName("Client_Personne_Fk")
                        .IsRequired();
                });

            modelBuilder.Entity("BoVoyage.Models.Destination", b =>
                {
                    b.HasOne("BoVoyage.Models.Destination", "IdParenteNavigation")
                        .WithMany("InverseIdParenteNavigation")
                        .HasForeignKey("IdParente")
                        .HasConstraintName("Destination_Destination_Fk");
                });

            modelBuilder.Entity("BoVoyage.Models.Dossierresa", b =>
                {
                    b.HasOne("BoVoyage.Models.Client", "IdClientNavigation")
                        .WithMany("Dossierresa")
                        .HasForeignKey("IdClient")
                        .HasConstraintName("Dossierresa_Client_Fk")
                        .IsRequired();

                    b.HasOne("BoVoyage.Models.Etatdossier", "IdEtatDossierNavigation")
                        .WithMany("Dossierresa")
                        .HasForeignKey("IdEtatDossier")
                        .HasConstraintName("Dossierresa_Etatdossier_Fk")
                        .IsRequired();

                    b.HasOne("BoVoyage.Models.Voyage", "IdVoyageNavigation")
                        .WithMany("Dossierresa")
                        .HasForeignKey("IdVoyage")
                        .HasConstraintName("Dossierresa_Voyage_Fk")
                        .IsRequired();
                });

            modelBuilder.Entity("BoVoyage.Models.Photo", b =>
                {
                    b.HasOne("BoVoyage.Models.Destination", "IdDestinationNavigation")
                        .WithMany("Photo")
                        .HasForeignKey("IdDestination")
                        .HasConstraintName("Photo_Destination_Fk")
                        .IsRequired();
                });

            modelBuilder.Entity("BoVoyage.Models.Voyage", b =>
                {
                    b.HasOne("BoVoyage.Models.Destination", "IdDestinationNavigation")
                        .WithMany("Voyage")
                        .HasForeignKey("IdDestination")
                        .HasConstraintName("Voyage_Destination_Fk")
                        .IsRequired();
                });

            modelBuilder.Entity("BoVoyage.Models.Voyageur", b =>
                {
                    b.HasOne("BoVoyage.Models.Personne", "IdNavigation")
                        .WithMany("Voyageur")
                        .HasForeignKey("Id")
                        .HasConstraintName("Voyageur_Personne_Fk")
                        .IsRequired();

                    b.HasOne("BoVoyage.Models.Voyage", "IdvoyageNavigation")
                        .WithMany("Voyageur")
                        .HasForeignKey("Idvoyage")
                        .HasConstraintName("Voyageur_Voyage_Fk")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
