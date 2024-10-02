using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartInventoryBE.Models;

namespace SmartInventoryBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentTransactionController : ControllerBase
    {
        private readonly SmartInventoryContext _context;

        public PaymentTransactionController(SmartInventoryContext context)
        {
            _context = context;
        }

        // GET: api/PaymentTransaction
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentTransaction>>> GetPaymentTransaction()
        {
            return await _context.PaymentTransaction.ToListAsync();
        }

        // GET: api/PaymentTransaction/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentTransaction>> GetPaymentTransaction(int id)
        {
            var paymentTransaction = await _context.PaymentTransaction.FindAsync(id);

            if (paymentTransaction == null)
            {
                return NotFound();
            }

            return paymentTransaction;
        }

        // PUT: api/PaymentTransaction/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaymentTransaction(int id, PaymentTransaction paymentTransaction)
        {
            if (id != paymentTransaction.PaymentTransactionId)
            {
                return BadRequest();
            }

            _context.Entry(paymentTransaction).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentTransactionExists(id))
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

        // POST: api/PaymentTransaction
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PaymentTransaction>> PostPaymentTransaction(PaymentTransaction paymentTransaction)
        {
            _context.PaymentTransaction.Add(paymentTransaction);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPaymentTransaction", new { id = paymentTransaction.PaymentTransactionId }, paymentTransaction);
        }

        // DELETE: api/PaymentTransaction/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaymentTransaction(int id)
        {
            var paymentTransaction = await _context.PaymentTransaction.FindAsync(id);
            if (paymentTransaction == null)
            {
                return NotFound();
            }

            _context.PaymentTransaction.Remove(paymentTransaction);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PaymentTransactionExists(int id)
        {
            return _context.PaymentTransaction.Any(e => e.PaymentTransactionId == id);
        }
    }
}
