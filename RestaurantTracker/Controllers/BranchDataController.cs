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

namespace RestaurantTracker.Controllers
{
    public class BranchDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        /// <summary>
        /// Returns a list of branches
        /// </summary>
        /// <returns>An array of branches
        /// </returns>
        /// <example>
        /// //GET: /api/BranchData/ListBranches -> [{"BranchId":"1", "Status": "Visited", "Rating": "4.00"}, {"BranchId":"2", "Status": "Not Visited", "Rating": "4.50"}]
        /// </example>

        [HttpGet]
        [Route("api/BranchData/ListBranches")]
        public IEnumerable<BranchDto> ListBranches()

        {
            List<Branch> Branches = db.Branches.ToList();
            List<BranchDto> BranchDtos = new List<BranchDto>();

            foreach (Branch Branch in Branches)
            {
                BranchDto BranchDto = new BranchDto();

                BranchDto.BranchId = Branch.BranchId;
                BranchDto.RestaurantId = Branch.RestaurantId;
                BranchDto.Status = Branch.Status;
                BranchDto.Review = Branch.Review;
                BranchDto.Location = Branch.Location;
                BranchDto.Address = Branch.Address;
                BranchDto.Rating = Branch.Rating;
                BranchDto.RestaurantName = Branch.Restaurant.RestaurantName;
                BranchDto.RestaurantType = Branch.Restaurant.RestaurantType;
                BranchDto.Cuisine = Branch.Restaurant.Cuisine;
                BranchDto.Budget = Branch.Restaurant.Budget;

                BranchDtos.Add(BranchDto);
            }
            return BranchDtos;
        }



        /// <summary>
        /// Returns information of a branch of the restaurant
        /// </summary>
        /// <param name="id">restaurant branch id</param>
        /// <returns>
        /// singular restaurant branch of the corresponding branch id</returns>
        /// <example>
        /// //GET: /api/BranchData/Listbranches/1 -> [{"BranchtId": "1", "RestaurantId": "1"}]
        /// </example>
        [HttpGet]
        [Route("api/BranchData/FindBranch/{id}")]
        public BranchDto FindBranch(int id)
        {
            Branch Branch = db.Branches.Find(id);

            BranchDto BranchDto = new BranchDto();

            BranchDto.BranchId = Branch.BranchId;
            BranchDto.RestaurantId = Branch.RestaurantId;
            BranchDto.Status = Branch.Status;
            BranchDto.Review = Branch.Review;
            BranchDto.Location = Branch.Location;
            BranchDto.Address = Branch.Address;
            BranchDto.Rating = Branch.Rating;
            BranchDto.RestaurantName = Branch.Restaurant.RestaurantName;
            BranchDto.RestaurantType = Branch.Restaurant.RestaurantType;
            BranchDto.Cuisine = Branch.Restaurant.Cuisine;
            BranchDto.Budget = Branch.Restaurant.Budget;

            return BranchDto;

        }



        // POST: api/BranchData/UpdateBranch/2
        [ResponseType(typeof(void))]
        [HttpPost]
        [Route("api/branchdata/updatebranch/{id}")]
        public IHttpActionResult UpdateBranch(int id, Branch Branch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Branch.BranchId)
            {

                return BadRequest();
            }

            db.Entry(Branch).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!BranchExists(id))
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
        private bool BranchExists(int id)
        {
            return db.Branches.Count(e => e.BranchId == id) > 0;
        }




        // POST: api/BranchData/AddBranch
        [ResponseType(typeof(Branch))]
        [HttpPost]
        [Route("api/branchdata/addbranch")]

        public IHttpActionResult AddBranch(Branch Branch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Branches.Add(Branch);
            db.SaveChanges();

            return Ok();
        }


        /// <summary>
        /// Deletes a branch from the system by it's ID.
        /// </summary>
        /// <param name="id">The primary key of the branch</param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <example>
        /// POST: api/BranchData/DeleteBranch/5
        /// FORM DATA: (empty)
        /// </example>
        [ResponseType(typeof(Branch))]
        [HttpPost]
        [Route("api/branchdata/deletebranch/{id}")]

        public IHttpActionResult DeleteBranch(int id)
        {
            Branch Branch = db.Branches.Find(id);
            if (Branch == null)
            {
                return NotFound();
            }

            db.Branches.Remove(Branch);
            db.SaveChanges();

            return Ok();
        }

        //Filtering
        // GET: api/BranchData/ListBranchesByRestaurant/1
        [HttpGet]
        [Route("api/BranchData/ListBranchesByRestaurant/{restaurantId}")]
        public IEnumerable<BranchDto> ListBranchesByRestaurant(int restaurantId)
        {
            List<Branch> branches = db.Branches.Where(b => b.RestaurantId == restaurantId).ToList();
            List<BranchDto> branchDtos = new List<BranchDto>();

            foreach (Branch branch in branches)
            {
                BranchDto branchDto = new BranchDto
                {
                    BranchId = branch.BranchId,
                    RestaurantId = branch.RestaurantId,
                    Status = branch.Status,
                    Review = branch.Review,
                    Location = branch.Location,
                    Address = branch.Address,
                    Rating = branch.Rating,
                    RestaurantName = branch.Restaurant.RestaurantName,
                    RestaurantType = branch.Restaurant.RestaurantType,
                    Cuisine = branch.Restaurant.Cuisine,
                    Budget = branch.Restaurant.Budget
                };
                branchDtos.Add(branchDto);
            }

            return branchDtos;
        }
    }
}
