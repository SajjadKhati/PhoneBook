using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Modules.Validation
{
    public class NationalCodeValidator : IValidator
    {
        private string valueString;


        public string ErrorMessage{ get; private set; }




        public NationalCodeValidator(string validationText)
        {
            /// در کد ملی ، لازم نیست که خالی بودن یا نبودن را بررسی کنیم چون موقع اضافه یا ویرایش کردن ، وجودش ضروری نیست .
            if (validationText == null)
                throw new ArgumentNullException("validationText", "validationText نباید null باشد .");

            this.valueString = validationText;
        }




        public bool IsValid()
        {
            /// چون NationalCode اختیاری هست ، پس اگر خالی باشد ، میتواند ذخیره شود .
            if (this.valueString == "")
                return true;

            bool isDigit = RegexWork.IsDigitRegexMatch(this.valueString);
            if (isDigit == false)
            {
                this.ErrorMessage = "مقدار وارد شده برای کد ملی، فقط باید عدد باشد و نباید کاراکتر دیگری جز عدد باشد .";
                return false;
            }
            else
            {
                if (this.valueString.Length == 10)
                    return true;
                else
                {
                    this.ErrorMessage = "تعداد اعداد وارد شده برای کد ملی ، فقط باید 10 رقمی باشد .";
                    return false;
                }
            }
        }


    }
}
