using Microsoft.AspNetCore.Mvc;
using LibraryProject.DataAccess.Models;
using LibraryProject.Services;
using Microsoft.AspNetCore.Authorization;

namespace LibraryProject.Controllers
{
	public class CategoriesController : Controller
	{
		private readonly ICategoriesService _categoriesService;

		public CategoriesController(ICategoriesService categoriesService)
		{
			_categoriesService = categoriesService;
		}
        [Authorize(Roles = "Admin, Recepsionist")]
        public async Task<IActionResult> Index()
        {
            var categories = await _categoriesService.GetAllAsync();
            var sortedCategories = categories.OrderBy(c => c.Name).ToList();
            return View(sortedCategories);
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Recepsionist")]
        public async Task<IActionResult> GetById(int id)
		{
			var categories = await _categoriesService.GetByIdAsync(id);
			return View(categories);
		}
		[HttpGet]
        [Authorize(Roles = "Admin, Recepsionist")]
        public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
        [Authorize(Roles = "Admin, Recepsionist")]
        public async Task<IActionResult> Create(CategoryModel model)
		{
            if (ModelState.IsValid)
			{
				await _categoriesService.AddAsync(new Category
				{
				Name = model.Name });
				return RedirectToAction(nameof(Index));
			}
			return View(model);
		}
		[HttpGet]
        [Authorize(Roles = "Admin, Recepsionist")]
        public async Task<IActionResult> Delete(int id)
		{
			var categories = await _categoriesService.GetByIdAsync(id);
			return View(categories);
		}
		[HttpPost]
        [Authorize(Roles = "Admin, Recepsionist")]
        public async Task<IActionResult> ConfirmDelete(int id)
		{
			await _categoriesService.DeleteAsync(id);
			return RedirectToAction(nameof(Index));
		}
		[HttpGet]
        [Authorize(Roles = "Admin, Recepsionist")]
        public async Task<IActionResult> Edit(int id)
		{
			var categories = await _categoriesService.GetByIdAsync(id);
			return View(new CategoryModel
			{ Name = categories.Name });
		}
		[HttpPost]
        [Authorize(Roles = "Admin, Recepsionist")]
        public async Task<IActionResult> Edit(int id,  CategoryModel model)
		{
			var categories = await _categoriesService.GetByIdAsync(id);
			categories.Name = model.Name;
			await _categoriesService.UpdateAsync(id, categories);
			return RedirectToAction(nameof(Index));
		}
		[HttpGet]
        [Authorize(Roles = "Admin, Recepsionist")]
        public async Task<IActionResult> Details(int id)
		{
			var categories = await _categoriesService.GetByIdAsync(id);
			return View(categories);
		}
	}
}
