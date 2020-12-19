using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Wba.SpyshopActual.Domain;
using Wba.SpyshopActual.Domain.Entitys;
using Wba.SpyshopActual.Web.Areas.Admin.ViewModels;
using Wba.SpyshopActual.Web.Data;

namespace Wba.SpyshopActual.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        private readonly IRepository<Category,int> _cRepository;

        public CategoriesController(IRepository<Category, int> cRepository)
        {
            _cRepository = cRepository;
        }

        // GET: Admin/Categories
        public async Task<IActionResult> Index()
        {
            var viewModel = new CategoriesIndexVM {

                Categories = await _cRepository.GetAll()
                .ToListAsync()
            
            };
            return View(viewModel);
        }

        // GET: Admin/Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewModel = await _cRepository.GetAll()
                .Select(c=> new CategoriesEditVM {
                    Id = c.Id,
                    Name = c.Name,
                    NumberOfProducts = c.Products.Count()
                }).FirstOrDefaultAsync(e=>e.Id==id);
            if (viewModel == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }

        // GET: Admin/Categories/Create
        public IActionResult Create()
        {
            return View(new CategoriesCreateVM());
        }

        // POST: Admin/Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoriesCreateVM createVM)
        {
            if (ModelState.IsValid)
            {
                Category createCategory = new Category {
                Name= createVM.Name
                };
                
                await _cRepository.AddAsync(createCategory);
                TempData[Constants.Constants.SuccesMessage] = $"Categorie {createCategory.Name} has been created";
                return RedirectToAction(nameof(Index));
            }
            return View(createVM);
        }

        // GET: Admin/Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var viewModel = await _cRepository.GetAll()
                .Select(c => 
                    new CategoriesEditVM {
                        Id = c.Id,
                        Name = c.Name,
                        NumberOfProducts=c.Products.Count})
                .SingleOrDefaultAsync(e=>e.Id == id);

            
            if (viewModel == null)
            {
                return NotFound();
            }
            return View(viewModel);
        }

        // POST: Admin/Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CategoriesEditVM editVM)
        {
            if (id != editVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Category updatedCategory = await _cRepository.GetByIdAsync(editVM.Id);
                    updatedCategory.Id = editVM.Id;
                    updatedCategory.Name = editVM.Name;
                    await _cRepository.UpdateAsync(updatedCategory);
                    TempData[Constants.Constants.SuccesMessage] = $"Categorie {updatedCategory.Name} has been updated";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(editVM.Id))
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
            return View(editVM);
        }

        // GET: Admin/Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _cRepository.GetAll()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Admin/Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _cRepository.GetAll().SingleOrDefaultAsync(c=>c.Id == id);
            
            await _cRepository.DeleteAsync(category);
            TempData[Constants.Constants.SuccesMessage] = $"Categorie {category.Name} has been deleted";
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return _cRepository.GetAll().Any(e => e.Id == id);
        }
    }
}
