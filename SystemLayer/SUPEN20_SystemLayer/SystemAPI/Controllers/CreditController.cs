using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SUPEN20DB.DbContexts;
using SUPEN20DB.Entites;
using SystemAPI.Models;

namespace SystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreditController : ControllerBase
    {
        private readonly SUPEN20DbContext _context;
        private readonly IMapper _mapper;

        public CreditController(SUPEN20DbContext context, IMapper mapper)
        {
            _context = context ??
                throw new ArgumentNullException(nameof(context));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        // GET: api/Credit
        // Gets a list of all credit/customer relations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Credit>>> GetCredit()
        {
            var creditsList = await _context.Credits.ToListAsync();

            return Ok(_mapper.Map<IEnumerable<CreditDTO>>(creditsList));
        }

        // GET: api/Credit/5 
        // Returns the amount of credit for a specific customer
        [HttpGet("{customerid}")]
        public async Task<ActionResult<Credit>> GetCredit(int customerid)
        {
            var credit = await _context.Credits.FindAsync(customerid);
            if (credit == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<CreditDTO>(credit));
        }

        // PUT: api/Credit/5
        // Updates the specified credit with it's new values.
        [HttpPut("{id}")]
        public async Task<ActionResult<Credit>> UpdateCredit(Guid id, Credit creditDto)
        {
            // Check if the Id entered is the same Id as the object that has been sent
            if (!(id.ToString().Equals(creditDto.CreditId.ToString())))
            {
                return BadRequest();
            }

            var credit = _mapper.Map<Credit>(creditDto);
            credit.LastModified = DateTime.Now;
            // "Modified: the entity is being tracked by the context and exists in the database, and some or all of its property values have been modified." Source: https://docs.microsoft.com/en-us/ef/ef6/saving/change-tracking/entity-state
            _context.Entry(credit).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CreditExist(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok(_mapper.Map<CreditDTO>(credit));
        }

        // POST: api/Credit 
        [HttpPost]
        public async Task<ActionResult<Product>> CreateCredit(Credit creditDto)
        {
           // Maps the incomming DTO-object to an Entity-object.
           var credit = _mapper.Map<Credit>(creditDto);

            _context.Credits.Add(credit);
            await _context.SaveChangesAsync();

            // Sends back result in the shape of an DTO.
            return CreatedAtAction("GetCredit", new { id = credit.CustomerId }, _mapper.Map<CreditDTO>(credit));
        }

        // DELETE: api/Credit/5 
        [HttpDelete("{id}")]
        public async Task<ActionResult<Credit>> DeleteCredit(Guid id)
        {
            var credit = await _context.Credits.FindAsync(id);
            if (credit == null)
            {
                return NotFound();
            }
            _context.Credits.Remove(credit);
            await _context.SaveChangesAsync();
            return credit;
        }

        private bool CreditExist(Guid id)
        {
            return _context.Credits.Any(e => e.CreditId == id);
        }
    }
}