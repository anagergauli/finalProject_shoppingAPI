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
    public class categoriesController : ControllerBase
    {
        private readonly shoppingAPIContext _context;

        public categoriesController(shoppingAPIContext context)
        {
            _context = context;
        }

        // GET: api/categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<category>>> Getcategories()
        {
          if (_context.categories == null)
          {
              return NotFound();
          }
            return await _context.categories.ToListAsync();
        }

        // GET: api/categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<category>> Getcategory(int id)
        {
          if (_context.categories == null)
          {
              return NotFound();
          }
            var category = await _context.categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        // PUT: api/categories/5
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Putcategory(int id, category category)
        {
            if (id != category.categoryID)
            {
                return BadRequest();
            }

            _context.Entry(category).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!categoryExists(id))
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

        // POST: api/categories
       
        [HttpPost]
        public async Task<ActionResult<category>> Postcategory(category category)
        {
          if (_context.categories == null)
          {
              return Problem("Entity set 'shoppingAPIContext.categories'  is null.");
          }
            _context.categories.Add(category);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (categoryExists(category.categoryID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("Getcategory", new { id = category.categoryID }, category);
        }

        // DELETE: api/categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletecategory(int id)
        {
            if (_context.categories == null)
            {
                return NotFound();
            }
            var category = await _context.categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _context.categories.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool categoryExists(int id)
        {
            return (_context.categories?.Any(e => e.categoryID == id)).GetValueOrDefault();
        }
    }
}
