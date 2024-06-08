using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace RestaurantTracker.Models
{
    public class Restaurant
    {
        [Key]
        public int RestaurantId { get; set; }
        public string RestaurantName { get; set; }
        public string RestaurantType { get; set; }
        public string Cuisine {  get; set; }
        public string Budget {  get; set; }

    }

    //Data Transfer Object (DTO)
    public class RestaurantDto
    {
        public int RestaurantId { get; set; }
        public string RestaurantName { get; set; }
        public string RestaurantType { get; set; }
        public string Cuisine { get; set; }
        public string Budget { get; set; }
    }
}