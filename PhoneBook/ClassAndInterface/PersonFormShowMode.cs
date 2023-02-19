using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhoneBook
{
    internal class PersonFormShowMode : PersonFormEditMode
    {

        /// <summary>
        /// برای رعایت اصول باز و بسته در solid و فرضا اگه در آینده بخواهیم فیلدی را اضافه یا کم کنیم و برای اینکه این کم یا زیاد کردن ، روی این متد سازنده تاثیری نگذارد ،
        /// بناراین اطلاعات های مورد نیاز را بصورت شی ای از یک کلاس دیگر دریافت میکنیم .
        /// کلاس PersonFormInformation
        /// </summary>
        public PersonFormShowMode(PersonFormInformation personFormInformation) : base(personFormInformation)
        {
        }




        public override void DoAction(PersonFormControls personFormControls)
        {
            base.DoAction(personFormControls);
            this.NoChangableAction(personFormControls);
        }


        private void NoChangableAction(PersonFormControls personFormControls)
        {
            personFormControls.FirstNameTextBox.ReadOnly = true;
            personFormControls.LastNameTextBox.ReadOnly = true;
            personFormControls.PhoneNumberTextBox.ReadOnly = true;
            personFormControls.NationalCodeTextBox.ReadOnly = true;

            personFormControls.SaveButton.Enabled = false;
        }

    }
}
