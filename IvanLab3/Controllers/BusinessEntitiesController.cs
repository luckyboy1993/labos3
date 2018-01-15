using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DALlab3.Entities;

namespace Lab3.Controllers
{
    public class BusinessEntitiesController : Controller
    {
        private readonly AdventureWorks2014Context _context;

        public BusinessEntitiesController(AdventureWorks2014Context context)
        {
            _context = context;
        }

        // GET: BusinessEntities
        public async Task<IActionResult> Index()
        {
            return View(await _context.BusinessEntity.ToListAsync());
        }

        // GET: BusinessEntities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var businessEntity = await _context.BusinessEntity
                .SingleOrDefaultAsync(m => m.BusinessEntityId == id);
            if (businessEntity == null)
            {
                return NotFound();
            }

            return View(businessEntity);
        }

        // GET: BusinessEntities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BusinessEntities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BusinessEntityId,Rowguid,ModifiedDate")] BusinessEntity businessEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(businessEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(businessEntity);
        }

        // GET: BusinessEntities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var businessEntity = await _context.BusinessEntity.SingleOrDefaultAsync(m => m.BusinessEntityId == id);
            if (businessEntity == null)
            {
                return NotFound();
            }
            return View(businessEntity);
        }

        // POST: BusinessEntities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BusinessEntityId,Rowguid,ModifiedDate")] BusinessEntity businessEntity)
        {
            if (id != businessEntity.BusinessEntityId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(businessEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BusinessEntityExists(businessEntity.BusinessEntityId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(businessEntity);
        }

        // GET: BusinessEntities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var businessEntity = await _context.BusinessEntity
                .SingleOrDefaultAsync(m => m.BusinessEntityId == id);
            if (businessEntity == null)
            {
                return NotFound();
            }

            return View(businessEntity);
        }

        // POST: BusinessEntities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var businessEntity = await _context.BusinessEntity.SingleOrDefaultAsync(m => m.BusinessEntityId == id);
            _context.BusinessEntity.Remove(businessEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BusinessEntityExists(int id)
        {
            return _context.BusinessEntity.Any(e => e.BusinessEntityId == id);
        }
    }
}
