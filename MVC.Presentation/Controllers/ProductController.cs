using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC.Application.Services.Interfaces.Products;
using MVC.Application.Services.Interfaces.User;
using MVC.Domain.Models;

namespace MVC.Presentation.Controllers
{
    public class ProductController : Controller
    {
        public readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<IActionResult> Index(int page =1 , int pageSize = 9)
        {
            var Count = await _productService.CountData();
            ViewBag.Count = Count;
            ViewBag.pageSize = pageSize;
            var data = await _productService.PagenationData(page, pageSize);
            if (data.IsSuccess == true && data.Obj != null)
                return View(data.Obj);
            return View();
        }
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(Product Model)
        {
            if (ModelState.IsValid)
            {
                var product = await _productService.Create(Model);
                return RedirectToAction(nameof(Index));
            }
            return View(Model);
        }
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var department = await _productService.GetById(id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department.Obj);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(int id, Product model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var Model = await _productService.Update(model);
                return RedirectToAction(nameof(Details), new { id = model.Id });
            }
            return View(model);
        }
        [Authorize]
        public async Task<IActionResult> Details(int id)
        {
            var department = await _productService.GetById(id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department.Obj);
        }
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var department = await _productService.GetById(id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department.Obj);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Delete(int id, Product model)
        {
            var department = await _productService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
