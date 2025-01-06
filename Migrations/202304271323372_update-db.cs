namespace QuanLyKhachSan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedb : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rooms", "numberChildren", c => c.Int(nullable: false));
            AddColumn("dbo.Rooms", "numberAdult", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Rooms", "numberAdult");
            DropColumn("dbo.Rooms", "numberChildren");
        }
    }
}
