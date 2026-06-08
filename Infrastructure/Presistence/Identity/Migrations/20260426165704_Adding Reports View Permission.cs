using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Presistence.Identity.Migrations
{
    /// <inheritdoc />
    public partial class AddingReportsViewPermission : Migration
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

            migrationBuilder.InsertData(
                table: "UsersPermissions",
                columns: new[] { "PermissionId", "UserId", "AssignedAt", "AssignedBy", "AssignerId", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "DeletionReason", "GrantedAt", "GrantedBy", "GranterId", "IsDeleted", "UpdatedAt", "UpdatedBy", "VersionNo" },
                values: new object[,]
                {
                    { 57, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", null, null, null, null, "", null, false, null, null, 1 },
                    { 58, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", null, null, null, null, "", null, false, null, null, 1 },
                    { 59, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", null, null, null, null, "", null, false, null, null, 1 },
                    { 60, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", null, null, null, null, "", null, false, null, null, 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UsersPermissions",
                keyColumns: new[] { "PermissionId", "UserId" },
                keyValues: new object[] { 57, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7") });

            migrationBuilder.DeleteData(
                table: "UsersPermissions",
                keyColumns: new[] { "PermissionId", "UserId" },
                keyValues: new object[] { 58, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7") });

            migrationBuilder.DeleteData(
                table: "UsersPermissions",
                keyColumns: new[] { "PermissionId", "UserId" },
                keyValues: new object[] { 59, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7") });

            migrationBuilder.DeleteData(
                table: "UsersPermissions",
                keyColumns: new[] { "PermissionId", "UserId" },
                keyValues: new object[] { 60, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7") });

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
