using Microsoft.AspNetCore.Mvc;
using LibraryProject.DataAccess.Models;
using LibraryProject.Services;

namespace LibraryProject.Controllers
{
	public class CategoriesController : Controller
	{
		private readonly ICategoriesService _categoriesService;

		public CategoriesController(ICategoriesService categoriesService)
		{
			_categoriesService = categoriesService;
		}
		public async Task<IActionResult> Index()
		{
			var categories = await _categoriesService.GetAllAsync();
			return View(categories);
		}
		[HttpGet]
		public async Task<IActionResult> GetById(int id)
		{
			var categories = await _categoriesService.GetByIdAsync(id);
			return View(categories);
		}
		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
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
		public async Task<IActionResult> Delete(int id)
		{
			var categories = await _categoriesService.GetByIdAsync(id);
			return View(categories);
		}
		[HttpPost]
		public async Task<IActionResult> ConfirmDelete(int id)
		{
			await _categoriesService.DeleteAsync(id);
			return RedirectToAction(nameof(Index));
		}
		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			var categories = await _categoriesService.GetByIdAsync(id);
			return View(new CategoryModel
			{ Name = categories.Name });
		}
		[HttpPost]
		public async Task<IActionResult> Edit(int id,  CategoryModel model)
		{
			var categories = await _categoriesService.GetByIdAsync(id);
			categories.Name = model.Name;
			await _categoriesService.UpdateAsync(id, categories);
			return RedirectToAction(nameof(Index));
		}
		[HttpGet]
		public async Task<IActionResult> Details(int id)
		{
			var categories = await _categoriesService.GetByIdAsync(id);
			return View(categories);
		}
	}
}
