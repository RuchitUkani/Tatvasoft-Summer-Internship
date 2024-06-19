using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Data_Access_Layer.Migrations
{
    /// <inheritdoc />
    public partial class addmission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Missions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MissionTitle = table.Column<string>(type: "text", nullable: true),
                    MissionDescription = table.Column<string>(type: "text", nullable: true),
                    MissionOrganisationName = table.Column<string>(type: "text", nullable: true),
                    MissionOrganisationDetail = table.Column<string>(type: "text", nullable: true),
                    CountryId = table.Column<int>(type: "integer", nullable: false),
                    CityId = table.Column<int>(type: "integer", nullable: false),
                    StartDate = table.Column<DateTime>(type: "date", nullable: false),
                    EndDate = table.Column<DateTime>(type: "date", nullable: false),
                    MissionType = table.Column<string>(type: "text", nullable: true),
                    TotalSheets = table.Column<int>(type: "integer", nullable: false),
                    RegistrationDeadLine = table.Column<DateTime>(type: "date", nullable: true),
                    MissionThemeId = table.Column<string>(type: "text", nullable: true),
                    MissionSkillId = table.Column<string>(type: "text", nullable: true),
                    MissionImages = table.Column<string>(type: "text", nullable: true),
                    MissionDocuments = table.Column<string>(type: "text", nullable: true),
                    MissionAvailability = table.Column<string>(type: "text", nullable: true),
                    MissionVideoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Missions", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Missions");
        }
    }
}
