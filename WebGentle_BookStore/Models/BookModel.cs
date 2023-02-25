using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebGentle_BookStore.Models
{
    public class BookModel
    {
        //DataType is Enum usd to only generate fields ont he form.it is not used to validate the data.
        [DataType(DataType.Date)]
        [Display(Name ="Choose Date")]
        public string MyField { get; set; }

        public int Id { get; set; }

        [Required(ErrorMessage ="Please enter Book Title")]
        [StringLength(100, MinimumLength =5)]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public string Description { get; set; }
        public string Category { get; set; }
        public string language { get; set; }

        [Required]
        [Display(Name ="Total Pages of Book")]
        public int? TotalPages { get; set; }
    }
}
