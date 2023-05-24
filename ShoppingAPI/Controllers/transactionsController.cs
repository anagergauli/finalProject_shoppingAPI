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
    public class transactionsController : ControllerBase
    {
        private readonly shoppingAPIContext _context;

        public transactionsController(shoppingAPIContext context)
        {
            _context = context;
        }

        // GET: api/transactions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<transaction>>> Gettransactions()
        {
          if (_context.transactions == null)
          {
              return NotFound();
          }
            return await _context.transactions.ToListAsync();
        }

        // GET: api/transactions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<transaction>> Gettransaction(int id)
        {
          if (_context.transactions == null)
          {
              return NotFound();
          }
            var transaction = await _context.transactions.FindAsync(id);

            if (transaction == null)
            {
                return NotFound();
            }

            return transaction;
        }

        // PUT: api/transactions/5
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Puttransaction(int id, transaction transaction)
        {
            if (id != transaction.transactionID)
            {
                return BadRequest();
            }

            _context.Entry(transaction).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!transactionExists(id))
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

        // POST: api/transactions
       
        [HttpPost]
        public async Task<ActionResult<transaction>> Posttransaction(transaction transaction)
        {
          if (_context.transactions == null)
          {
              return Problem("Entity set 'shoppingAPIContext.transactions'  is null.");
          }
            _context.transactions.Add(transaction);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (transactionExists(transaction.transactionID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("Gettransaction", new { id = transaction.transactionID }, transaction);
        }

        // DELETE: api/transactions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletetransaction(int id)
        {
            if (_context.transactions == null)
            {
                return NotFound();
            }
            var transaction = await _context.transactions.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }

            _context.transactions.Remove(transaction);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool transactionExists(int id)
        {
            return (_context.transactions?.Any(e => e.transactionID == id)).GetValueOrDefault();
        }
    }
}
