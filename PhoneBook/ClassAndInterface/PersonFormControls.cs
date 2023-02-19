using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhoneBook
{
    public class PersonFormControls
    {
        public TextBox FirstNameTextBox{ get; set; }


        public TextBox LastNameTextBox{ get; set; }


        public TextBox PhoneNumberTextBox{ get; set; }


        public TextBox NationalCodeTextBox{ get; set; }


        public Button SaveButton{ get; set; }



        public PersonFormControls()
        {

        }


        public PersonFormControls(TextBox firstNameTextBox, TextBox lastNameTextBox,
            TextBox phoneNumberTextBox, TextBox nationalCodeTextBox)
        {
            this.FirstNameTextBox = firstNameTextBox;
            this.LastNameTextBox = lastNameTextBox;
            this.PhoneNumberTextBox = phoneNumberTextBox;
            this.NationalCodeTextBox = nationalCodeTextBox;
        }


        public PersonFormControls(TextBox firstNameTextBox, TextBox lastNameTextBox, TextBox phoneNumberTextBox, 
            TextBox nationalCodeTextBox, Button saveButton) 
            : this(firstNameTextBox, lastNameTextBox, phoneNumberTextBox, nationalCodeTextBox)
        {
            this.SaveButton = saveButton;
        }


    }
}
