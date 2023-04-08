using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppLibraryV1.StableModels
{
    public enum Menu
    {
        AuthorGetAll = 1,
        AuthorGetById,
        AuthorAdd,
        AuthorEdit,
        AuthorRemove,

        BookGetAll,
        BookGetById,
        BookAdd,
        BookEdit,
        BookRemove,

        SaveAndExit
    }
}
