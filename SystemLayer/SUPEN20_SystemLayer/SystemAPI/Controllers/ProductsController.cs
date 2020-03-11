using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SUPEN20DB.DbContexts;
using SUPEN20DB.Entites;

namespace SystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private SUPEN20DbContext _context;

        public ProductsController(SUPEN20DbContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            try
            {
                // Returns all the products from the database
                return await _context.Products.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return NotFound();
            }
        }

        // GET: api/Products/5 
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(Guid id)
        {
            try
            {
                // Saves the product that matches the id into a variable
                var product = await _context.Products.FindAsync(id);

                // Makes sure the variable is not null
                if (product == null)
                {
                    return NotFound();
                }

                // Returns the product
                return product;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return NotFound();
            }
        }

        // PUT: api/Products/5 
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(Guid id, Product product)
        {
            try
            {
                // Check if the Id entered is the same Id as the object that has been sent
                if (!(id.ToString().Equals(product.ProductId.ToString())))
                {
                    return BadRequest();
                }

                // "Modified: the entity is being tracked by the context and exists in the database, and some or all of its property values have been modified." Source: https://docs.microsoft.com/en-us/ef/ef6/saving/change-tracking/entity-state
                _context.Entry(product).State = EntityState.Modified;
                try
                {
                    // Saves changes to the database
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductsExist(id))
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
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500);
            }
            
        }

        // POST: api/Products 
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            try
            {
                // Adds the new product to the database
                _context.Products.Add(product);

                // Saves changes to the database
                await _context.SaveChangesAsync();

                // Returns the object created
                return CreatedAtAction("GetProduct", new { id = product.ProductId }, product);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500);
            }
        }

        // DELETE: api/Products/5 
        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> DeleteProduct(Guid id)
        {
            try
            {
                // Looks for the specified product and saves it into a variable
                var product = await _context.Products.FindAsync(id);

                // Makes sure tha variable is not null
                if (product == null)
                {
                    return NotFound();
                }

                // Removed the product from the database 
                _context.Products.Remove(product);

                // Save changes to the database
                await _context.SaveChangesAsync();

                // Returns success
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500);
            }
        }

        // A method that checks if the specific item exist in the database by looking for the id sent as a parameter
        private bool ProductsExist(Guid id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}