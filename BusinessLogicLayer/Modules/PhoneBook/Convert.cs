using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Modules.PhoneBook
{
    internal class Convert
    {
        /// <summary>
        /// این متد ، در صورت خالی بودن tpIenumerable ، مقدار null را برنمیگرداند بلکه شیِ لیست خالی را برمیگرداند .
        /// </summary>
        /// <typeparam name="Tp"></typeparam>
        /// <param name="tpIenumerable"></param>
        /// <returns></returns>
        internal static List<Tp> ConvertIenumerableToList<Tp>(IEnumerable<Tp> tpIenumerable)
        {
            List<Tp> tpList = new List<Tp>();
            if (tpIenumerable != null && tpIenumerable.Count() > 0)
                tpList = tpIenumerable.ToList();

            return tpList;
        }


    }

}
