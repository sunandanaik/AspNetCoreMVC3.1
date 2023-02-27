﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebGentle_BookStore.Data
{
    public class Language
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        //To create relationship between language and book table
        public ICollection<Books> Books { get; set; }
    }
}
