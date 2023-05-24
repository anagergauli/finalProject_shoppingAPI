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
    public class ordersController : ControllerBase
    {
        private readonly shoppingAPIContext _context;

        public ordersController(shoppingAPIContext context)
        {
            _context = context;
        }

        // GET: api/orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<order>>> Getorders()
        {
          if (_context.orders == null)
          {
              return NotFound();
          }
            return await _context.orders.ToListAsync();
        }

        // GET: api/orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<order>> Getorder(int id)
        {
          if (_context.orders == null)
          {
              return NotFound();
          }
            var order = await _context.orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        // PUT: api/orders/5
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Putorder(int id, order order)
        {
            if (id != order.orderID)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!orderExists(id))
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

        // POST: api/orders
      
        [HttpPost]
        public async Task<ActionResult<order>> Postorder(order order)
        {
          if (_context.orders == null)
          {
              return Problem("Entity set 'shoppingAPIContext.orders'  is null.");
          }
            _context.orders.Add(order);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (orderExists(order.orderID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("Getorder", new { id = order.orderID }, order);
        }

        // DELETE: api/orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deleteorder(int id)
        {
            if (_context.orders == null)
            {
                return NotFound();
            }
            var order = await _context.orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool orderExists(int id)
        {
            return (_context.orders?.Any(e => e.orderID == id)).GetValueOrDefault();
        }
    }
}
