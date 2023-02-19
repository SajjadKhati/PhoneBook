using BusinessLogicLayer.Modules.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Modules.PhoneBook
{
    public class CheckValidate
    {
        public static bool IsAddMethodValidate(Person person)
        {
            IValidator firstNameValidator = new FirstNameValidator(person.FirstName);
            if (firstNameValidator.IsValid() == false)
                throw new ValidatorException("نام", firstNameValidator.ErrorMessage);

            IValidator lastNameValidator = new LastNameValidator(person.LastName);
            if (lastNameValidator.IsValid() == false)
                throw new ValidatorException("نام خانوادگی", lastNameValidator.ErrorMessage);

            IValidator phoneNumberValidator = new PhoneNumberValidator(person.PhoneNumber.ToString());
            if (phoneNumberValidator.IsValid() == false)
                throw new ValidatorException("شماره تماس", phoneNumberValidator.ErrorMessage);

            IValidator nationalCodeValidator = new NationalCodeValidator(person.NationalCode.ToString());
            if (nationalCodeValidator.IsValid() == false)
                throw new ValidatorException("کد ملی", nationalCodeValidator.ErrorMessage);

            return true;
        }


        public static bool IsEditMethodValidate(Person person)
        {
            bool idValidator = IsIDValidate(person.PersonID);
            if (idValidator == false)
                return false;

            return IsAddMethodValidate(person);
        }


        public static bool IsIDValidate(int id)
        {
            IValidator isIDValidate = new IdentityValidator(id);
            if (isIDValidate.IsValid() == false)
                throw new ValidatorException("شناسه شخص", isIDValidate.ErrorMessage);

            return true;
        }



    }


}
