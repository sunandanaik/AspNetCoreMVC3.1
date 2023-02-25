using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebGentle_BookStore.Models;
using WebGentle_BookStore.Repository;
using System.Dynamic;

namespace WebGentle_BookStore.Controllers
{
    public class BookController : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}
        private readonly BookRepository _bookRepository = null;
        public BookController(BookRepository bookRepository)
        {
            //_bookRepository = new BookRepository();
            _bookRepository = bookRepository;
        }
        public async Task<ViewResult> GetAllBooks()
        {
            //return _bookRepository.GetAllBooks();
            var allbookData =await  _bookRepository.GetAllBooks();
            return View(allbookData);
        }

        [Route("book-details/{id}", Name ="BookDetailsRoute")]
        public async Task<ViewResult> GetBook(int id, string nameOfBook)
        {
            //Using Dynamic Views.
            //dynamic oneBookData = new ExpandoObject();
            //oneBookData.book = _bookRepository.GetBookById(id);
            //oneBookData.Name = "Sunanda Naik";
            //OR
            var oneBookData = await _bookRepository.GetBookById(id);
            return View(oneBookData);
        }

        public List<BookModel> SearchBooks(string bookName, string authorName)
        {
            //return $"Book with Name = {bookName} and Author is = {authorName}";
            return _bookRepository.SearchBooks(bookName, authorName);
        }

        public ViewResult AddNewBook(bool isSuccess = false, int bookId = 0)
        {
            ViewBag.IsSuccess = isSuccess;
            ViewBag.BookId = bookId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNewBook(BookModel bookModel)
        {
            //To check if model properties are vaildated or not.
            if (ModelState.IsValid)
            {
                int bookId = await _bookRepository.AddNewBook(bookModel);
                if (bookId > 0)
                {
                    //return RedirectToAction(nameof(AddNewBook)); //it returns string
                    //OR
                    return RedirectToAction("AddNewBook", new { isSuccess = true, bookId = bookId });
                }
            }
            ViewBag.IsSuccess = false;
            ViewBag.BookId = 0;

            //If you want to add custom error message to Modelstate
            ModelState.AddModelError("", "This is my custom error message.");

            return View();
        }
    }
}
