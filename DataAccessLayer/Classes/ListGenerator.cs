using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    internal class ListGenerator
    {
        internal static List<T> CreatList<T>(params T[] elements)
        {
            return new List<T>(elements);
        }



    }
}
