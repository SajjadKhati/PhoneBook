using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhoneBook
{
    /// <summary>
    /// چون فرم شخص ، بصورت چندین حالت و در شرایط مختلف باز میشود ، و حتی ممکن است در نسخه های بعدی ، این شرایط تغییر هم کنند ،
    /// بنابراین برای رعایت اصل باز و بسته در اصول solid ، این اینترفیس را برایش میسازیم .
    ///
    /// متد سازنده های کلاس های مشتق این اینترفیس ، مقادیر رشته ی مورد نظر را برای تنظیم کردن در TextBox ها میگیرند.
    /// </summary>
    public interface IPersonFormOpenMode
    {
        /// <summary>
        /// برای رعایت اصول باز و بسته در solid و فرضا اگه در آینده بخواهیم فیلدی را اضافه یا کم کنیم و برای اینکه این کم یا زیاد کردن ، روی اینترفیس تاثیری نگذارد ،
        /// بناراین اطلاعات TextBox ها و هر اطلاعات مورد نیاز را بصورت شی ای از یک کلاس دیگر دریافت میکنیم .
        /// کلاس PersonFormControls
        /// </summary>
        void DoAction(PersonFormControls personFormControls);
    }
}
