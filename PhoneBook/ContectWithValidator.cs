using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLogicLayer.Modules.PhoneBook;

namespace PhoneBook
{
    internal class ContectWithValidator
    {
        internal static bool IsValidForAdding(PersonFormInformation personFormInformation)
        {
            bool isValid = false;
            string errorCaption = "خطای اعتبار سنجی";

            try
            {
                Person personForValidating = new Person();
                personForValidating.FirstName = personFormInformation.FirstName;
                personForValidating.LastName = personFormInformation.LastName;
                personForValidating.NationalCode = personFormInformation.NationalCode;

                long phoneNumberConverted = Convertor.PhoneNumberWithMessage(personFormInformation.PhoneNumber);
                if (phoneNumberConverted < 0)
                    return false;

                personForValidating.PhoneNumber = phoneNumberConverted;
                isValid = CheckValidate.IsAddMethodValidate(personForValidating);
            }
            catch (ValidatorException validatorException)
            {
                string errorText = $"خطای اعتبار سنجی برای فرمت '{validatorException.ErrorFieldSource}' روی داد\n\n{validatorException.Message}";
                MessageBox.Show(errorText, errorCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return isValid;
        }


    }
}
