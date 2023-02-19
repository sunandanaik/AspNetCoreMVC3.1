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

        public int AddNewBook(BookModel model)
        {
            //Now create instance of Books entity class here and map all the properties of model book class to entity book class.
            var newBook = new Books()
            {
                Title = model.Title,
                Author = model.Author,
                Description = model.Description,
                Category = model.Category,
                language = model.language,
                TotalPages = model.TotalPages,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow
            };
            //Now to map entity class to context class.
            _context.Books.Add(newBook);
            _context.SaveChanges(); //then only application will hit db.
            //Once saved the Id will be automatically be associated with this newBook object.

            return newBook.Id;
        }
        public List<BookModel> GetAllBooks()
        {
            return DataSource();
        }

        public BookModel GetBookById(int id)
        {
            return DataSource().Where(x => x.Id == id).FirstOrDefault();
        }

        public List<BookModel> SearchBooks(string bookName, string authorName)
        {
            return DataSource().Where(x => x.Title.Contains(bookName) || x.Author.Contains(authorName)).ToList();
        }

        private List<BookModel> DataSource()
        {
            return new List<BookModel>()
            {
                new BookModel(){Id=1,Title="MVC",Author="James Boss", Description="This is the description for MVC book.", Category="Programming", language="English",TotalPages=134},
                new BookModel(){Id=2, Title="Java",Author="James Gosling", Description="This is the description for Java book.", Category="Concept", language="French",TotalPages=205},
                new BookModel(){Id=3,Title="C++",Author="Dennis Ritchie", Description="This is the description for Cplusplus book.", Category="Developer", language="Hindi",TotalPages=564},
                new BookModel(){Id=3,Title="Azure DevOps",Author="Sunanda", Description="This is the description for Azure DevOps book.", Category="DevOps",language="English", TotalPages=800}
            };
        }
    }
}
