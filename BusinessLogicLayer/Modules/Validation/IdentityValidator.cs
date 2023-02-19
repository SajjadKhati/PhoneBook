using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Modules.Validation
{
    public class IdentityValidator : IValidator
    {
        private string valueString;


        public string ErrorMessage { get; private set; }




        public IdentityValidator(long id)
        {
            this.valueString = id.ToString();
        }



        public bool IsValid()
        {
            bool isPositive = ValidationWork.IsPsitiveNumber(this.valueString);
            if(isPositive == true)
                return true;
            else
            {
                this.ErrorMessage = "عدد باید بزرگتر از صفر باشد .";
                return false;
            }
        }



    }
}
