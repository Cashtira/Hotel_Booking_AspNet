using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCmodel.Migrations
{
    /// <inheritdoc />
    public partial class AddAdminAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Chèn tài khoản admin vào bảng AspNetUsers
            migrationBuilder.Sql(@"
        INSERT INTO AspNetUsers (Id, UserName, NormalizedUserName, Email, NormalizedEmail, EmailConfirmed, PasswordHash, SecurityStamp, ConcurrencyStamp, PhoneNumber, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnd, LockoutEnabled, AccessFailedCount)
        VALUES 
        ('c4436116-76c4-4978-8fc3-659c49f0eec5', 
        'admin', 
        'ADMIN', 
        'vuongkhiem56@gmail.com', 
        'VUONGKHIEM56@GMAIL.COM', 
        0, 
        'AQAAAAIAAYagAAAAENHNVlYitBM/2aTPffUKVPUFrj0vFFhbphl/KhkD2OV2323qps3SvrT3o4vXgoqQdw==', 
        'NACP5UCOKBMHP3KEEG3HMOHIWKEA5FUV', 
        '29cf6cd8-4b51-450d-8cfe-fa6c56adc7c5', 
        NULL, 
        0, 
        0, 
        NULL, 
        1, 
        0);
    ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
