﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebGentle_BookStore.Models;
using WebGentle_BookStore.Repository;
using System.Dynamic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace WebGentle_BookStore.Controllers
{
    public class BookController : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}
        private readonly BookRepository _bookRepository = null;
        private readonly LanguageRepository _languageRepository = null;
        private readonly IWebHostEnvironment _webHostEnvironment;

        //We need to add repository dependency into this constructor since Asp.net core by default supports dependency injection.
        public BookController(BookRepository bookRepository,LanguageRepository languageRepository, IWebHostEnvironment env)
        {
            //_bookRepository = new BookRepository();
            _bookRepository = bookRepository;
            _languageRepository = languageRepository;
            _webHostEnvironment = env;
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

        //public List<BookModel> SearchBooks(string bookName, string authorName)
        //{
        //    //return $"Book with Name = {bookName} and Author is = {authorName}";
        //    return _bookRepository.SearchBooks(bookName, authorName);
        //}

        public async Task<ViewResult> AddNewBook(bool isSuccess = false, int bookId = 0)
        {
            //To show as default value selected when AddNewBook page is loaded from Controller.
            var bookModel = new BookModel
            {
                //language = "English"
                //OR to pass default selected value, we pass id.
                //language = "2"
            };
            //Now Using List
            //var list = new List<string>() { "Hindi", "English", "Dutch" };
            //ViewBag.Language = list;
            //OR
            //Now using SelectList method
            //ViewBag.Language = new SelectList(GetLanguage(), "Id", "Text");
            //OR 
            //Now Using SelectListItem method
            //ViewBag.Language = GetLanguage().Select(x => new SelectListItem()
            //{
            //    Text = x.Text,
            //    Value = x.Id.ToString()
            //}).ToList();

            //OR
            //ViewBag.Language = new List<SelectListItem>()
            //{
            //    new SelectListItem(){Text = "hindi", Value = "1" },
            //    new SelectListItem(){ Text="English", Value="2", Disabled=true},
            //    new SelectListItem(){Text = "Marathi", Value = "3", Selected = true}
            //};

            //OR
            //To use Enum

            //OR
            //To get all language data from database.And to format data and send to the View.
            var languages = new SelectList(await _languageRepository.GetLanguages(), "Id", "Name");
            ViewBag.Language = languages;

            ViewBag.IsSuccess = isSuccess;
            ViewBag.BookId = bookId;
            return View(bookModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewBook(BookModel bookModel)
        {
            //To check if model properties are vaildated or not.
            if (ModelState.IsValid)
            {
                if(bookModel.CoverPhoto != null)
                {
                    string folder = "books/cover/";
                    bookModel.CoverImageUrl = await UploadImage(folder,bookModel.CoverPhoto);
                }
                if (bookModel.GalleryImages != null)
                {
                    string folder = "books/gallery/";

                    //Now to add 2 properties: url & name to store into Db.
                    bookModel.Gallery = new List<GalleryModel>(); //Here make object of list of GalleryModel.

                    foreach(var file in bookModel.GalleryImages)
                    {
                        var gallery = new GalleryModel()
                        {
                            Name = file.FileName,
                            URL = await UploadImage(folder, file) //Now to save url into DB we need to store in another class GalleryModel.cs
                        };
                        bookModel.Gallery.Add(gallery); //Add each gallery imgs into List of GalleryModel imgs.
                    }
                   
                }

                if (bookModel.BookPdf != null)
                {
                    string folder = "books/pdf/";
                    bookModel.BookPdfUrl = await UploadImage(folder, bookModel.BookPdf);
                }

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
            //In order to pass language list after AddNewBook page load, we need to mention in Post here.
            //var list = new List<string>() { "Hindi", "English", "Dutch" };
            //ViewBag.Language = list;
            //OR
            //ViewBag.Language = new SelectList(GetLanguage(), "Id", "Text");

            //OR
            var languages = new SelectList(await _languageRepository.GetLanguages(), "Id", "Name");
            ViewBag.Language = languages;

            //If you want to add custom error message to Modelstate
            ModelState.AddModelError("", "This is my custom error message.");

            return View();
        }

        //Now to make this method common, will have to pass folder name dynamically in parameter.
        private async Task<string> UploadImage(string folderPath,IFormFile file)
        {
            //To append image name to the folder and also you may upload multiple images with same name
            //so we use unique key as guid.
            folderPath += Guid.NewGuid().ToString() + "_" + file.FileName;
            //bookModel.CoverImageUrl = "/" + folder; //Here binding image url to model property.
            //In order to combine both the paths, use Path of System.IO class.
            string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folderPath);
            //Now to save image to folder
            await file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
            return "/" + folderPath;
        }

        //private List<LanguageModel> GetLanguage()
        //{
        //    return new List<LanguageModel>()
        //    {
        //        new LanguageModel(){Id=1, Text= "Hindi"},
        //        new LanguageModel(){Id=2, Text="English"},
        //        new LanguageModel(){Id=3, Text="German"}
        //    };
        //}
    }
}
