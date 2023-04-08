using ConsoleAppLibraryV1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppLibraryV1.Storage
{
    [Serializable]
    public class Database
    {
        public GenericStore<Author> Authors { get; set; }
        public GenericStore<Book> Books { get; set; }
    }
}
