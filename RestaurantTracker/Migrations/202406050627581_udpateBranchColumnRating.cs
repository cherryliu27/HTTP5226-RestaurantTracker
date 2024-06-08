namespace RestaurantTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class udpateBranchColumnRating : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Branches", "Rating", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Branches", "Rating", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
