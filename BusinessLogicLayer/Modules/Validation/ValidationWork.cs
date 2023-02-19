using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Modules.Validation
{
    internal class ValidationWork
    {
        internal static bool IsPsitiveNumber(string idString)
        {
            long id = -1;
            long.TryParse(idString, out id);

            if (id > 0)
                return true;
            else
                return false;
        }


        internal static bool HasCharNumberBetween(string numberString, int smallerNumber, int biggerNumber)
        {
            if (numberString.Length >= smallerNumber && numberString.Length <= biggerNumber)
                return true;
            else
                return false ;
        }


    }
}
