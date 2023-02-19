using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Modules.Validation
{
    public class FirstNameValidator : IValidator
    {
        private string valueString;


        public string ErrorMessage { get; protected set; }




        public FirstNameValidator(string validationText)
        {
            if(string.IsNullOrEmpty(validationText) == true)
                throw new ArgumentNullException("validationText", "validationText نباید خالی یا null باشد .");

            this.valueString = validationText;
        }




        /// <summary>
        /// بخاطر اجرای اصل Liskov در solid ، چون کلاس دیگری از این ارث بری و استفاده میکند ، مستقیما درون این متد ، در صورت نیاز ، پروپرتیِ ErrorMessage مقداردهی نمیشود
        /// و متد دیگری که virtual هست ، فراخوانی میشود تا زمانی که در کلاس فرزند ، این متد را فراخوانی میکنیم ، نیاز به تغییر این متد در کلاس فرزند نداشته باشیم تا اصل liskov نقض شود .
        /// بلکه فقط نیاز به تغییر متد دیگری _SetErrorMessage_ که virtual هست ، داریم .
        /// </summary>
        /// <param name="validationText"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public bool IsValid()
        {
            try
            {
                bool isMatched = Regex.Match(this.valueString, @"^[\p{L}\p{M}' \.\-]+$").Success;
                if (isMatched == false)
                    this.SetErrorMessage(isMatched);

                return isMatched;
            }
            catch (ArgumentException argumentExp)
            {
                throw new ArgumentException("یک خطا در ترجمه ی عبارات نامنظم روی داد .", argumentExp);
            }
        }


        protected virtual void SetErrorMessage(bool isRegexMatched)
        {
            if (isRegexMatched == false)
                this.ErrorMessage = "اطلاعات وارد شده برای نام شخص ، نامعتبر هست .";
        }


    }
}
