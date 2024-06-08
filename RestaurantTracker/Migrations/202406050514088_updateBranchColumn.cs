namespace RestaurantTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateBranchColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Branches", "Rating", c => c.Decimal(nullable: false, precision: 18, scale: 1));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Branches", "Rating");
        }
    }
}
