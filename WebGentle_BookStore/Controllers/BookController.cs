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
        public ViewResult GetAllBooks()
        {
            //return _bookRepository.GetAllBooks();
            var allbookData = _bookRepository.GetAllBooks();
            return View(allbookData);
        }

        [Route("book-details/{id}", Name ="BookDetailsRoute")]
        public ViewResult GetBook(int id, string nameOfBook)
        {
            //Using Dynamic Views.
            //dynamic oneBookData = new ExpandoObject();
            //oneBookData.book = _bookRepository.GetBookById(id);
            //oneBookData.Name = "Sunanda Naik";
            //OR
            var oneBookData = _bookRepository.GetBookById(id);
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
        public IActionResult AddNewBook(BookModel bookModel)
        {
            int bookId = _bookRepository.AddNewBook(bookModel);
            if(bookId > 0)
            {
                //return RedirectToAction(nameof(AddNewBook)); //it returns string
                //OR
                return RedirectToAction("AddNewBook",new { isSuccess = true, bookId = bookId });
            }
            return View();
        }
    }
}
