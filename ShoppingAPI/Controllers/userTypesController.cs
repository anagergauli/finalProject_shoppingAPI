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
    public class userTypesController : ControllerBase
    {
        private readonly shoppingAPIContext _context;

        public userTypesController(shoppingAPIContext context)
        {
            _context = context;
        }

        // GET: api/userTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<userType>>> GetuserTypes()
        {
          if (_context.userTypes == null)
          {
              return NotFound();
          }
            return await _context.userTypes.ToListAsync();
        }

        // GET: api/userTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<userType>> GetuserType(int id)
        {
          if (_context.userTypes == null)
          {
              return NotFound();
          }
            var userType = await _context.userTypes.FindAsync(id);

            if (userType == null)
            {
                return NotFound();
            }

            return userType;
        }

        // PUT: api/userTypes/5
      
        [HttpPut("{id}")]
        public async Task<IActionResult> PutuserType(int id, userType userType)
        {
            if (id != userType.typeID)
            {
                return BadRequest();
            }

            _context.Entry(userType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!userTypeExists(id))
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

        // POST: api/userTypes
       
        [HttpPost]
        public async Task<ActionResult<userType>> PostuserType(userType userType)
        {
          if (_context.userTypes == null)
          {
              return Problem("Entity set 'shoppingAPIContext.userTypes'  is null.");
          }
            _context.userTypes.Add(userType);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (userTypeExists(userType.typeID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetuserType", new { id = userType.typeID }, userType);
        }

        // DELETE: api/userTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteuserType(int id)
        {
            if (_context.userTypes == null)
            {
                return NotFound();
            }
            var userType = await _context.userTypes.FindAsync(id);
            if (userType == null)
            {
                return NotFound();
            }

            _context.userTypes.Remove(userType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool userTypeExists(int id)
        {
            return (_context.userTypes?.Any(e => e.typeID == id)).GetValueOrDefault();
        }
    }
}
