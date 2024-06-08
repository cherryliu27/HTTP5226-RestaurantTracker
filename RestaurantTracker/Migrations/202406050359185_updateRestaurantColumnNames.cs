namespace RestaurantTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateRestaurantColumnNames : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Restaurants", "Cuisine", c => c.String());
            AddColumn("dbo.Restaurants", "Budget", c => c.String());
            DropColumn("dbo.Restaurants", "RestaurantCuisine");
            DropColumn("dbo.Restaurants", "RestaurantBudget");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Restaurants", "RestaurantBudget", c => c.String());
            AddColumn("dbo.Restaurants", "RestaurantCuisine", c => c.String());
            DropColumn("dbo.Restaurants", "Budget");
            DropColumn("dbo.Restaurants", "Cuisine");
        }
    }
}
