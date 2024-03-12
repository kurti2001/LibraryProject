using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using LibraryProject.DataAccess.Models;
using LibraryProject.Services;

namespace LibraryProject.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBooksService _booksService;
        private readonly ICategoriesService _categoriesService;

        public BooksController(IBooksService booksService,
                               ICategoriesService categoriesService)
        {
            _booksService = booksService;
            _categoriesService = categoriesService;
        }

        public async Task<IActionResult> Index()
        {
            var books = await _booksService.GetAllAsync();
            return View(books);
        }
        [HttpGet]
        public async Task<IActionResult> Index(string Title)
        {
            var books = await _booksService.GetAllAsync();

            if (!String.IsNullOrEmpty(Title))
            {
                books = books.Where(x => x.Title.Contains(Title, StringComparison.OrdinalIgnoreCase));
            }

            return View(books);
        }


        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var books = await _booksService.GetByIdAsync(id);
            return View(books);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await _categoriesService.GetAllAsync();
            var categoriesModel = categories.Select(category => new SelectListItem(category.Name,
                                                                                     category.Id.ToString()))
                                             .ToList();
            ViewBag.Categories = categoriesModel;
            return View(new BookModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(BookModel model, IFormFile picture)
        {
            if (ModelState.IsValid)
            {
                var currentTime = DateTime.UtcNow;

                string imagePath = null;
                if (picture != null && picture.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(picture.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await picture.CopyToAsync(stream);
                    }
                    imagePath = "/images/" + fileName;
                }

                await _booksService.AddAsync(new Book
                {
                    Title = model.Title,
                    Description = model.Description,
                    ISBN = model.ISBN,
                    Created = currentTime,
                    Updated = currentTime,
                    CategoryId = model.CategoryId,
                    ImagePath = imagePath
                });

                return RedirectToAction(nameof(Index));
            }

            var categories = await _categoriesService.GetAllAsync();
            ViewBag.Categories = categories.Select(category => new SelectListItem(category.Name, category.Id.ToString())).ToList();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var books = await _booksService.GetByIdAsync(id);
            return View(books);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            await _booksService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var books = await _booksService.GetByIdAsync(id);
            var categories = await _categoriesService.GetAllAsync();
            var categoriesModel = categories.Select(category => new SelectListItem(category.Name,
                                                                                    category.Id.ToString(),
                                                                                    category.Id == books.CategoryId))
                                             .ToList();
            ViewBag.Categories = categoriesModel;
            return View(new BookModel
            {
                Title = books.Title,
                Description = books.Description,
                ISBN = books.ISBN,
                Updated = books.Updated,
                CategoryId = books.CategoryId
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, BookModel model, IFormFile picture)
        {
            if (ModelState.IsValid)
            {
                var book = await _booksService.GetByIdAsync(id);

                book.Title = model.Title;
                book.Description = model.Description;
                book.ISBN = model.ISBN;
                book.Updated = DateTime.UtcNow;
                book.CategoryId = model.CategoryId;

                if (picture != null && picture.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(picture.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await picture.CopyToAsync(stream);
                    }
                    book.ImagePath = "/images/" + fileName;
                }

                await _booksService.UpdateAsync(id, book);
                return RedirectToAction(nameof(Index));
            }

            var categories = await _categoriesService.GetAllAsync();
            ViewBag.Categories = categories.Select(category => new SelectListItem(category.Name, category.Id.ToString())).ToList();
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var books = await _booksService.GetByIdAsync(id);
            books.Category = await _categoriesService.GetByIdAsync(books.CategoryId);
            return View(books);
        }
        [HttpGet("books/search")]
        public async Task<List<BookModel>> FilterBooks(string q)
        {
            var books = await _booksService.FindByTitle(q);
            foreach (var book in books)
            {
                var entity = await _booksService.GetByIdAsync(book.Id);
                book.ImagePath = entity.ImagePath;
            }

            return books;
        }
        [HttpGet("books/filter")]
        public Task<IActionResult> FilterBooksView(string? q)
        {
            List<BookModel> result = new();
            if(q != null)
            {
                result = FilterBooks(q).Result;
            }
            return Task.FromResult<IActionResult>(View(result));
        }

    }
}
