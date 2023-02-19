using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    /// <summary>
    /// بخاطر اصل جداسازی اینترفیس ها در اصول solid ، یا همون اصل interface segregation principle ، اینترفیس ها را از هم جدا کردم .
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface ISyncRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();

        /// <summary>
        /// بهتر بود چون در آینده شاید ممکن باشد که فیلدها برای جستجو ، تغییر کنند ، و بنابراین برای اینکه اینترفیس مان تغییر نکند و اصل باز و بسته در solid نقض نشود ،
        /// بهتر بود که مقادیر پارامترهای این متد را در کلاسی ذخیره و شی ای از آن کلاس را به عنوان پارامتر دریافت میکردیم .
        /// 
        /// اما چون هم کمتر این اتفاق ممکن هست پیش بیاید و هم بخاطر کمبود وقت ، این طور پیاده سازی انجام نشد .
        /// </summary>
        /// <returns></returns>
        IEnumerable<TEntity> SearchByAllFields(string firstName = "", string lastName = "", string phoneNumber = "", string nationalCode = "");

        IEnumerable<TEntity> SearchByID(int id);

        /// <summary>
        /// در راستای اجرا اصل باز و بسته در solid و بسته بودن نسبت به تغییرات ، احتمالا بهتر هست که این نوع متدها که با اضافه شدن ستون در دیتاتبیس تغییر میکنند ،
        /// بصورت آرایه ای از object ها به عنوان ورودی دریافت شوند .
        /// اما برای پیاده سازی اش فرصت لازم هست چون اطلاعات تعداد و نوع ستون را از دیتابیس هم نیاز دارد تا قبلش بداند و شاید روال های اضافی دیگر هم باشد .
        /// <returns>
        ///  موفقیت آمیز بودن یا نبودن عملیات را برمیگرداند
        /// </returns>
        bool Insert(string firstName, string lastName, long phoneNumber, string nationalCode = "");


        /// <summary>
        ///  موفقیت آمیز بودن یا نبودن عملیات را برمیگرداند
        /// <returns></returns>
        bool Update(int id, string firstName, string lastName, long phoneNumber, string nationalCode = "");


        /// <summary>
        ///  موفقیت آمیز بودن یا نبودن عملیات را برمیگرداند
        /// <returns></returns>
        bool Delete(int id);
    }
}
