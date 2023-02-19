using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Modules.Validation
{
    public class PhoneNumberValidator : IValidator
    {
        private string valueString;


        public string ErrorMessage { get; private set; }




        public PhoneNumberValidator(string validationText)
        {
            if (string.IsNullOrEmpty(validationText) == true)
                throw new ArgumentNullException("validationText", "validationText نباید خالی یا null باشد .");

            this.valueString = validationText;
        }




        public bool IsValid()
        {
            bool isDigit = RegexWork.IsDigitRegexMatch(this.valueString);
            if (isDigit == false)
            {
                this.ErrorMessage = "مقدار وارد شده برای شماره تلفن ، فقط باید عدد باشد و نباید کاراکتر دیگری جز عدد باشد .";
                return false;
            }
            else
            {
                bool isPositive = ValidationWork.IsPsitiveNumber(this.valueString);
                if (isPositive == false)
                {
                    this.ErrorMessage = "عدد باید بزرگتر از صفر باشد .";
                    return false;
                }

                bool isBetween = ValidationWork.HasCharNumberBetween(this.valueString, 8, 10);
                if (isBetween == false)
                {
                    this.ErrorMessage = "تعداد اعداد وارد شده برای شماره تلفن ، باید بین 7 تا 10 رقم بدون در نظر گرفتن اولین عدد از صِفر باشد .";
                    return false;
                }

                return true;
            }
        }


    }
}
