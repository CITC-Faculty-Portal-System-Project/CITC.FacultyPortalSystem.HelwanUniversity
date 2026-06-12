using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Presistence.Identity.Migrations
{
    /// <inheritdoc />
    public partial class GvivngAllPermissionsToManager : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "UsersPermissions",
                columns: new[] { "PermissionId", "UserId", "AssignedAt", "AssignedBy", "AssignerId", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "DeletionReason", "GrantedAt", "GrantedBy", "GranterId", "IsDeleted", "UpdatedAt", "UpdatedBy", "VersionNo" },
                values: new object[,]
                {
                    { 1, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", null, null, null, null, "", null, false, null, null, 1 },
                    { 2, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", null, null, null, null, "", null, false, null, null, 1 },
                    { 3, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", null, null, null, null, "", null, false, null, null, 1 },
                    { 4, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", null, null, null, null, "", null, false, null, null, 1 },
                    { 5, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", null, null, null, null, "", null, false, null, null, 1 },
                    { 6, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", null, null, null, null, "", null, false, null, null, 1 },
                    { 7, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", null, null, null, null, "", null, false, null, null, 1 },
                    { 8, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", null, null, null, null, "", null, false, null, null, 1 },
                    { 9, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", null, null, null, null, "", null, false, null, null, 1 },
                    { 10, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", null, null, null, null, "", null, false, null, null, 1 },
                    { 11, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", null, null, null, null, "", null, false, null, null, 1 },
                    { 12, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", null, null, null, null, "", null, false, null, null, 1 },
                    { 13, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", null, null, null, null, "", null, false, null, null, 1 },
                    { 14, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", null, null, null, null, "", null, false, null, null, 1 },
                    { 15, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", null, null, null, null, "", null, false, null, null, 1 },
                    { 16, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", null, null, null, null, "", null, false, null, null, 1 },
                    { 17, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", null, null, null, null, "", null, false, null, null, 1 },
                    { 18, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", null, null, null, null, "", null, false, null, null, 1 },
                    { 19, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", null, null, null, null, "", null, false, null, null, 1 },
                    { 20, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", null, null, null, null, "", null, false, null, null, 1 },
                    { 21, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", null, null, null, null, "", null, false, null, null, 1 },
                    { 22, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", null, null, null, null, "", null, false, null, null, 1 },
                    { 23, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", null, null, null, null, "", null, false, null, null, 1 },
                    { 24, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", null, null, null, null, "", null, false, null, null, 1 },
                    { 25, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", null, null, null, null, "", null, false, null, null, 1 },
                    { 26, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", null, null, null, null, "", null, false, null, null, 1 },
                    { 27, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", null, null, null, null, "", null, false, null, null, 1 },
                    { 28, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", null, null, null, null, "", null, false, null, null, 1 },
                    { 29, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", null, null, null, null, "", null, false, null, null, 1 },
                    { 30, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", null, null, null, null, "", null, false, null, null, 1 },
                    { 31, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", null, null, null, null, "", null, false, null, null, 1 },
                    { 32, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", null, null, null, null, "", null, false, null, null, 1 },
                    { 33, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", null, null, null, null, "", null, false, null, null, 1 },
                    { 34, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", null, null, null, null, "", null, false, null, null, 1 },
                    { 35, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", null, null, null, null, "", null, false, null, null, 1 },
                    { 36, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", null, null, null, null, "", null, false, null, null, 1 },
                    { 37, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", null, null, null, null, "", null, false, null, null, 1 },
                    { 38, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", null, null, null, null, "", null, false, null, null, 1 },
                    { 39, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", null, null, null, null, "", null, false, null, null, 1 },
                    { 40, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", null, null, null, null, "", null, false, null, null, 1 },
                    { 41, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", null, null, null, null, "", null, false, null, null, 1 },
                    { 42, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", null, null, null, null, "", null, false, null, null, 1 },
                    { 43, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", null, null, null, null, "", null, false, null, null, 1 },
                    { 44, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", null, null, null, null, "", null, false, null, null, 1 },
                    { 45, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", null, null, null, null, "", null, false, null, null, 1 },
                    { 46, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", null, null, null, null, "", null, false, null, null, 1 },
                    { 47, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", null, null, null, null, "", null, false, null, null, 1 },
                    { 48, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", null, null, null, null, "", null, false, null, null, 1 },
                    { 49, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", null, null, null, null, "", null, false, null, null, 1 },
                    { 50, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", null, null, null, null, "", null, false, null, null, 1 },
                    { 51, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", null, null, null, null, "", null, false, null, null, 1 },
                    { 52, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", null, null, null, null, "", null, false, null, null, 1 },
                    { 53, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", null, null, null, null, "", null, false, null, null, 1 },
                    { 54, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", null, null, null, null, "", null, false, null, null, 1 },
                    { 55, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", null, null, null, null, "", null, false, null, null, 1 },
                    { 56, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", null, null, null, null, "", null, false, null, null, 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UsersPermissions",
                keyColumns: new[] { "PermissionId", "UserId" },
                keyValues: new object[] { 1, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7") });

            migrationBuilder.DeleteData(
                table: "UsersPermissions",
                keyColumns: new[] { "PermissionId", "UserId" },
                keyValues: new object[] { 2, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7") });

            migrationBuilder.DeleteData(
                table: "UsersPermissions",
                keyColumns: new[] { "PermissionId", "UserId" },
                keyValues: new object[] { 3, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7") });

            migrationBuilder.DeleteData(
                table: "UsersPermissions",
                keyColumns: new[] { "PermissionId", "UserId" },
                keyValues: new object[] { 4, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7") });

            migrationBuilder.DeleteData(
                table: "UsersPermissions",
                keyColumns: new[] { "PermissionId", "UserId" },
                keyValues: new object[] { 5, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7") });

            migrationBuilder.DeleteData(
                table: "UsersPermissions",
                keyColumns: new[] { "PermissionId", "UserId" },
                keyValues: new object[] { 6, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7") });

            migrationBuilder.DeleteData(
                table: "UsersPermissions",
                keyColumns: new[] { "PermissionId", "UserId" },
                keyValues: new object[] { 7, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7") });

            migrationBuilder.DeleteData(
                table: "UsersPermissions",
                keyColumns: new[] { "PermissionId", "UserId" },
                keyValues: new object[] { 8, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7") });

            migrationBuilder.DeleteData(
                table: "UsersPermissions",
                keyColumns: new[] { "PermissionId", "UserId" },
                keyValues: new object[] { 9, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7") });

            migrationBuilder.DeleteData(
                table: "UsersPermissions",
                keyColumns: new[] { "PermissionId", "UserId" },
                keyValues: new object[] { 10, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7") });

            migrationBuilder.DeleteData(
                table: "UsersPermissions",
                keyColumns: new[] { "PermissionId", "UserId" },
                keyValues: new object[] { 11, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7") });

            migrationBuilder.DeleteData(
                table: "UsersPermissions",
                keyColumns: new[] { "PermissionId", "UserId" },
                keyValues: new object[] { 12, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7") });

            migrationBuilder.DeleteData(
                table: "UsersPermissions",
                keyColumns: new[] { "PermissionId", "UserId" },
                keyValues: new object[] { 13, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7") });

            migrationBuilder.DeleteData(
                table: "UsersPermissions",
                keyColumns: new[] { "PermissionId", "UserId" },
                keyValues: new object[] { 14, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7") });

            migrationBuilder.DeleteData(
                table: "UsersPermissions",
                keyColumns: new[] { "PermissionId", "UserId" },
                keyValues: new object[] { 15, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7") });

            migrationBuilder.DeleteData(
                table: "UsersPermissions",
                keyColumns: new[] { "PermissionId", "UserId" },
                keyValues: new object[] { 16, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7") });

            migrationBuilder.DeleteData(
                table: "UsersPermissions",
                keyColumns: new[] { "PermissionId", "UserId" },
                keyValues: new object[] { 17, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7") });

            migrationBuilder.DeleteData(
                table: "UsersPermissions",
                keyColumns: new[] { "PermissionId", "UserId" },
                keyValues: new object[] { 18, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7") });

            migrationBuilder.DeleteData(
                table: "UsersPermissions",
                keyColumns: new[] { "PermissionId", "UserId" },
                keyValues: new object[] { 19, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7") });

            migrationBuilder.DeleteData(
                table: "UsersPermissions",
                keyColumns: new[] { "PermissionId", "UserId" },
                keyValues: new object[] { 20, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7") });

            migrationBuilder.DeleteData(
                table: "UsersPermissions",
                keyColumns: new[] { "PermissionId", "UserId" },
                keyValues: new object[] { 21, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7") });

            migrationBuilder.DeleteData(
                table: "UsersPermissions",
                keyColumns: new[] { "PermissionId", "UserId" },
                keyValues: new object[] { 22, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7") });

            migrationBuilder.DeleteData(
                table: "UsersPermissions",
                keyColumns: new[] { "PermissionId", "UserId" },
                keyValues: new object[] { 23, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7") });

            migrationBuilder.DeleteData(
                table: "UsersPermissions",
                keyColumns: new[] { "PermissionId", "UserId" },
                keyValues: new object[] { 24, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7") });

            migrationBuilder.DeleteData(
                table: "UsersPermissions",
                keyColumns: new[] { "PermissionId", "UserId" },
                keyValues: new object[] { 25, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7") });

            migrationBuilder.DeleteData(
                table: "UsersPermissions",
                keyColumns: new[] { "PermissionId", "UserId" },
                keyValues: new object[] { 26, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7") });

            migrationBuilder.DeleteData(
                table: "UsersPermissions",
                keyColumns: new[] { "PermissionId", "UserId" },
                keyValues: new object[] { 27, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7") });

            migrationBuilder.DeleteData(
                table: "UsersPermissions",
                keyColumns: new[] { "PermissionId", "UserId" },
                keyValues: new object[] { 28, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7") });

            migrationBuilder.DeleteData(
                table: "UsersPermissions",
                keyColumns: new[] { "PermissionId", "UserId" },
                keyValues: new object[] { 29, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7") });

            migrationBuilder.DeleteData(
                table: "UsersPermissions",
                keyColumns: new[] { "PermissionId", "UserId" },
                keyValues: new object[] { 30, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7") });

            migrationBuilder.DeleteData(
                table: "UsersPermissions",
                keyColumns: new[] { "PermissionId", "UserId" },
                keyValues: new object[] { 31, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7") });

            migrationBuilder.DeleteData(
                table: "UsersPermissions",
                keyColumns: new[] { "PermissionId", "UserId" },
                keyValues: new object[] { 32, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7") });

            migrationBuilder.DeleteData(
                table: "UsersPermissions",
                keyColumns: new[] { "PermissionId", "UserId" },
                keyValues: new object[] { 33, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7") });

            migrationBuilder.DeleteData(
                table: "UsersPermissions",
                keyColumns: new[] { "PermissionId", "UserId" },
                keyValues: new object[] { 34, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7") });

            migrationBuilder.DeleteData(
                table: "UsersPermissions",
                keyColumns: new[] { "PermissionId", "UserId" },
                keyValues: new object[] { 35, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7") });

            migrationBuilder.DeleteData(
                table: "UsersPermissions",
                keyColumns: new[] { "PermissionId", "UserId" },
                keyValues: new object[] { 36, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7") });

            migrationBuilder.DeleteData(
                table: "UsersPermissions",
                keyColumns: new[] { "PermissionId", "UserId" },
                keyValues: new object[] { 37, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7") });

            migrationBuilder.DeleteData(
                table: "UsersPermissions",
                keyColumns: new[] { "PermissionId", "UserId" },
                keyValues: new object[] { 38, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7") });

            migrationBuilder.DeleteData(
                table: "UsersPermissions",
                keyColumns: new[] { "PermissionId", "UserId" },
                keyValues: new object[] { 39, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7") });

            migrationBuilder.DeleteData(
                table: "UsersPermissions",
                keyColumns: new[] { "PermissionId", "UserId" },
                keyValues: new object[] { 40, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7") });

            migrationBuilder.DeleteData(
                table: "UsersPermissions",
                keyColumns: new[] { "PermissionId", "UserId" },
                keyValues: new object[] { 41, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7") });

            migrationBuilder.DeleteData(
                table: "UsersPermissions",
                keyColumns: new[] { "PermissionId", "UserId" },
                keyValues: new object[] { 42, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7") });

            migrationBuilder.DeleteData(
                table: "UsersPermissions",
                keyColumns: new[] { "PermissionId", "UserId" },
                keyValues: new object[] { 43, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7") });

            migrationBuilder.DeleteData(
                table: "UsersPermissions",
                keyColumns: new[] { "PermissionId", "UserId" },
                keyValues: new object[] { 44, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7") });

            migrationBuilder.DeleteData(
                table: "UsersPermissions",
                keyColumns: new[] { "PermissionId", "UserId" },
                keyValues: new object[] { 45, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7") });

            migrationBuilder.DeleteData(
                table: "UsersPermissions",
                keyColumns: new[] { "PermissionId", "UserId" },
                keyValues: new object[] { 46, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7") });

            migrationBuilder.DeleteData(
                table: "UsersPermissions",
                keyColumns: new[] { "PermissionId", "UserId" },
                keyValues: new object[] { 47, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7") });

            migrationBuilder.DeleteData(
                table: "UsersPermissions",
                keyColumns: new[] { "PermissionId", "UserId" },
                keyValues: new object[] { 48, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7") });

            migrationBuilder.DeleteData(
                table: "UsersPermissions",
                keyColumns: new[] { "PermissionId", "UserId" },
                keyValues: new object[] { 49, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7") });

            migrationBuilder.DeleteData(
                table: "UsersPermissions",
                keyColumns: new[] { "PermissionId", "UserId" },
                keyValues: new object[] { 50, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7") });

            migrationBuilder.DeleteData(
                table: "UsersPermissions",
                keyColumns: new[] { "PermissionId", "UserId" },
                keyValues: new object[] { 51, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7") });

            migrationBuilder.DeleteData(
                table: "UsersPermissions",
                keyColumns: new[] { "PermissionId", "UserId" },
                keyValues: new object[] { 52, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7") });

            migrationBuilder.DeleteData(
                table: "UsersPermissions",
                keyColumns: new[] { "PermissionId", "UserId" },
                keyValues: new object[] { 53, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7") });

            migrationBuilder.DeleteData(
                table: "UsersPermissions",
                keyColumns: new[] { "PermissionId", "UserId" },
                keyValues: new object[] { 54, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7") });

            migrationBuilder.DeleteData(
                table: "UsersPermissions",
                keyColumns: new[] { "PermissionId", "UserId" },
                keyValues: new object[] { 55, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7") });

            migrationBuilder.DeleteData(
                table: "UsersPermissions",
                keyColumns: new[] { "PermissionId", "UserId" },
                keyValues: new object[] { 56, new Guid("a9923638-8866-4a89-a9fe-9cf329cfc8f7") });
        }
    }
}
