using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebGentle_BookStore.Enums
{
    public enum LanguageEnum
    {
        [Display(Name ="Hindi language")]
        Hindi = 10,
        [Display(Name = "English language")]
        English = 11,
        [Display(Name = "Dutch language")]
        Dutch = 12,
        [Display(Name = "Marathi language")]
        Marathi = 13

    }
}
