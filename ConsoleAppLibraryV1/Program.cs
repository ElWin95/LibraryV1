using ConsoleAppLibraryV1.Helper;
using ConsoleAppLibraryV1.Models;
using ConsoleAppLibraryV1.StableModels;
using ConsoleAppLibraryV1.Storage;
using System.Runtime.Serialization.Formatters.Binary;

namespace ConsoleAppLibraryV1
{
    internal class Program
    {
        const string databaseFile = "database.dat";

        #region Store
        static GenericStore<Author> authorStore = new GenericStore<Author>();
        static GenericStore<Book> bookStore = new GenericStore<Book>();
        #endregion

        static void Main(string[] args)
        {
            using (FileStream fileStream = File.Open(databaseFile, FileMode.OpenOrCreate))
            {
                BinaryFormatter bf = new BinaryFormatter();
                var db = (Database)bf.Deserialize(fileStream);

                if (db != null)
                {
                    authorStore = db.Authors;
                    bookStore = db.Books;
                }
            }

            int id;
            bool allowForClear = true;
            Author author;
            Book book;

            Menu menu;

        l1:
            menu = Extension.PrintMenu();

            switch (menu)
            {
                case Menu.AuthorGetAll:
                    #region For All
                    Console.Clear();
                    if (authorStore.Length == 0)
                    {
                        Console.WriteLine("Muellifler boshdur,yeni muellif elave edin...");
                        goto case Menu.AuthorAdd;
                    }

                    ShowAllAuthor(false);
                    goto l1;
                #endregion
                case Menu.AuthorGetById:
                    ShowAllAuthor(true);
                l2:
                    id = Extension.ReadInteger("Marka id: ", true, authorStore.Min(x => x.Id), authorStore.Max(x => x.Id));

                    author = authorStore.Find(id);

                    if (author == null)
                    {
                        Console.WriteLine($"Bu muellif movcud deyil");
                        goto l1;
                    }
                    Console.WriteLine(author);
                    goto l1;

                case Menu.AuthorAdd:
                    if (allowForClear)
                        Console.Clear();
                    author = new Author();
                    author.Name = Extension.ReadString("Muellif adini yaz: ");
                    author.Surname = Extension.ReadString("Muellif soyadini yaz: ");
                    authorStore.Add(author);
                    Console.Write("Elave edildi");
                    goto l1;

                case Menu.AuthorEdit:

                    ShowAllAuthor(true);
                    id = Extension.ReadInteger("Muellif id: ", true, authorStore.Min(x => x.Id), authorStore.Max(x => x.Id));

                    author = authorStore.Find(id);
                    if (author == null)
                    {
                        goto case Menu.AuthorEdit;
                    }

                    author.Name = Extension.ReadString("Muellif adi: ");
                    goto case Menu.AuthorGetAll;


                case Menu.AuthorRemove:
                    ShowAllAuthor(true);
                    id = Extension.ReadInteger("Muellif id: ", true, authorStore.Min(x => x.Id), authorStore.Max(x => x.Id));

                    author = authorStore.Find(id);
                    if (author == null)
                    {
                        goto case Menu.AuthorRemove;
                    }

                    authorStore.Remove(author);

                    goto case Menu.AuthorGetAll;

                case Menu.BookGetAll:
                    #region For All
                    Console.Clear();
                    if (bookStore.Length == 0)
                    {
                        Console.WriteLine("Kitablar boshdur,yeni kitab elave edin...");
                        goto case Menu.BookAdd;
                    }

                    ShowAllBooks(false);
                    goto l1;
                    #endregion
                case Menu.BookGetById:
                    ShowAllBooks(true);
                    Console.WriteLine($"=========== ======== ===========");
                l5:
                    id = Extension.ReadInteger("Kitab id: ", true, bookStore.Min(x => x.Id), bookStore.Max(x => x.Id));

                    book = bookStore.Find(id);

                    if (book == null)
                    {
                        Console.WriteLine($"Bu kitab movcud deyil");
                        goto l5;
                    }
                    Console.WriteLine(book);
                    goto l1;
                case Menu.BookAdd:
                    Console.Clear();
                    if (authorStore.Length == 0)
                    {
                        allowForClear = false;
                        Console.WriteLine("Muellifler boshdur,ilk once muellif elave edilmelidir!");
                        goto case Menu.AuthorAdd;
                    }
                    book = new Book();
                l6:
                    ShowAllAuthor(false);
                    id = Extension.ReadInteger("Muellif id: ", true, authorStore.Min(x => x.Id), authorStore.Max(x => x.Id));

                    author = authorStore.Find(id);
                    if (author == null)
                    {
                        goto l6;
                    }

                    book.PageCount = Extension.ReadUInt16("Sehifesi: ", true, 50, 1500);
                    book.Price = Extension.ReadDecimal("Qiymet: ", true, 2);
                    book.Genre = Extension.ReadEnum<Genre>("Janr: ");

                    bookStore.Add(book);

                    ShowAllBooks(true);
                    goto l1;
                case Menu.BookEdit:
                    ShowAllBooks(true);
                    id = Extension.ReadInteger("Muellif id: ", true, bookStore.Min(x => x.Id), bookStore.Max(x => x.Id));

                    book = bookStore.Find(id);
                    if (book == null)
                    {
                        goto case Menu.AuthorEdit;
                    }

                    book.Name = Extension.ReadString("Muellif adi: ");
                    ShowAllAuthor(false);
                l4:
                    id = Extension.ReadInteger("Muellif id: ", true, authorStore.Min(x => x.Id), authorStore.Max(x => x.Id));

                    if (!authorStore.Any(x => x.Id == id))
                    {
                        Console.WriteLine($"Muellif movcud deyil,siyahidan secin!");

                        goto l4;
                    }
                    book.AuthorId = id;
                    goto case Menu.BookGetAll;

                case Menu.BookRemove:
                    ShowAllBooks(true);
                    id = Extension.ReadInteger("Kitab id: ", true, bookStore.Min(x => x.Id), bookStore.Max(x => x.Id));

                    book = bookStore.Find(id);
                    if (book == null)
                    {
                        goto case Menu.BookRemove;
                    }

                    bookStore.Remove(book);

                    goto case Menu.BookGetAll;
                case Menu.SaveAndExit:

                    Database db = new Database();
                    db.Authors = authorStore;
                    db.Books = bookStore;

                    FileStream fileStream = File.Create(databaseFile);

                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(fileStream, db);
                    fileStream.Flush();
                    fileStream.Close();

                    Console.WriteLine("Cixish ucun her hansi duymeni sixin!");
                    Console.ReadKey();
                    break;
                default:
                    break;
            }
        }

        private static void ShowAllBooks(bool clearBefore)
        {
            if (clearBefore)
            {
                Console.Clear();
            }

            Console.WriteLine($"=========== KITABLAR ===========");
            foreach (var item in bookStore)
            {
                var author = authorStore.Find(item.AuthorId);

                Console.WriteLine($"{item.Id}. {author.Name} {author.Surname}-\n{item}\n-------------------------------------------");
            }
            Console.WriteLine($"=========== ======== ===========");
        }

        private static void ShowAllAuthor(bool clearBefore)
        {
            if (clearBefore)
            {
                Console.Clear();
            }

            Console.WriteLine($"=========== MUELLIFLER ===========");
            foreach (var item in authorStore)
            {
                Console.WriteLine($"{item.Id} {item.Name} {item.Surname}");
            }
            Console.WriteLine($"=========== ======== ===========");
        }

    }
}