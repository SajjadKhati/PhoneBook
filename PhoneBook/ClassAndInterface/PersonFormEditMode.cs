using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhoneBook
{
    internal class PersonFormEditMode : IPersonFormOpenMode
    {
        private readonly PersonFormInformation personFormInformation;




        /// <summary>
        /// برای رعایت اصول باز و بسته در solid و فرضا اگه در آینده بخواهیم فیلدی را اضافه یا کم کنیم و برای اینکه این کم یا زیاد کردن ، روی این متد سازنده تاثیری نگذارد ،
        /// بناراین اطلاعات های مورد نیاز را بصورت شی ای از یک کلاس دیگر دریافت میکنیم .
        /// کلاس PersonFormInformation
        /// </summary>
        public PersonFormEditMode(PersonFormInformation personFormInformation)
        {
            this.personFormInformation = personFormInformation;
        }




        public virtual void DoAction(PersonFormControls personFormControls)
        {
            this.SetTextBoxsText(personFormControls);
        }


        private void SetTextBoxsText(PersonFormControls personFormControls)
        {
            personFormControls.FirstNameTextBox.Text = this.personFormInformation.FirstName;
            personFormControls.LastNameTextBox.Text = this.personFormInformation.LastName;
            personFormControls.PhoneNumberTextBox.Text = this.personFormInformation.PhoneNumber;
            personFormControls.NationalCodeTextBox.Text = this.personFormInformation.NationalCode;
        }


    }
}
