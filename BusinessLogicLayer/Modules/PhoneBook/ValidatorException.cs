using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Modules.PhoneBook
{
    public class ValidatorException : Exception
    {
        /// <summary>
        /// فیلدی که باعث منشاء استثنا و خطا بود
        /// </summary>
        public string ErrorFieldSource { get; set; }




        public ValidatorException(string errorFieldSource, string message) : base(message)
        {
            this.ErrorFieldSource = errorFieldSource;
        }


    }
}
