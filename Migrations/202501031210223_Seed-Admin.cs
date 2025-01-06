namespace QuanLyKhachSan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedAdmin : DbMigration
    {
        public override void Up()
        {
       //     migrationBuilder.InsertData(
       //table: "Users",
       //columns: new[] { "fullName", "userName", "email", "password", "phoneNumber", "address", "gender", "status", "idRole" },
       //values: new object[]
       //{
       //     "Quản trị viên",
       //     "admin",
       //     "admin@gmail.com",
       //     "25f9e794323b453885f5181f1b624d0b", // Mã hóa MD5 cho mật khẩu "123456789"
       //     "0394073758",
       //     "Hà Nội",
       //     "Nam",
       //     1,
       //     1
       //});
        }
        
        public override void Down()
        {
        }
    }
}
