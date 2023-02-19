using BusinessLogicLayer.Modules.PhoneBook;
using PhoneBookPresentationLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhoneBook
{
    public partial class PersonForm : Form
    {
        /// <summary>
        /// اطلاعاتی که کاربر در textbox ها تغییر داد ، درون این شی ذخیره میشود .
        /// </summary>
        internal PersonFormInformation PersonFormInformation{ get; set; }




        public PersonForm()
        {
            InitializeComponent();

            this.InitializeCustomDefinedMembers();
            this.SettingControlsProperties();
        }


        public PersonForm(IPersonFormOpenMode personFormOpenMode) : this()
        {
            PersonFormControls personFormControls = new PersonFormControls(
                this.FirstNameTextBox, this.LastNameTextBox, this.PhoneNumberTextBox, this.NationalCodeTextBox, this.SaveButton);

            personFormOpenMode.DoAction(personFormControls);
        }

        private void InitializeCustomDefinedMembers()
        {
            this.PersonFormInformation = new PersonFormInformation();
        }


        private void SettingControlsProperties()
        {
            this.SettingGroupBoxsProperties();
        }


        private void SettingGroupBoxsProperties()
        {
            this.PersonSpecificGroupBox.RightToLeft = RightToLeft.Yes;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            bool isInitializeValid = this.AreRequiredFieldsFull();
            if (isInitializeValid == false)
            {
                string errorCaption = "خطای اعتبار سنجی";
                string errorText = $"لطفا همه ی فیلدهای ضروری و ستاره دار را پر کنید .";
                MessageBox.Show(errorText, errorCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            /// قبل از اعتبارسنجی ، مقدار پروپرتی باید ست شود .
            this.SetPersonFormInformation();

            bool isValidAllThings = this.IsValid();
            if (isValidAllThings == false)
                return;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }


        private void SetPersonFormInformation()
        {
            /// شی PersonFormInformation ، در متد سازنده قبلا ایجاد شده
            //this.PersonFormInformation.PersonID = 
            this.PersonFormInformation.FirstName = this.FirstNameTextBox.Text;
            this.PersonFormInformation.LastName = this.LastNameTextBox.Text;
            this.PersonFormInformation .PhoneNumber = this.PhoneNumberTextBox.Text;
            this.PersonFormInformation.NationalCode = this.NationalCodeTextBox.Text;
        }


        /// <summary>
        /// این اعتبار سنجی بررسی اولیه میکند که TextBox های ضروری ، خالی نباشند .
        /// </summary>
        /// <returns></returns>
        private bool AreRequiredFieldsFull()
        {
            foreach (Control control in this.PersonSpecificGroupBox.Controls)
            {
                if (control is TextBox && control != this.NationalCodeTextBox)
                {
                    bool isEmpty = string.IsNullOrEmpty(control.Text);
                    if (isEmpty == true)
                        return false;
                }
            }
            return true;
        }


        private bool IsValid()
        {
            bool isValidate = ContectWithValidator.IsValidForAdding(this.PersonFormInformation);
            return isValidate;
        }



    }
}
