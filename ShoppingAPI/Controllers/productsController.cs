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
    public class productsController : ControllerBase
    {
        private readonly shoppingAPIContext _context;

        public productsController(shoppingAPIContext context)
        {
            _context = context;
        }

        // GET: api/products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<product>>> Getproducts()
        {
          if (_context.products == null)
          {
              return NotFound();
          }
            return await _context.products.ToListAsync();
        }

        // GET: api/products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<product>> Getproduct(int id)
        {
          if (_context.products == null)
          {
              return NotFound();
          }
            var product = await _context.products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/products/5
      
        [HttpPut("{id}")]
        public async Task<IActionResult> Putproduct(int id, product product)
        {
            if (id != product.productID)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!productExists(id))
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

        // POST: api/products
     
        [HttpPost]
        public async Task<ActionResult<product>> Postproduct(product product)
        {
          if (_context.products == null)
          {
              return Problem("Entity set 'shoppingAPIContext.products'  is null.");
          }
            _context.products.Add(product);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (productExists(product.productID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("Getproduct", new { id = product.productID }, product);
        }

        // DELETE: api/products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deleteproduct(int id)
        {
            if (_context.products == null)
            {
                return NotFound();
            }
            var product = await _context.products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool productExists(int id)
        {
            return (_context.products?.Any(e => e.productID == id)).GetValueOrDefault();
        }
    }
}
