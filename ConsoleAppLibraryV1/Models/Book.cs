using ConsoleAppLibraryV1.StableModels;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppLibraryV1.Models
{
    [Serializable]
    public class Book : IIdentity
    {
        static int counter = 0;
        public Book()
        {
            counter++;
            this.Id = counter;
        }

        public int Id { get; private set; }
        public string Name { get; set; }
        public int AuthorId { get; set; }
        public Genre Genre { get; set; }
        public ushort PageCount { get; set; }
        public decimal Price { get; set; }

        public override string ToString()
        {
            return $"Category: {Genre.ToString().PadRight(25)}\nYear: {Year}\nPrice: {Price}";
        }
    }
}
