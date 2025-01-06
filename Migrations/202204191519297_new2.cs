namespace QuanLyKhachSan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rooms", "view", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Rooms", "view");
        }
    }
}
