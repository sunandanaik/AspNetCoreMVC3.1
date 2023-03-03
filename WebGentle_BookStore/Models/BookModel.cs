using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WebGentle_BookStore.Enums;
using WebGentle_BookStore.Helpers;

namespace WebGentle_BookStore.Models
{
    public class BookModel
    {
        //DataType is Enum usd to only generate fields ont he form.it is not used to validate the data.
        [DataType(DataType.Date)]
        [Display(Name ="Choose Date")]
        public string MyField { get; set; }

        public int Id { get; set; }

        //[Required(ErrorMessage ="Please enter Book Title")]
        //[StringLength(100, MinimumLength =5)]
        //To add custom error msg use ErrorMessage property here.
        //[MyCustomValidation(ErrorMessage ="This is custom error message for custom validation.")]
        //[MyCustomValidation(Text ="Azure")]
        //OR if you donot want to pass text property here then make use of constructor in MyCustomValidationAttribute class.
        [MyCustomValidation("Azure")]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public string Description { get; set; }
        public string Category { get; set; }

        [Required]
        [Display(Name ="Choose Language")]
        //public string language { get; set; }
        public int LanguageId { get; set; }

        public string Language { get; set; }

        [Required(ErrorMessage ="Please choose the languages of your book")]
        public List<string> Multilanguage { get; set; }

        [Required(ErrorMessage = "Please choose the languages of your book")]
        public LanguageEnum LanguageEnum { get; set; } 

        [Required]
        [Display(Name ="Total Pages of Book")]
        public int? TotalPages { get; set; }
    }
}
