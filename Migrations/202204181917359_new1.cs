namespace QuanLyKhachSan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "email", c => c.String(nullable: false, maxLength: 255));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "email");
        }
    }
}
