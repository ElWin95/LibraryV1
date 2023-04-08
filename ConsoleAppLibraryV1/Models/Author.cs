using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppLibraryV1.Models
{
    [Serializable]
    public class Author : IIdentity, IEquatable<Author>
    {
        static int counter = 0;
        public Author()
        {
            counter++;
            this.Id = counter;
        }

        public int Id { get; private set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public bool Equals(Author? other)
        {
            return this.Id == other.Id;
        }

        public override string ToString()
        {
            return $"{Id}.{Name} {Surname}";
        }
    }
}
