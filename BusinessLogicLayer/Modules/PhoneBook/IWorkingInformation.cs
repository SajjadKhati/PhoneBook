using BusinessLogicLayer.Modules.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Modules.PhoneBook
{
    /// <summary>
    /// بخاطر اصل جداسازی اینترفیس ها در اصول solid ، این اینترفیس برای کار مجزا ، جدا شد .
    /// این اینترفیس برای عمل درج و به روزرسانی و حذف و جستجوی اطلاعات در لایه ی منطق تجاری یا لایه ی Model هست .
    ///
    /// بخاطر اینکه این اینترفیس برای پروژه های دیگه هم استفاده بشود ، نوع جنریک برای آن در نظر میگیریم .
    ///و در کلاس PhoneBook  نوع جنریک آنرا تعیین میکنیم .
    /// </summary>
    public interface IWorkingInformation<Tp> where Tp : class
    {
        IEnumerable<Tp> GetAllData();


        IEnumerable<Tp> SearchByAllFields(string firstName = "", string lastName = "", string phoneNumber = "", string nationalCode = "");


        Tp SearchById(int id);




        /// <summary>
        /// به دلیل رعایت اصل باز و بسته در solid ، فقط یک شی از نوع Tp از رابط کاربری دریافت میشود و اعتبار سنجی ، درون همین متد Add انجام میشود .
        /// وگرنه اگر به ازای هر فیلد ، شی ای از IValidator دریافت گردد ، با هر بار اضافه و یا تغییر در ستون ها در دیتابیس ، این متد در این اینترفیس هم باید تغییر کند .
        /// </summary>
        /// <param name="type">
        ///نوع مورد نظر برای اضافه شدن .
        /// </param>
        /// <returns></returns>
        bool Add(Tp type);


        /// <summary>
        /// توضیحات این متد هم مثل متد Add هست
        /// </summary>
        /// <param name="type">
        ///نوع مورد نظر برای اضافه شدن .
        /// </param>
        /// <returns></returns>
        bool Edit(Tp type);


        bool Delete(int id);

    }
}
