using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DALlab3.Entities;

namespace Lab3.ApiContollers
{
    [Produces("application/json")]
    [Route("api/BusinessEntities")]
    public class BusinessEntitiesController : Controller
    {
        private readonly AdventureWorks2014Context _context;

        public BusinessEntitiesController(AdventureWorks2014Context context)
        {
            _context = context;
        }

        // GET: api/BusinessEntities
        [HttpGet]
        public IEnumerable<BusinessEntity> GetBusinessEntity()
        {
            return _context.BusinessEntity;
        }

        // GET: api/BusinessEntities/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBusinessEntity([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var businessEntity = await _context.BusinessEntity.SingleOrDefaultAsync(m => m.BusinessEntityId == id);

            if (businessEntity == null)
            {
                return NotFound();
            }

            return Ok(businessEntity);
        }

        // PUT: api/BusinessEntities/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBusinessEntity([FromRoute] int id, [FromBody] BusinessEntity businessEntity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != businessEntity.BusinessEntityId)
            {
                return BadRequest();
            }

            _context.Entry(businessEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BusinessEntityExists(id))
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

        // POST: api/BusinessEntities
        [HttpPost]
        public async Task<IActionResult> PostBusinessEntity([FromBody] BusinessEntity businessEntity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.BusinessEntity.Add(businessEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBusinessEntity", new { id = businessEntity.BusinessEntityId }, businessEntity);
        }

        // DELETE: api/BusinessEntities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBusinessEntity([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var businessEntity = await _context.BusinessEntity.SingleOrDefaultAsync(m => m.BusinessEntityId == id);
            if (businessEntity == null)
            {
                return NotFound();
            }

            _context.BusinessEntity.Remove(businessEntity);
            await _context.SaveChangesAsync();

            return Ok(businessEntity);
        }

        private bool BusinessEntityExists(int id)
        {
            return _context.BusinessEntity.Any(e => e.BusinessEntityId == id);
        }
    }
}