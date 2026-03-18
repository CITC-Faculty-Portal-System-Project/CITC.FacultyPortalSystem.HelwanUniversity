using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Presistence.Identity.Migrations
{
    /// <inheritdoc />
    public partial class PermissionEntitesInitalCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletionReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VersionNo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RolesPermissions",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PermissionId = table.Column<int>(type: "int", nullable: false),
                    AssignedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AssignedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AssignerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GrantedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GrantedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GranterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletionReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VersionNo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolesPermissions", x => new { x.RoleId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_RolesPermissions_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolesPermissions_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsersPermissions",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PermissionId = table.Column<int>(type: "int", nullable: false),
                    AssignedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AssignedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AssignerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GrantedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GrantedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GranterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletionReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VersionNo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersPermissions", x => new { x.UserId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_UsersPermissions_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersPermissions_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Code", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "DeletionReason", "Description", "DisplayName", "IsDeleted", "Type", "UpdatedAt", "UpdatedBy", "VersionNo" },
                values: new object[,]
                {
                    { 1, "UserAccount.Create", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, "Enables Assignee to Create entities which UserAccount includes", "UserAccount - Create", false, 1, null, null, 0 },
                    { 2, "UserAccount.Read", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, "Enables Assignee to Read entities which UserAccount includes", "UserAccount - Read", false, 1, null, null, 0 },
                    { 3, "UserAccount.Update", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, "Enables Assignee to Update entities which UserAccount includes", "UserAccount - Update", false, 1, null, null, 0 },
                    { 4, "UserAccount.Delete", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, "Enables Assignee to Delete entities which UserAccount includes", "UserAccount - Delete", false, 1, null, null, 0 },
                    { 5, "FacultyMemberData.Create", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, "Enables Assignee to Create entities which FacultyMemberData includes", "FacultyMemberData - Create", false, 2, null, null, 0 },
                    { 6, "FacultyMemberData.Read", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, "Enables Assignee to Read entities which FacultyMemberData includes", "FacultyMemberData - Read", false, 2, null, null, 0 },
                    { 7, "FacultyMemberData.Update", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, "Enables Assignee to Update entities which FacultyMemberData includes", "FacultyMemberData - Update", false, 2, null, null, 0 },
                    { 8, "FacultyMemberData.Delete", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, "Enables Assignee to Delete entities which FacultyMemberData includes", "FacultyMemberData - Delete", false, 2, null, null, 0 },
                    { 9, "FacultyMemberContributionsData.Create", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, "Enables Assignee to Create entities which FacultyMemberContributionsData includes", "FacultyMemberContributionsData - Create", false, 3, null, null, 0 },
                    { 10, "FacultyMemberContributionsData.Read", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, "Enables Assignee to Read entities which FacultyMemberContributionsData includes", "FacultyMemberContributionsData - Read", false, 3, null, null, 0 },
                    { 11, "FacultyMemberContributionsData.Update", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, "Enables Assignee to Update entities which FacultyMemberContributionsData includes", "FacultyMemberContributionsData - Update", false, 3, null, null, 0 },
                    { 12, "FacultyMemberContributionsData.Delete", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, "Enables Assignee to Delete entities which FacultyMemberContributionsData includes", "FacultyMemberContributionsData - Delete", false, 3, null, null, 0 },
                    { 13, "FacultyMemberExperincesData.Create", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, "Enables Assignee to Create entities which FacultyMemberExperincesData includes", "FacultyMemberExperincesData - Create", false, 4, null, null, 0 },
                    { 14, "FacultyMemberExperincesData.Read", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, "Enables Assignee to Read entities which FacultyMemberExperincesData includes", "FacultyMemberExperincesData - Read", false, 4, null, null, 0 },
                    { 15, "FacultyMemberExperincesData.Update", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, "Enables Assignee to Update entities which FacultyMemberExperincesData includes", "FacultyMemberExperincesData - Update", false, 4, null, null, 0 },
                    { 16, "FacultyMemberExperincesData.Delete", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, "Enables Assignee to Delete entities which FacultyMemberExperincesData includes", "FacultyMemberExperincesData - Delete", false, 4, null, null, 0 },
                    { 17, "FacultyMemberHigherStudiesData.Create", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, "Enables Assignee to Create entities which FacultyMemberHigherStudiesData includes", "FacultyMemberHigherStudiesData - Create", false, 5, null, null, 0 },
                    { 18, "FacultyMemberHigherStudiesData.Read", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, "Enables Assignee to Read entities which FacultyMemberHigherStudiesData includes", "FacultyMemberHigherStudiesData - Read", false, 5, null, null, 0 },
                    { 19, "FacultyMemberHigherStudiesData.Update", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, "Enables Assignee to Update entities which FacultyMemberHigherStudiesData includes", "FacultyMemberHigherStudiesData - Update", false, 5, null, null, 0 },
                    { 20, "FacultyMemberHigherStudiesData.Delete", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, "Enables Assignee to Delete entities which FacultyMemberHigherStudiesData includes", "FacultyMemberHigherStudiesData - Delete", false, 5, null, null, 0 },
                    { 21, "FacultyMemberMissionsData.Create", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, "Enables Assignee to Create entities which FacultyMemberMissionsData includes", "FacultyMemberMissionsData - Create", false, 6, null, null, 0 },
                    { 22, "FacultyMemberMissionsData.Read", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, "Enables Assignee to Read entities which FacultyMemberMissionsData includes", "FacultyMemberMissionsData - Read", false, 6, null, null, 0 },
                    { 23, "FacultyMemberMissionsData.Update", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, "Enables Assignee to Update entities which FacultyMemberMissionsData includes", "FacultyMemberMissionsData - Update", false, 6, null, null, 0 },
                    { 24, "FacultyMemberMissionsData.Delete", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, "Enables Assignee to Delete entities which FacultyMemberMissionsData includes", "FacultyMemberMissionsData - Delete", false, 6, null, null, 0 },
                    { 25, "FacultyMemberPrizesData.Create", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, "Enables Assignee to Create entities which FacultyMemberPrizesData includes", "FacultyMemberPrizesData - Create", false, 7, null, null, 0 },
                    { 26, "FacultyMemberPrizesData.Read", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, "Enables Assignee to Read entities which FacultyMemberPrizesData includes", "FacultyMemberPrizesData - Read", false, 7, null, null, 0 },
                    { 27, "FacultyMemberPrizesData.Update", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, "Enables Assignee to Update entities which FacultyMemberPrizesData includes", "FacultyMemberPrizesData - Update", false, 7, null, null, 0 },
                    { 28, "FacultyMemberPrizesData.Delete", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, "Enables Assignee to Delete entities which FacultyMemberPrizesData includes", "FacultyMemberPrizesData - Delete", false, 7, null, null, 0 },
                    { 29, "FacultyMemberProjectsAndComiteesData.Create", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, "Enables Assignee to Create entities which FacultyMemberProjectsAndComiteesData includes", "FacultyMemberProjectsAndComiteesData - Create", false, 8, null, null, 0 },
                    { 30, "FacultyMemberProjectsAndComiteesData.Read", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, "Enables Assignee to Read entities which FacultyMemberProjectsAndComiteesData includes", "FacultyMemberProjectsAndComiteesData - Read", false, 8, null, null, 0 },
                    { 31, "FacultyMemberProjectsAndComiteesData.Update", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, "Enables Assignee to Update entities which FacultyMemberProjectsAndComiteesData includes", "FacultyMemberProjectsAndComiteesData - Update", false, 8, null, null, 0 },
                    { 32, "FacultyMemberProjectsAndComiteesData.Delete", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, "Enables Assignee to Delete entities which FacultyMemberProjectsAndComiteesData includes", "FacultyMemberProjectsAndComiteesData - Delete", false, 8, null, null, 0 },
                    { 33, "FacultyMemberResearchesData.Create", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, "Enables Assignee to Create entities which FacultyMemberResearchesData includes", "FacultyMemberResearchesData - Create", false, 9, null, null, 0 },
                    { 34, "FacultyMemberResearchesData.Read", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, "Enables Assignee to Read entities which FacultyMemberResearchesData includes", "FacultyMemberResearchesData - Read", false, 9, null, null, 0 },
                    { 35, "FacultyMemberResearchesData.Update", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, "Enables Assignee to Update entities which FacultyMemberResearchesData includes", "FacultyMemberResearchesData - Update", false, 9, null, null, 0 },
                    { 36, "FacultyMemberResearchesData.Delete", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, "Enables Assignee to Delete entities which FacultyMemberResearchesData includes", "FacultyMemberResearchesData - Delete", false, 9, null, null, 0 },
                    { 37, "FacultyMemberScientificProgressionData.Create", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, "Enables Assignee to Create entities which FacultyMemberScientificProgressionData includes", "FacultyMemberScientificProgressionData - Create", false, 10, null, null, 0 },
                    { 38, "FacultyMemberScientificProgressionData.Read", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, "Enables Assignee to Read entities which FacultyMemberScientificProgressionData includes", "FacultyMemberScientificProgressionData - Read", false, 10, null, null, 0 },
                    { 39, "FacultyMemberScientificProgressionData.Update", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, "Enables Assignee to Update entities which FacultyMemberScientificProgressionData includes", "FacultyMemberScientificProgressionData - Update", false, 10, null, null, 0 },
                    { 40, "FacultyMemberScientificProgressionData.Delete", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, "Enables Assignee to Delete entities which FacultyMemberScientificProgressionData includes", "FacultyMemberScientificProgressionData - Delete", false, 10, null, null, 0 },
                    { 41, "FacultyMemberWritingsData.Create", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, "Enables Assignee to Create entities which FacultyMemberWritingsData includes", "FacultyMemberWritingsData - Create", false, 11, null, null, 0 },
                    { 42, "FacultyMemberWritingsData.Read", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, "Enables Assignee to Read entities which FacultyMemberWritingsData includes", "FacultyMemberWritingsData - Read", false, 11, null, null, 0 },
                    { 43, "FacultyMemberWritingsData.Update", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, "Enables Assignee to Update entities which FacultyMemberWritingsData includes", "FacultyMemberWritingsData - Update", false, 11, null, null, 0 },
                    { 44, "FacultyMemberWritingsData.Delete", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, "Enables Assignee to Delete entities which FacultyMemberWritingsData includes", "FacultyMemberWritingsData - Delete", false, 11, null, null, 0 },
                    { 45, "Tickets.Create", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, "Enables Assignee to Create entities which Tickets includes", "Tickets - Create", false, 12, null, null, 0 },
                    { 46, "Tickets.Read", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, "Enables Assignee to Read entities which Tickets includes", "Tickets - Read", false, 12, null, null, 0 },
                    { 47, "Tickets.Update", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, "Enables Assignee to Update entities which Tickets includes", "Tickets - Update", false, 12, null, null, 0 },
                    { 48, "Tickets.Delete", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, "Enables Assignee to Delete entities which Tickets includes", "Tickets - Delete", false, 12, null, null, 0 },
                    { 49, "Tickets.Assign", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, "Allows assigning tickets to support agents", "Tickets - Assign", false, 12, null, null, 0 },
                    { 50, "Tickets.Reply", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, "Allows replying to tickets", "Tickets - Reply", false, 12, null, null, 0 },
                    { 51, "Tickets.Close", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, "Allows closing tickets", "Tickets - Close", false, 12, null, null, 0 },
                    { 52, "Tickets.Reopen", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, "Allows reopening tickets", "Tickets - Reopen", false, 12, null, null, 0 },
                    { 53, "Tickets.ChangePriority", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, "Allows changing ticket priority", "Tickets - Change Priority", false, 12, null, null, 0 },
                    { 54, "Tickets.ChangeStatus", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, "Allows changing ticket status", "Tickets - Change Status", false, 12, null, null, 0 },
                    { 55, "Tickets.ViewAll", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, "Allows viewing all tickets in the system", "Tickets - View All", false, 12, null, null, 0 },
                    { 56, "Tickets.ViewAssigned", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, "Allows viewing only assigned tickets", "Tickets - View Assigned", false, 12, null, null, 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_Code",
                table: "Permissions",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_DisplayName",
                table: "Permissions",
                column: "DisplayName");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_Id",
                table: "Permissions",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_Type",
                table: "Permissions",
                column: "Type");

            migrationBuilder.CreateIndex(
                name: "IX_RolesPermissions_PermissionId",
                table: "RolesPermissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersPermissions_PermissionId",
                table: "UsersPermissions",
                column: "PermissionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RolesPermissions");

            migrationBuilder.DropTable(
                name: "UsersPermissions");

            migrationBuilder.DropTable(
                name: "Permissions");
        }
    }
}
