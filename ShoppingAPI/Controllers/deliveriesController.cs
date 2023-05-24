using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingAPI.Models;

namespace ShoppingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class deliveriesController : ControllerBase
    {
        private readonly shoppingAPIContext _context;

        public deliveriesController(shoppingAPIContext context)
        {
            _context = context;
        }

        // GET: api/deliveries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<delivery>>> Getdeliveries()
        {
          if (_context.deliveries == null)
          {
              return NotFound();
          }
            return await _context.deliveries.ToListAsync();
        }

        // GET: api/deliveries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<delivery>> Getdelivery(int id)
        {
          if (_context.deliveries == null)
          {
              return NotFound();
          }
            var delivery = await _context.deliveries.FindAsync(id);

            if (delivery == null)
            {
                return NotFound();
            }

            return delivery;
        }

        // PUT: api/deliveries/5
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Putdelivery(int id, delivery delivery)
        {
            if (id != delivery.deliveryID)
            {
                return BadRequest();
            }

            _context.Entry(delivery).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!deliveryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/deliveries
       
        [HttpPost]
        public async Task<ActionResult<delivery>> Postdelivery(delivery delivery)
        {
          if (_context.deliveries == null)
          {
              return Problem("Entity set 'shoppingAPIContext.deliveries'  is null.");
          }
            _context.deliveries.Add(delivery);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (deliveryExists(delivery.deliveryID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("Getdelivery", new { id = delivery.deliveryID }, delivery);
        }

        // DELETE: api/deliveries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletedelivery(int id)
        {
            if (_context.deliveries == null)
            {
                return NotFound();
            }
            var delivery = await _context.deliveries.FindAsync(id);
            if (delivery == null)
            {
                return NotFound();
            }

            _context.deliveries.Remove(delivery);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool deliveryExists(int id)
        {
            return (_context.deliveries?.Any(e => e.deliveryID == id)).GetValueOrDefault();
        }
    }
}
