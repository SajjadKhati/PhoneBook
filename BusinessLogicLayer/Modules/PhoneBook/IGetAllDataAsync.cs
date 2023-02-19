using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Modules.PhoneBook
{
    /// <summary>
    /// بخاطر اصل جداسازی اینترفیس ها در solid ، این اینترفیس برای کاری مجزا ، جدا شد
    /// 
    /// همچنین متدی نظیر SearchByAllFieldsAsync در این اینترفیس ساخته نشد چون حداقل در این نسخه بخاطر کمبود زمان ، DataTable
    /// برایش در نظر گرفتیم و بخاطر وجود DataTable ، جستجوها از داخل آن انجام میشود نه از دیتابیس .
    ///
    /// بخاطر اینکه این اینترفیس برای پروژه های دیگه هم استفاده بشود ، نوع جنریک برای آن در نظر میگیریم .
    ///و در کلاس PhoneBook  نوع جنریک آنرا تعیین میکنیم .
    /// </summary>
    public interface IGetAllDataAsync<Tp> where Tp : class
    {
        Task<IEnumerable<Tp>> GetAllDataAsync();
    }
}
