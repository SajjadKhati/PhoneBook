using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Modules.PhoneBook
{
    /// <summary>
    /// برای نگاشت و map شدن در dapper ، باید نام این پروپرتی های کلاس Entity که به عنوان خروجی ، به متد Query اش میدیم ،
    /// باید دقیقا برابر با نام ستون های جدول های دیتابیس مون باشه وگرنه اگه نام پروپرتی هاش فرق کنه ، مقدارهای ستون های جداول ، توی این پروپرتی ها ، تنظیم نمیشه .
    /// </summary>
    public class Person
    {
        public int PersonID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public long PhoneNumber { get; set; }

        public string NationalCode { get; set; }




        public Person()
        {

        }


        public Person(string firstName, string lastName, long phoneNumber, string nationalCode)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.PhoneNumber = phoneNumber;
            this.NationalCode = nationalCode;
        }


        public Person(int personID, string firstName, string lastName, long phoneNumber, string nationalCode)
        {
            this.PersonID = personID;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.PhoneNumber = phoneNumber;
            this.NationalCode = nationalCode;
        }

    }
}
