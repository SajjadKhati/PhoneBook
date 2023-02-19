using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Modules.PhoneBook
{
    /// <summary>
    /// بخاطر اینکه پروپرتی PersonsDataTable از نوع DataTable و این نوع هم Dispose شدنی هست
    /// و از این پروپرتی ، در کلاسی که این اینترفیس را پیاده سازی میکند ، شیِ جدیدی ساخته میشود ، به همین علت ، این اینترفیس از اینترفیس IDisposable مشتق میشود .
    ///
    ///چون شی کلاسی که این اینترفیس را پیاده سازی میکند ، معمولا در تمام طول عمر رابط کاربری ، استفاده میشود ،
    /// پس متد Dispose اش را فقط زمانی که از کل برنامه در UI خارج میشویم ، فراخوانی میکنیم .
    /// 
    /// بخاطر اینکه این اینترفیس برای پروژه های دیگه هم استفاده بشود ، نوع جنریک برای آن در نظر میگیریم .
    ///و در کلاس PhoneBook  نوع جنریک آنرا تعیین میکنیم .
    /// </summary>
    public interface IPhoneBook<Tp> : IWorkingInformation<Tp>, IGetAllDataAsync<Tp>, IDisposable 
    where Tp : class
    {
        /// <summary>
        /// این نوع ، فقط باید توسط بقیه ی لایه ها یا قسمت های دیگه ، get شود و فقط از داخل کلاسی که این پروپرتی را پیاده سازی میکند ، قابل تغییر و مقداردهی باشد .
        /// </summary>
        DataTable PersonsDataTable { get; }



    }

}
