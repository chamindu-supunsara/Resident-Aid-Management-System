using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SITS.BNS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init20250611 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UIA_AppRoles",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedByName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdatedByName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UIA_AppRoles", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "UIA_GramaOffice",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocationCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: true),
                    Longitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Latitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedByName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdatedByName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UIA_GramaOffice", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "UIA_AppUsers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Organization = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrganizationId = table.Column<int>(type: "int", nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GramaOfficeId = table.Column<int>(type: "int", nullable: true),
                    CreatedByName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdatedByName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UIA_AppUsers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UIA_AppUsers_UIA_GramaOffice_GramaOfficeId",
                        column: x => x.GramaOfficeId,
                        principalTable: "UIA_GramaOffice",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "UIA_AppUserRoles",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppUserId = table.Column<int>(type: "int", nullable: false),
                    AppRoleId = table.Column<int>(type: "int", nullable: false),
                    CreatedByName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdatedByName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UIA_AppUserRoles", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UIA_AppUserRoles_UIA_AppRoles_AppRoleId",
                        column: x => x.AppRoleId,
                        principalTable: "UIA_AppRoles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UIA_AppUserRoles_UIA_AppUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "UIA_AppUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UIA_Family",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FamilyNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HouseUnitNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HouseholdNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GramaOfficeId = table.Column<int>(type: "int", nullable: false),
                    OfficerId = table.Column<int>(type: "int", nullable: false),
                    CreatedByName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdatedByName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UIA_Family", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UIA_Family_UIA_AppUsers_OfficerId",
                        column: x => x.OfficerId,
                        principalTable: "UIA_AppUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UIA_Family_UIA_GramaOffice_GramaOfficeId",
                        column: x => x.GramaOfficeId,
                        principalTable: "UIA_GramaOffice",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UIA_RefreshToken",
                columns: table => new
                {
                    Token = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Expires = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsRevoked = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedByName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdatedByName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UIA_RefreshToken", x => x.Token);
                    table.ForeignKey(
                        name: "FK_UIA_RefreshToken_UIA_AppUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "UIA_AppUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UIA_FamilyMember",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NIC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthDay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaritalStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Job = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Income = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FamilyId = table.Column<int>(type: "int", nullable: false),
                    CreatedByName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdatedByName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UIA_FamilyMember", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UIA_FamilyMember_UIA_Family_FamilyId",
                        column: x => x.FamilyId,
                        principalTable: "UIA_Family",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UIA_AidDetail",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SpecialAids = table.Column<bool>(type: "bit", nullable: false),
                    OtherAids = table.Column<bool>(type: "bit", nullable: false),
                    Aswesuma_1 = table.Column<bool>(type: "bit", nullable: false),
                    Aswesuma_2 = table.Column<bool>(type: "bit", nullable: false),
                    Aswesuma_3 = table.Column<bool>(type: "bit", nullable: false),
                    Aswesuma_4 = table.Column<bool>(type: "bit", nullable: false),
                    Aswesuma_5 = table.Column<bool>(type: "bit", nullable: false),
                    Aswesuma_6 = table.Column<bool>(type: "bit", nullable: false),
                    KidneyAid = table.Column<bool>(type: "bit", nullable: false),
                    Scholarship = table.Column<bool>(type: "bit", nullable: false),
                    EldersAid = table.Column<bool>(type: "bit", nullable: false),
                    HealthAid = table.Column<bool>(type: "bit", nullable: false),
                    DisabilityAid = table.Column<bool>(type: "bit", nullable: false),
                    Other = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MemberId = table.Column<int>(type: "int", nullable: false),
                    CreatedByName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdatedByName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UIA_AidDetail", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UIA_AidDetail_UIA_FamilyMember_MemberId",
                        column: x => x.MemberId,
                        principalTable: "UIA_FamilyMember",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UIA_AidDetail_MemberId",
                table: "UIA_AidDetail",
                column: "MemberId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UIA_AppUserRoles_AppRoleId",
                table: "UIA_AppUserRoles",
                column: "AppRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UIA_AppUserRoles_AppUserId",
                table: "UIA_AppUserRoles",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UIA_AppUsers_GramaOfficeId",
                table: "UIA_AppUsers",
                column: "GramaOfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_UIA_Family_GramaOfficeId",
                table: "UIA_Family",
                column: "GramaOfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_UIA_Family_OfficerId",
                table: "UIA_Family",
                column: "OfficerId");

            migrationBuilder.CreateIndex(
                name: "IX_UIA_FamilyMember_FamilyId",
                table: "UIA_FamilyMember",
                column: "FamilyId");

            migrationBuilder.CreateIndex(
                name: "IX_UIA_RefreshToken_UserId",
                table: "UIA_RefreshToken",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UIA_AidDetail");

            migrationBuilder.DropTable(
                name: "UIA_AppUserRoles");

            migrationBuilder.DropTable(
                name: "UIA_RefreshToken");

            migrationBuilder.DropTable(
                name: "UIA_FamilyMember");

            migrationBuilder.DropTable(
                name: "UIA_AppRoles");

            migrationBuilder.DropTable(
                name: "UIA_Family");

            migrationBuilder.DropTable(
                name: "UIA_AppUsers");

            migrationBuilder.DropTable(
                name: "UIA_GramaOffice");
        }
    }
}
