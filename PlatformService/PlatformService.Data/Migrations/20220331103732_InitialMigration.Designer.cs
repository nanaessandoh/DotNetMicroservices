// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PlatformService.Data;

#nullable disable

namespace PlatformService.Data.Migrations
{
    [DbContext(typeof(PlatFormDbContext))]
    [Migration("20220331103732_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("PlatformService.Data.Models.Platform", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Cost")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Publisher")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Platforms");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Cost = "Free",
                            Name = "DotNet",
                            Publisher = "Microsoft"
                        },
                        new
                        {
                            Id = 2,
                            Cost = "Free",
                            Name = "SQL Server Express",
                            Publisher = "Microsoft"
                        },
                        new
                        {
                            Id = 3,
                            Cost = "Free",
                            Name = "Kubernetes",
                            Publisher = "Cloud Native Computing Foundation"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
