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
    public class cartsController : ControllerBase
    {
        private readonly shoppingAPIContext _context;

        public cartsController(shoppingAPIContext context)
        {
            _context = context;
        }

        // GET: api/carts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<cart>>> Getcarts()
        {
          if (_context.carts == null)
          {
              return NotFound();
          }
            return await _context.carts.ToListAsync();
        }

        // GET: api/carts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<cart>> Getcart(int id)
        {
          if (_context.carts == null)
          {
              return NotFound();
          }
            var cart = await _context.carts.FindAsync(id);

            if (cart == null)
            {
                return NotFound();
            }

            return cart;
        }

        // PUT: api/carts/5
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Putcart(int id, cart cart)
        {
            if (id != cart.cartID)
            {
                return BadRequest();
            }

            _context.Entry(cart).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!cartExists(id))
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

        // POST: api/carts
        
        [HttpPost]
        public async Task<ActionResult<cart>> Postcart(cart cart)
        {
          if (_context.carts == null)
          {
              return Problem("Entity set 'shoppingAPIContext.carts'  is null.");
          }
            _context.carts.Add(cart);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (cartExists(cart.cartID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("Getcart", new { id = cart.cartID }, cart);
        }

        // DELETE: api/carts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletecart(int id)
        {
            if (_context.carts == null)
            {
                return NotFound();
            }
            var cart = await _context.carts.FindAsync(id);
            if (cart == null)
            {
                return NotFound();
            }

            _context.carts.Remove(cart);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool cartExists(int id)
        {
            return (_context.carts?.Any(e => e.cartID == id)).GetValueOrDefault();
        }
    }
}
