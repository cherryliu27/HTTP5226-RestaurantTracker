using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using RestaurantTracker.Models;
using System.Diagnostics;

namespace RestaurantTracker.Controllers
{
    public class RestaurantDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        /// <summary>
        /// Returns a list of restaurants in the system
        /// </summary>
        /// <returns>
        /// An Array of restaurants
        /// </returns>
        /// <example>
        /// //GET: /api/RestaurantData/ListRestaurants -> [{"RestaurantId": "1", "RestaurantName": "Miss Lin Cafe"}, {"RestaurantId": "2", "RestaurantName": "Daldongnae Korean BBQ"}]
        /// </example>
        [HttpGet]
        [Route("api/RestaurantData/ListRestaurants")]
        public IEnumerable<RestaurantDto> ListRestaurants(string SearchKey = null)
        {
            List<Restaurant> Restaurants;

            if (!string.IsNullOrEmpty(SearchKey))
            {
                Restaurants = db.Restaurants.Where(x => x.RestaurantName.Contains(SearchKey)).ToList();
            }
            else
            {
                Restaurants = db.Restaurants.ToList();

            }

            List<RestaurantDto> RestaurantDtos = new List<RestaurantDto>();

            foreach (Restaurant Restaurant in Restaurants)
            {
                RestaurantDto RestaurantDto = new RestaurantDto();

                RestaurantDto.RestaurantId = Restaurant.RestaurantId;
                RestaurantDto.RestaurantName = Restaurant.RestaurantName;
                RestaurantDto.RestaurantType = Restaurant.RestaurantType;
                RestaurantDto.Cuisine = Restaurant.Cuisine;
                RestaurantDto.Budget = Restaurant.Budget;

                RestaurantDtos.Add(RestaurantDto);
            }
            return RestaurantDtos;
        }



        /// <summary>
        /// Returns information of a restaurant
        /// </summary>
        /// <param name="id">Restaurant id</param>
        /// <returns>
        /// singular restaurant of the corresponding restaurant id</returns>
        /// <example>
        /// //GET: /api/RestaurantData/ListRestaurants/1 -> [{"RestaurantId": "1", "RestaurantName": "Miss Lin Cafe"}]
        /// </example>
        [HttpGet]
        [Route("api/RestaurantData/FindRestaurant/{id}")]
        public RestaurantDto FindRestaurant(int id)
        {
            Restaurant Restaurant = db.Restaurants.Find(id);

            RestaurantDto RestaurantDto = new RestaurantDto();

            RestaurantDto.RestaurantId = Restaurant.RestaurantId;
            RestaurantDto.RestaurantName = Restaurant.RestaurantName;
            RestaurantDto.RestaurantType = Restaurant.RestaurantType;
            RestaurantDto.Cuisine = Restaurant.Cuisine;
            RestaurantDto.Budget = Restaurant.Budget;

            return RestaurantDto;

        }


        // POST: api/RestaurantData/UpdateRestaurant/2
        [ResponseType(typeof(void))]
        [HttpPost]
        [Route("api/restaurantdata/updaterestaurant/{id}")]
        public IHttpActionResult UpdateRestaurant(int id, Restaurant Restaurant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Restaurant.RestaurantId)
            {

                return BadRequest();
            }

            db.Entry(Restaurant).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!RestaurantExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        //check if restaurant exists
        private bool RestaurantExists(int id)
        {
            return db.Restaurants.Count(e => e.RestaurantId == id) > 0;
        }


        // POST: api/RestaurantData/AddRestaurant
        [ResponseType(typeof(Restaurant))]
        [HttpPost]
        [Route("api/restaurantdata/addrestaurant")]

        public IHttpActionResult AddRestaurant(Restaurant Restaurant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Restaurants.Add(Restaurant);
            db.SaveChanges();

            // Return the newly created restaurant with its ID
            return CreatedAtRoute("RestaurantDetails", new { id = Restaurant.RestaurantId }, Restaurant);
        }


        /// <summary>
        /// Deletes a restaurant from the system by it's ID.
        /// </summary>
        /// <param name="id">The primary key of the restaurant</param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <example>
        /// POST: api/restaurantData/Deleterestaurant/5
        /// FORM DATA: (empty)
        /// </example>
        [ResponseType(typeof(Restaurant))]
        [HttpPost]
        [Route("api/restaurantdata/deleterestaurant/{id}")]

        public IHttpActionResult DeleteRestaurant(int id)
        {
            Restaurant Restaurant = db.Restaurants.Find(id);
            if (Restaurant == null)
            {
                return NotFound();
            }

            db.Restaurants.Remove(Restaurant);
            db.SaveChanges();

            return Ok();
        }

        /// <summary>
        /// Returns a list of restarurant and filters by restaurant name matching the search key
        /// </summary>
        /// <param name="SearchKey">User Search Key for restaurant names</param>
        /// <returns>
        /// returns a list of restaurants</returns>
        /// <example>
        /// GET: /api/RestaurantData/searchRestaurants/Miss  -> [{"RestaurantId": "1", "RestaurantName": "Miss Lin Cafe"}]
        /// </example>
        //[HttpGet]
        //[Route("api/restaurantdata/searchrestaurant/{SearchKey?}")]
        //public IEnumerable<RestaurantDto> SearchRestaurant(string SearchKey)
        //{
        //    List<Restaurant> Restaurants;

        //    if (string.IsNullOrEmpty(SearchKey))
        //    {
        //        Restaurants = db.Restaurants.ToList();
        //    }
        //    else
        //    {
        //        Restaurants = db.Restaurants.Where(x => x.RestaurantName.Contains(SearchKey)).ToList();

        //    }

        //    List<RestaurantDto> RestaurantDtos = new List<RestaurantDto>();

        //    foreach (Restaurant Restaurant in Restaurants)
        //    {
        //        RestaurantDto RestaurantDto = new RestaurantDto();


        //        RestaurantDto.RestaurantId = Restaurant.RestaurantId;
        //        RestaurantDto.RestaurantName = Restaurant.RestaurantName;
        //        RestaurantDto.RestaurantType = Restaurant.RestaurantType;
        //        RestaurantDto.Cuisine = Restaurant.Cuisine;
        //        RestaurantDto.Budget = Restaurant.Budget;


        //        RestaurantDtos.Add(RestaurantDto);
        //    }
        //    return RestaurantDtos;
        //}

    }



}
