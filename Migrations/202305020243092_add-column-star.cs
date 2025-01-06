namespace QuanLyKhachSan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcolumnstar : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RoomComments", "star", c => c.Int(nullable: false));
            AddColumn("dbo.RoomComments", "createdDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RoomComments", "createdDate");
            DropColumn("dbo.RoomComments", "star");
        }
    }
}
