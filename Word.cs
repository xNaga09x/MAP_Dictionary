using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tema1
{
    internal class Word
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public Category Category { get; set; }
    }

    public class Category
    {
        public String Name { get; set; }
    }

    public class Admin
    {
        public String Name { get; set; }
        public String Password { get; set; }
    }
}
