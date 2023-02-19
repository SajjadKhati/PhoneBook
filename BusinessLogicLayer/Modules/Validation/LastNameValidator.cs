using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Modules.Validation
{
    public class LastNameValidator : FirstNameValidator
    {
        public LastNameValidator(string validationText) : base(validationText)
        {
        }


        protected override void SetErrorMessage(bool isRegexMatched)
        {
            if(isRegexMatched == false)
                this.ErrorMessage = "اطلاعات وارد شده برای نام خانوادگی شخص ، نامعتبر هست .";
        }


    }


}
