using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
    public class ProductsController : Controller
    {
        private readonly IRepository<Product,int> _pRepository;
        private readonly IRepository<Category, int> _cRepository;
        private readonly IWebHostEnvironment _env;

        public ProductsController(IRepository<Product,int> pRepository, IRepository<Category, int> cRepository,  IWebHostEnvironment env)
        {
            _pRepository = pRepository;
            _cRepository = cRepository;
            _env = env;
        }

        // GET: Controllers/Products
        public async Task<IActionResult> Index()
        {
            var spyShopContext = _pRepository.GetAll().Include(p => p.Category);
            var viewModel = new ProductsIndexVM();
            viewModel.Products = await spyShopContext.ToListAsync();


            return View(viewModel);
        }

        // GET: Controllers/Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _pRepository.GetAll()
                                    .Include(p => p.Category)
                                    .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            var viewModel = new ProductDetailVM { 
                Id = product.Id,
                Name = product.Description,
                Description = product.Description,
                PhotoUrl = product.PhotoUrl,
                Price = product.Price,
                SortNumber = product.SortNumber,
                CategoryName = product.Category.Name            
            };

            return View(viewModel);
        }

        // GET: Controllers/Products/Create
        public IActionResult Create()
        {
            //ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            var viewModel = new ProductsCreateVM { Price = 0 };
            viewModel.AvailableCategories = _cRepository.GetAll().OrderBy(e => e.Name);

            
            return View(viewModel);
        }

        // POST: Controllers/Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductsCreateVM createVM)
        {
            if (ModelState.IsValid)
            {
                Category category = await _cRepository.GetByIdAsync(createVM.CategoryId.Value);

                if (category != null)
                {

                    Product createdProduct = new Product
                    {
                        Name = createVM.Name,
                        Description = createVM.Description,
                        Price = createVM.Price,
                        PhotoUrl = createVM.PhotoUrl,
                        SortNumber = createVM.SortNumber,
                        Category=category

                    };
                    createdProduct.PhotoUrl = await SaveProductImage(createVM.UploadedImage);



                    await _pRepository.AddAsync(createdProduct);
                    TempData[Constants.Constants.SuccesMessage] = $"Product {createdProduct.Name} has been created" ;
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(nameof(createVM.CategoryId), "This Category was not found");
                }

            }

            createVM.AvailableCategories = _cRepository.GetAll().OrderBy(e => e.Name);
            //ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            return View(createVM);
        }

        // GET: Controllers/Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _pRepository.GetAll().Include(p=>p.Category).SingleOrDefaultAsync(m=>m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            var viewModel = new ProductsEditVM
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                PhotoUrl = product.PhotoUrl,
                Price = product.Price,
                SortNumber = product.SortNumber,
                CategoryId = product.Category.Id,
                AvailableCategories=_cRepository.GetAll().OrderBy(e=>e.Name)

            };


            //ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            return View(viewModel);
        }

        // POST: Controllers/Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductsEditVM editVM)
        {
            if (id != editVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Category category = await _cRepository.GetByIdAsync(editVM.CategoryId.Value);

                    if (category != null)
                    {

                        Product updateProduct = new Product
                        {
                            Id = editVM.Id,
                            Name = editVM.Name,
                            Description = editVM.Description,
                            Price = editVM.Price,
                            PhotoUrl = editVM.PhotoUrl,
                            SortNumber = editVM.SortNumber,
                            CategoryId = category.Id

                        };



                        await _pRepository.UpdateAsync(updateProduct);
                        TempData[Constants.Constants.SuccesMessage] = $"Product {updateProduct.Name} has been Updated";

                    }
                    else
                    {
                        ModelState.AddModelError(nameof(editVM.CategoryId), "This Category does not exist");
                    }



                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(editVM.Id))
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
            //ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);

            editVM.AvailableCategories = _cRepository.GetAll().OrderBy(e => e.Name);
            return View(editVM);
        }

        // GET: Controllers/Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _pRepository.GetAll()
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Controllers/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _pRepository.GetByIdAsync(id);
            DeleteProductImage(product);

            await _pRepository.DeleteAsync(product);
            TempData[Constants.Constants.SuccesMessage] = $"Product {product.Name} has been deleted";
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _pRepository.GetAll().Any(e => e.Id == id);
        }

        private async Task<string> SaveProductImage(IFormFile file)
        {
            string uniqueFileName = Guid.NewGuid().ToString("N") + file.FileName;
            string savePath = Path.Combine(_env.WebRootPath, "images", "products", uniqueFileName);

            using(var newFileStream = new FileStream(savePath, FileMode.Create))
            {
                await file.CopyToAsync(newFileStream);
            }
            return uniqueFileName;



        }

        private void DeleteProductImage(Product product)
        {

            if (!string.IsNullOrWhiteSpace(product?.PhotoUrl))
            {
                string deletePath = Path.Combine(_env.WebRootPath, "images", "products", product?.PhotoUrl);
                System.IO.File.Delete(deletePath);
            }

        }

    }
}
