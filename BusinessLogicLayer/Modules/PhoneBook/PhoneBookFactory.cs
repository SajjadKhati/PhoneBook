using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Modules.PhoneBook
{
    /// <summary>
    /// پیاده سازی الگوی طراحی Factory Method برای ماژول IPhoneBook
    /// 
    /// هر چند کلاس PhoneBook ، شیِ Disposable ای هست و درون این کلاس به آن شیِ جدیدی اختصاص دادیم و پس باید این کلاس را هم Disposable میکردیم ،
    /// اما بخاطر اینکه این متد Disposable ، درون اینترفیسِ IPhoneBook هم در دسترس هست ، و در لایه ی UI ، فقط زمان خروج از برنامه ، آنرا Dispose میکنیم و
    /// و همچنین میخوایم این کلاس را هم static کنیم ، به همین جهت ، این کلاس را Disposable نکردیم .
    /// </summary>
    public class PhoneBookFactory
    {
        public static IPhoneBook<Person> CreatePhoneBook(string fullDatabaseFilePath)
        {
            if (string.IsNullOrEmpty(fullDatabaseFilePath) == true)
                throw new ArgumentNullException("fullDatabaseFilePath", "آدرس کامل فایل دیتابیس نیاز هست");

            return new PhoneBook(fullDatabaseFilePath);
        }



    }
}
