namespace QuanLyKhachSan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedb1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RoomComments",
                c => new
                    {
                        idRoomComment = c.Int(nullable: false, identity: true),
                        idRoom = c.Int(nullable: false),
                        text = c.String(),
                        idUser = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.idRoomComment)
                .ForeignKey("dbo.Rooms", t => t.idRoom)
                .ForeignKey("dbo.Users", t => t.idUser)
                .Index(t => t.idRoom)
                .Index(t => t.idUser);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RoomComments", "idUser", "dbo.Users");
            DropForeignKey("dbo.RoomComments", "idRoom", "dbo.Rooms");
            DropIndex("dbo.RoomComments", new[] { "idUser" });
            DropIndex("dbo.RoomComments", new[] { "idRoom" });
            DropTable("dbo.RoomComments");
        }
    }
}
