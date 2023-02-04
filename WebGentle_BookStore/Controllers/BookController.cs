using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebGentle_BookStore.Models;
using WebGentle_BookStore.Repository;

namespace WebGentle_BookStore.Controllers
{
    public class BookController : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}
        private readonly BookRepository _bookRepository = null;
        public BookController()
        {
            _bookRepository = new BookRepository();
        }
        public ViewResult GetAllBooks()
        {
            //return _bookRepository.GetAllBooks();
            var allbookData = _bookRepository.GetAllBooks();
            return View(allbookData);
        }

        public BookModel GetBook(int id)
        {
            return _bookRepository.GetBookById(id);
        }

        public List<BookModel> SearchBooks(string bookName, string authorName)
        {
            //return $"Book with Name = {bookName} and Author is = {authorName}";
            return _bookRepository.SearchBooks(bookName, authorName);
        }
    }
}
