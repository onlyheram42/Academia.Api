using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Academia.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRoleToString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop the check constraint first
            migrationBuilder.DropCheckConstraint(
                name: "CK_Role_Users",
                table: "Users");

            // Now alter the column from int to string
            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "Student",
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 0);

            // Recreate the check constraint with the correct values
            migrationBuilder.Sql(
                @"ALTER TABLE Users 
                  ADD CONSTRAINT CK_Role_Users 
                  CHECK (Role IN ('Student', 'Admin', 'Teacher'))");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop the check constraint
            migrationBuilder.DropCheckConstraint(
                name: "CK_Role_Users",
                table: "Users");

            // Revert column back to int
            migrationBuilder.AlterColumn<int>(
                name: "Role",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "Student");

            // Recreate the original check constraint
            migrationBuilder.Sql(
                @"ALTER TABLE Users 
                  ADD CONSTRAINT CK_Role_Users 
                  CHECK (Role IN ('Student', 'Admin', 'Teacher'))");
        }
    }
}
