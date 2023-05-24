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
    public class userAccountsController : ControllerBase
    {
        private readonly shoppingAPIContext _context;

        public userAccountsController(shoppingAPIContext context)
        {
            _context = context;
        }

        // GET: api/userAccounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<userAccount>>> GetuserAccounts()
        {
          if (_context.userAccounts == null)
          {
              return NotFound();
          }
            return await _context.userAccounts.ToListAsync();
        }

        // GET: api/userAccounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<userAccount>> GetuserAccount(int id)
        {
          if (_context.userAccounts == null)
          {
              return NotFound();
          }
            var userAccount = await _context.userAccounts.FindAsync(id);

            if (userAccount == null)
            {
                return NotFound();
            }

            return userAccount;
        }

        // PUT: api/userAccounts/5
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutuserAccount(int id, userAccount userAccount)
        {
            if (id != userAccount.userID)
            {
                return BadRequest();
            }

            _context.Entry(userAccount).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!userAccountExists(id))
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

        // POST: api/userAccounts
       
        [HttpPost]
        public async Task<ActionResult<userAccount>> PostuserAccount(userAccount userAccount)
        {
          if (_context.userAccounts == null)
          {
              return Problem("Entity set 'shoppingAPIContext.userAccounts'  is null.");
          }
            _context.userAccounts.Add(userAccount);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (userAccountExists(userAccount.userID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetuserAccount", new { id = userAccount.userID }, userAccount);
        }

        // DELETE: api/userAccounts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteuserAccount(int id)
        {
            if (_context.userAccounts == null)
            {
                return NotFound();
            }
            var userAccount = await _context.userAccounts.FindAsync(id);
            if (userAccount == null)
            {
                return NotFound();
            }

            _context.userAccounts.Remove(userAccount);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool userAccountExists(int id)
        {
            return (_context.userAccounts?.Any(e => e.userID == id)).GetValueOrDefault();
        }
    }
}
