namespace RestaurantTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class branch : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Branches",
                c => new
                    {
                        BranchId = c.Int(nullable: false, identity: true),
                        Status = c.String(),
                        Review = c.String(),
                        Location = c.String(),
                        Address = c.String(),
                        RestaurantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BranchId)
                .ForeignKey("dbo.Restaurants", t => t.RestaurantId, cascadeDelete: true)
                .Index(t => t.RestaurantId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Branches", "RestaurantId", "dbo.Restaurants");
            DropIndex("dbo.Branches", new[] { "RestaurantId" });
            DropTable("dbo.Branches");
        }
    }
}
