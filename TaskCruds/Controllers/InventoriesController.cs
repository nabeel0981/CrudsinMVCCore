using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TaskCruds.Db_Context;
using TaskCruds.Models;

namespace TaskCruds.Controllers
{
    public class InventoriesController : Controller
    {
        private readonly Inventory_Db _context;

        public InventoriesController(Inventory_Db context)
        {
            _context = context;
        }

        // GET: Inventories
        public async Task<IActionResult> Index()
        {
            if (_context.Inventories == null)
            {
                return Problem("Entity set 'Inventory_Db.Inventories' is null.");
            }

            var inventories = await _context.Inventories.ToListAsync();

            // Calculate NetMaterial for each item before displaying
            foreach (var inventory in inventories)
            {
                inventory.NetMaterial = inventory.ItemBalanceInSystem - inventory.DamageMaterial - inventory.ExpiredMaterial;
            }

            return View(inventories);
        }

        // GET: Inventories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Inventories == null)
            {
                return NotFound();
            }

            var inventory = await _context.Inventories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (inventory == null)
            {
                return NotFound();
            }

            return View(inventory);
        }

        // GET: Inventories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Inventories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ItemCode,ItemName,ItemUnit,ItemBalanceInSystem,ItemBalanceInStore,DamageMaterial,ExpiredMaterial,NetMaterial")] Inventory inventory)
        {
            if (ModelState.IsValid)
            {
                // Calculate NetMaterial and update ItemBalanceInStore and ItemBalanceInSystem
                inventory.NetMaterial = inventory.ItemBalanceInSystem - inventory.DamageMaterial - inventory.ExpiredMaterial;
                inventory.ItemBalanceInStore = inventory.NetMaterial;

                _context.Add(inventory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(inventory);
        }
        // GET: Inventories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Inventories == null)
            {
                return NotFound();
            }

            var inventory = await _context.Inventories.FindAsync(id);
            if (inventory == null)
            {
                return NotFound();
            }
            return View(inventory);
        }

       
        // POST: Inventories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ItemCode,ItemName,ItemUnit,ItemBalanceInSystem,ItemBalanceInStore,DamageMaterial,ExpiredMaterial,NetMaterial")] Inventory inventory)
        {
            if (id != inventory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Calculate NetMaterial and update ItemBalanceInStore and ItemBalanceInSystem
                    inventory.NetMaterial = inventory.ItemBalanceInSystem - inventory.DamageMaterial - inventory.ExpiredMaterial;
                    inventory.ItemBalanceInStore = inventory.NetMaterial;

                    _context.Update(inventory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InventoryExists(inventory.Id))
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
            return View(inventory);
        }

        // GET: Inventories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Inventories == null)
            {
                return NotFound();
            }

            var inventory = await _context.Inventories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (inventory == null)
            {
                return NotFound();
            }

            return View(inventory);
        }

        // POST: Inventories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Inventories == null)
            {
                return Problem("Entity set 'Inventory_Db.Inventories'  is null.");
            }

            var inventory = await _context.Inventories.FindAsync(id);
            if (inventory != null)
            {
                // Soft delete by reducing ItemBalanceInStore and updating NetMaterial
                inventory.ItemBalanceInStore -= 1; // Assuming you're removing one item
                inventory.NetMaterial = inventory.ItemBalanceInSystem - inventory.DamageMaterial - inventory.ExpiredMaterial;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InventoryExists(int id)
        {
          return (_context.Inventories?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
