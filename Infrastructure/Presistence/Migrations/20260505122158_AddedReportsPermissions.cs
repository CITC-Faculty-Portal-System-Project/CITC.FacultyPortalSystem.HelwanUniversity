using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Presistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedReportsPermissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Code", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "DeletionReason", "Description", "DisplayName", "IsDeleted", "Type", "UpdatedAt", "UpdatedBy", "VersionNo" },
                values: new object[,]
                {
                    { 57, "Reports.Create", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, "Enables Assignee to Create entities which Reports includes", "Reports - Create", false, 13, null, null, 0 },
                    { 58, "Reports.Read", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, "Enables Assignee to Read entities which Reports includes", "Reports - Read", false, 13, null, null, 0 },
                    { 59, "Reports.Update", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, "Enables Assignee to Update entities which Reports includes", "Reports - Update", false, 13, null, null, 0 },
                    { 60, "Reports.Delete", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, "Enables Assignee to Delete entities which Reports includes", "Reports - Delete", false, 13, null, null, 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 60);
        }
    }
}
