using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebGentle_BookStore.Data;
using WebGentle_BookStore.Models;

namespace WebGentle_BookStore.Repository
{
    public class BookRepository
    {
        //Since EF works with context class, we use BookStoreContext class here.
        private readonly BookStoreContext _context = null;

        //Now to initialize the above context class we create constructor and create instance inside it.
        public BookRepository(BookStoreContext context)
        {
            _context = context;
        }

        public async Task<int> AddNewBook(BookModel model)
        {
            //Now create instance of Books entity class here and map all the properties of model book class to entity book class.
            var newBook = new Books()
            {
                Title = model.Title,
                Author = model.Author,
                Description = model.Description,
                Category = model.Category,
                LanguageId = model.LanguageId,
                TotalPages = model.TotalPages.HasValue ? model.TotalPages : 0,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow,
                CoverImageUrl = model.CoverImageUrl //we need to get path in folder variable.1. to pass as parameter & 2. to add property in bookmodel and use.
            };

            //Make a list
            var galleryLst = new List<BookGallery>();
            newBook.bookGallery = new List<BookGallery>();

            foreach (var file in model.Gallery)
            {
                newBook.bookGallery.Add (new BookGallery()
                {
                    Name = file.Name,
                    URL = file.URL
                });
                
            }
           
            //Now to map entity class instance to context class.
            //_context.Books.Add(newBook);
            //_context.SaveChanges(); //then only application will hit db.

            //OR To make async call using EF Core.
            await _context.Books.AddAsync(newBook);
            await _context.SaveChangesAsync();
            
            //Once saved the Id will be automatically be associated with this newBook object.
            return newBook.Id;
        }
        public async Task<List<BookModel>> GetAllBooks()
        {
            //var books = new List<BookModel>();
            //var booksData = await _context.Books.ToListAsync();
            ////Now to convert Books to List of BookModel.
            //if(booksData.Any() == true)
            //{
            //    foreach(var item in booksData)
            //    {
            //        books.Add(new BookModel()
            //        {
            //            Title = item.Title,
            //            Author = item.Author,
            //            Description = item.Description,
            //            Category = item.Category,
            //            LanguageId = item.LanguageId,
            //            Language = item.Language.Name,
            //            TotalPages = item.TotalPages,
            //            Id = item.Id,
            //            CoverImageUrl = item.CoverImageUrl

            //        });
            //    }
            //}
            //return books;

            //OR-----
            var booksData = await _context.Books.Select(book => new BookModel()
            {
                Title = book.Title,
                Author = book.Author,
                Description = book.Description,
                Category = book.Category,
                LanguageId = book.LanguageId,
                Language = book.Language.Name,
                TotalPages = book.TotalPages,
                Id = book.Id,
                CoverImageUrl = book.CoverImageUrl
            }).ToListAsync();

            return booksData;

            //OR---
            //return DataSource();
        }

        public async Task<BookModel> GetBookById(int id)
        {
            var bookData = await _context.Books.Where(x => x.Id == id).Select(book => new BookModel()
            {
                Title = book.Title,
                Author = book.Author,
                Description = book.Description,
                Category = book.Category,
                LanguageId = book.LanguageId,
                Language = book.Language.Name,
                TotalPages = book.TotalPages,
                Id = book.Id,
                CoverImageUrl = book.CoverImageUrl,
                Gallery = book.bookGallery.Select(g => new GalleryModel()
                {
                    Id = g.Id,
                    Name = g.Name,
                    URL = g.URL
                }).ToList()
            }).FirstOrDefaultAsync();

            return bookData;
            //OR
            //var bookData = await _context.Books.FindAsync(id);
            //if (bookData != null)
            //{
            //    var bookModel = new BookModel()
            //    {
            //        Title = bookData.Title,
            //        Author = bookData.Author,
            //        Description = bookData.Description,
            //        Category = bookData.Category,
            //        LanguageId = bookData.LanguageId,
            //        Language = bookData.Language.Name,
            //        TotalPages = bookData.TotalPages,
            //        Id =bookData.Id
            //    };
            //    return bookModel;
            //}
            //return null;
            //OR
            //_context.Books.Where(x => x.Id == id).FirstOrDefaultAsync();
            //return DataSource().Where(x => x.Id == id).FirstOrDefault();
        }

        //public List<BookModel> SearchBooks(string bookName, string authorName)
        //{
        //    return DataSource().Where(x => x.Title.Contains(bookName) || x.Author.Contains(authorName)).ToList();
        //}

        //private List<BookModel> DataSource()
        //{
        //    return new List<BookModel>()
        //    {
        //        new BookModel(){Id=1,Title="MVC",Author="James Boss", Description="This is the description for MVC book.", Category="Programming", language="English",TotalPages=134},
        //        new BookModel(){Id=2, Title="Java",Author="James Gosling", Description="This is the description for Java book.", Category="Concept", language="French",TotalPages=205},
        //        new BookModel(){Id=3,Title="C++",Author="Dennis Ritchie", Description="This is the description for Cplusplus book.", Category="Developer", language="Hindi",TotalPages=564},
        //        new BookModel(){Id=3,Title="Azure DevOps",Author="Sunanda", Description="This is the description for Azure DevOps book.", Category="DevOps",language="English", TotalPages=800}
        //    };
        //}
    }
}
