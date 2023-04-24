using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebGentle_BookStore.Data
{
    //Entity Class
    public class Books
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }

        //public string language { get; set; }
        public int LanguageId { get; set; }
        public int? TotalPages { get; set; }
        public string CoverImageUrl { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }

        //To give relationship between Books class and Language class
        public Language Language { get; set; }
        public ICollection<BookGallery> bookGallery { get; set; } //with this approach we created one-to-many relationship



    }
}
