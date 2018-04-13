using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlackBaudDonorsWebAPI.Models;
using Microsoft.AspNetCore.Mvc;


namespace BlackBaudDonorsWebAPI.Controllers
{
    [Route("api/[controller]")]
    public class DonorsController : Controller
    {
        private readonly BlackBaudDonorsContext _context;

        public DonorsController(BlackBaudDonorsContext context)
        {
            _context = context;

            if (_context.Donors.Count() == 0)
            {
                _context.Donors.Add(new Donor { FirstName = "Spencer", LastName = "Oakes", Address = "17A Princess St", City = "Charleston", State = "SC", Zip = 29401 });
                _context.Donors.Add(new Donor { FirstName = "Melinda", LastName = "Gates", Address = "1234 Donation Dr", City = "Seattle", State = "WA", Zip = 98101 });
                _context.Donors.Add(new Donor { FirstName = "Oprah", LastName = "Winfrey", Address = "9876 Charity St", City = "Chicago", State = "IL", Zip = 60007 });
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public IEnumerable<Donor> GetDonors()
        {
            var city = (string)HttpContext.Request.Query["city"];
            var state = (string)HttpContext.Request.Query["state"];
            var zip = (string)HttpContext.Request.Query["zip"];

            if (city != null)
                return _context.Donors.Where(q => q.City.ToLower() == city.ToLower()).ToList();

            if (state != null)
                return _context.Donors.Where(q => q.State.ToLower() == state.ToLower()).ToList();

            if (zip != null)
                return _context.Donors.Where(q => q.Zip == Convert.ToInt32(zip)).ToList();

            return _context.Donors.ToList();     
        }

        [HttpGet("{id}", Name = "GetDonor")]
        public IActionResult GetDonorById(int id)
        {
            var donor = _context.Donors.FirstOrDefault(q => q.Id == id);

            if (donor == null)
                return NotFound();

            return new ObjectResult(donor);
        }

        [HttpPost]
        public IActionResult AddDonor([FromBody] Donor donor)
        {
            if (donor == null)
                return BadRequest();

            _context.Donors.Add(donor);
            _context.SaveChanges();

            return CreatedAtRoute("GetDonor", new { id = donor.Id }, donor);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteDonor(int id)
        {
            var donor = _context.Donors.FirstOrDefault(q => q.Id == id);

            if (donor == null)
                return NotFound();

            _context.Donors.Remove(donor);
            _context.SaveChanges();

            return new NoContentResult();
        }
    }
}
