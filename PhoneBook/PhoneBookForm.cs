using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLogicLayer;
using BusinessLogicLayer.Modules.PhoneBook;
using myPhoneBook = BusinessLogicLayer.Modules.PhoneBook;
using BusinessLogicLayer.Modules.Validation;
using PhoneBook;
using PhoneBook.ContactWithPhoneBook;

namespace PhoneBookPresentationLayer
{
    public partial class PhoneBookForm : Form
    {
        #region  Fields


        private ContactWithPhoneBook contactWithPhoneBook;


        private ContextMenu dataGridViewContextMenu;


        #endregion


        ////////////////////////////////////////////////////////////////////////////


        #region  Properties


        /// <summary>
        /// این فیلد ، برای گرفتن اطلاعات سطر کلیک شده ، برای استفاده در منو آیتم ها ، زمانی که روی DataGridView راست کلیک میکنیم ، هست .
        /// </summary>
        private int ClickedRowIndex { get; set; } = -1;


        #endregion


        ////////////////////////////////////////////////////////////////////////////


        #region  Methods


        #region  Constractors


        public PhoneBookForm()
        {
            InitializeComponent();

            this.InitializeCustomDefinedMembers();
            this.SettingControlsProperties();
        }


        #endregion


        ////////////////////////////////////////////////////////////////////////////


        #region  Initialize Methods


        private void InitializeCustomDefinedMembers()
        {
            this.contactWithPhoneBook = new ContactWithPhoneBook(Program.fullDatabaseFilePath);
        }


        private void SettingControlsProperties()
        {
            this.SettingFormProperties();
            this.SettingGroupBoxsProperties();
            this.SettingDataGridViewProperties();
        }


        private void SettingFormProperties()
        {
            this.FormClosed += PhoneBookForm_FormClosed;
        }


        private void SettingGroupBoxsProperties()
        {
            this.PersonListGroupBox.RightToLeft = RightToLeft.Yes;
            this.SearchSpecificGroupBox.RightToLeft = RightToLeft.Yes;
        }


        private void SettingDataGridViewProperties()
        {
            this.InitializeDataGridViewContextMenu();

            this.PersonsDataGridView.EditMode = DataGridViewEditMode.EditProgrammatically;
            this.PersonsDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            /// بایندینگ ، در اینجا صورت گرفت .
            this.PersonsDataGridView.DataSource = this.contactWithPhoneBook.PhoneBook.PersonsDataTable;
        }


        private void InitializeDataGridViewContextMenu()
        {
            this.dataGridViewContextMenu = new ContextMenu();

            MenuItem showMenuItem = new MenuItem("نمایش", new EventHandler(this.ShowMenuItem_Click));
            MenuItem editMenuItem = new MenuItem("ویرایش", new EventHandler(this.EditMenuItem_Click));
            MenuItem separetorMenuItem = new MenuItem("-");
            MenuItem deleteMenuItem = new MenuItem("حدف", new EventHandler(this.DeleteMenuItem_Click));

            this.dataGridViewContextMenu.MenuItems.Add(showMenuItem);
            this.dataGridViewContextMenu.MenuItems.Add(editMenuItem);
            this.dataGridViewContextMenu.MenuItems.Add(separetorMenuItem);
            this.dataGridViewContextMenu.MenuItems.Add(deleteMenuItem);

        }


        #endregion


        ////////////////////////////////////////////////////////////////////////////


        #region  Events Handlers


        private void ShowMenuItem_Click(object sender, EventArgs e)
        {
            this.GetPersonInfoShowWindowInShowMode();
        }


        private void EditMenuItem_Click(object sender, EventArgs e)
        {
            PersonFormInformation currentSelectedRowPerson = this.GetPersonInfoFromSelectedRow();
            if (currentSelectedRowPerson == null)
                return;

            PersonFormInformation editedPersonInfo = this.ShowPersonWindowInEditMode(currentSelectedRowPerson);
            if (editedPersonInfo == null)
                return;

            if (this.contactWithPhoneBook.Edit(editedPersonInfo) == true)
                /// برای اینکه اگه قبل از اینکه پنجره ی PersonWindow را باز کرده باشند ، اقدام به جستجو کرده بودند و این جستجو ، در نتیجه ی اطلاعاتی که بعد از ویرایش کردن ،
                /// تاثیری نداشته باشد ، TextBox های مربوط به جستجو را خالی میکنیم .
                this.EmptySearchTextBoxs();
        }


        private void DeleteMenuItem_Click(object sender, EventArgs e)
        {
            ///  برای گرفتن اطلاعات شماره تلفنِ سطری که قرار هست توسط کاربر حذف شود ،
            /// و نمایش شماره ی تلفن به کاربر جهت اینکه کاربر مطمئن شود که رکورد مورد نظر برای حذف کردن را درست انتخاب کرده است . و همچنین نام فرد
            /// شماره تلفن از این جهت انتخاب میشود چون کلید واحد در دیتابیس هست .
            PersonFormInformation currentSelectedRowPerson = this.GetPersonInfoFromSelectedRow();
            if (currentSelectedRowPerson == null)
                return;

            if (this.IsConfirmDeleteShowDialog(currentSelectedRowPerson.FirstName, currentSelectedRowPerson.PhoneNumber) == false)
                return;

            if (this.contactWithPhoneBook.Delete(currentSelectedRowPerson.PersonID) == true)
                this.EmptySearchTextBoxs();
        }


        private void PhoneBookForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DisposeCustomMembers();
        }


        private void PersonsDataGridView_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            this.SetDataGridViewColumnsOrder();
            this.HidePersonIDColumn(e.Column);
        }


        private void PersonsDataGridView_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;
            bool isMouseOver = this.IsMouseOverDataGridView(e.X, e.Y);
            if (isMouseOver == false)
                return;

            this.SetClickedRowIndex(e.X, e.Y);
            this.ShowDataGridViewsMenu(e.X, e.Y);
        }


        private void PersonsDataGridView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            bool isMouseOver = this.IsMouseOverDataGridView(e.X, e.Y);
            if (isMouseOver == false)
                return;

            this.SetClickedRowIndex(e.X, e.Y);
            this.GetPersonInfoShowWindowInShowMode();
        }


        private void AddPersonButton_Click(object sender, EventArgs e)
        {
            PersonFormInformation personInformationForSave = this.ShowPersonWindow();
            if (personInformationForSave == null)
                return;

            if (this.contactWithPhoneBook.Add(personInformationForSave) == true)
                /// برای اینکه اگه قبل از اینکه پنجره ی PersonWindow را باز کرده باشند ، اقدام به جستجو کرده بودند و این جستجو ، در نتیجه ی اطلاعاتی که بعد از اضافه کردن ،
                /// تاثیری نداشته باشد ، TextBox های مربوط به جستجو را خالی میکنیم .
                this.EmptySearchTextBoxs();
        }


        private async void PhoneBookForm_Load(object sender, EventArgs e)
        {
            await this.contactWithPhoneBook.GetAllDataAsync();
        }


        private void SearchButton_Click(object sender, EventArgs e)
        {
            this.SearchByAllFields();
        }


        private void PersonsDataGridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            this.SetPersonCountLabel();
        }


        private void PersonsDataGridView_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            this.SetPersonCountLabel();
        }



        #endregion


        ////////////////////////////////////////////////////////////////////////////


        #region  Methods


        private bool IsConfirmDeleteShowDialog(string firstName, string phoneNumber)
        {
            string deleteCaption = "حذف اطلاعات شخص";
            string deleteMessage = "آیا مطمئنید که میخواهید اطلاعات شخص زیر را حذف کنید؟\n\n" +
                                   $"نام  :  {firstName}\nشماره تماس  :  {phoneNumber}";

            DialogResult quessionResult = MessageBox.Show(deleteMessage, deleteCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);

            return quessionResult == DialogResult.Yes;
        }


        private void SetDataGridViewColumnsOrder()
        {
            try
            {
                if (this.PersonsDataGridView.Columns["نام"] != null)
                    this.PersonsDataGridView.Columns["نام"].DisplayIndex = 0;

                if (this.PersonsDataGridView.Columns["نام خانوادگی"] != null)
                    this.PersonsDataGridView.Columns["نام خانوادگی"].DisplayIndex = 1;

                if (this.PersonsDataGridView.Columns["شماره تماس"] != null)
                    this.PersonsDataGridView.Columns["شماره تماس"].DisplayIndex = 2;

                if (this.PersonsDataGridView.Columns["کد ملی"] != null)
                    this.PersonsDataGridView.Columns["کد ملی"].DisplayIndex = 3;
            }
            catch (Exception e)
            {
            }
        }


        private void HidePersonIDColumn(DataGridViewColumn dataColumn)
        {
            if (dataColumn.Name == "PersonID")
                this.PersonsDataGridView.Columns[dataColumn.Name].Visible = false;
        }


        /// <summary>
        /// زمانی که فرم اصلی برنامه بسته شد ، اگر پروپرتیِ PhoneBook مقدار داشت ، آنرا Dispose کند .
        /// چون شامل DataTable هست که شی ای Dispose شدنی هست .
        ///
        /// همچنین بقیه ی کنترل های Dispose شدنی ای که در این کلاس شی جدیدی از آنها ایجاد شد را هم Dispose میکند .
        /// </summary>
        private void DisposeCustomMembers()
        {
            this.contactWithPhoneBook?.Dispose();
            this.dataGridViewContextMenu?.Dispose();
        }


        private void SetClickedRowIndex(int mousePosX, int mousePosY)
        {
            this.ClickedRowIndex = this.PersonsDataGridView.HitTest(mousePosX, mousePosY).RowIndex;
        }


        private bool IsMouseOverDataGridView(int mousePosX, int mousePosY)
        {
            int currentMouseOverRow = this.PersonsDataGridView.HitTest(mousePosX, mousePosY).RowIndex;
            int lastRowNumber = this.PersonsDataGridView.RowCount - 2;
            if (currentMouseOverRow >= 0 && currentMouseOverRow <= lastRowNumber)
                return true;
            else
                return false;
        }


        /// <summary>
        /// این متد ، اطلاعات سطری که کلیک شد را دریافت و پنجره ی PersonWindow را فقط در حالت Show Mode نمایش میدهد .
        /// </summary>
        private void GetPersonInfoShowWindowInShowMode()
        {
            PersonFormInformation currentSelectedRowPerson = this.GetPersonInfoFromSelectedRow();
            if (currentSelectedRowPerson == null)
                return;

            this.ShowPersonWindowInShowMode(currentSelectedRowPerson);
        }


        private void ShowPersonWindowInShowMode(PersonFormInformation personFormInformation)
        {
            IPersonFormOpenMode openMode = new PersonFormShowMode(personFormInformation);
            this.ShowPersonWindow(openMode);
        }


        private PersonFormInformation ShowPersonWindowInEditMode(PersonFormInformation personFormInformation)
        {
            IPersonFormOpenMode editMode = new PersonFormEditMode(personFormInformation);
            PersonFormInformation editedPersonInfo = this.ShowPersonWindow(editMode);
            if (editedPersonInfo != null)
                editedPersonInfo.PersonID = personFormInformation.PersonID;

            return editedPersonInfo;
        }


        private PersonFormInformation GetPersonInfoFromSelectedRow()
        {
            PersonFormInformation currentSelectedRowPerson = null;
            try
            {
                string personID = this.PersonsDataGridView.Rows[this.ClickedRowIndex].Cells["PersonID"].Value.ToString();
                string firstName = this.PersonsDataGridView.Rows[this.ClickedRowIndex].Cells["نام"].Value.ToString();
                string lastName = this.PersonsDataGridView.Rows[this.ClickedRowIndex].Cells["نام خانوادگی"].Value.ToString();
                string phoneNumber = this.PersonsDataGridView.Rows[this.ClickedRowIndex].Cells["شماره تماس"].Value.ToString();
                string nationalCode = this.PersonsDataGridView.Rows[this.ClickedRowIndex].Cells["کد ملی"].Value.ToString();

                currentSelectedRowPerson = new PersonFormInformation(personID, firstName, lastName, phoneNumber, nationalCode);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "خطا زمان گرفتن اطلاعات از سطر");
            }

            return currentSelectedRowPerson;
        }


        private void ShowDataGridViewsMenu(int mousePosX, int mousePosY)
        {
            this.dataGridViewContextMenu.Show(this.PersonsDataGridView, new Point(mousePosX, mousePosY));
        }


        private void EmptySearchTextBoxs()
        {
            foreach (Control control in this.SearchSpecificGroupBox.Controls)
            {
                if (control is TextBox)
                    control.Text = "";
            }
        }


        /// <summary>
        /// اگر کاربر در PersonForm ، دکمه ی ذخیره را کلیک کرد و مشکل اعتبار سنجی هم نداشت ، این متد ، مقدار پروپرتیِ PersonFormInformation در کلاس PersonForm
        /// را که حاوی اطلاعات ذخیره سازی هست ، برای ذخیره سازی برمیگرداند وگرنه null را برمیگرداند .
        /// </summary>
        /// <returns></returns>
        private PersonFormInformation ShowPersonWindow()
        {
            using (PersonForm addPersonForm = new PersonForm())
            {
                DialogResult addPersonFormResult = addPersonForm.ShowDialog();
                if (addPersonFormResult == DialogResult.OK)
                    return addPersonForm.PersonFormInformation;
            }

            return null;
        }


        /// <summary>
        /// این متد ، در حالت خاص ، که یا حالت نمایش و یا ویرایش هست ، پنجره ی PersonForm را باز میکند .
        /// </summary>
        /// <param name="openMode"></param>
        /// <returns></returns>
        private PersonFormInformation ShowPersonWindow(IPersonFormOpenMode openMode)
        {
            using (PersonForm addPersonForm = new PersonForm(openMode))
            {
                DialogResult addPersonFormResult = addPersonForm.ShowDialog();
                if (addPersonFormResult == DialogResult.OK)
                    return addPersonForm.PersonFormInformation;
            }

            return null;
        }


        private void SearchByAllFields()
        {
            string firstNameSearchText = this.FirstNameTextBox.Text;
            string lastNameSearchText = this.LastNameTextBox.Text;
            string phoneNumberSearchText = this.PhoneNumberTextBox.Text;
            string nationalCodeSearchText = this.NationalCodeTextBox.Text;

            this.contactWithPhoneBook.SearchByAllFields(firstNameSearchText, lastNameSearchText,
                phoneNumberSearchText, nationalCodeSearchText);
        }


        private void SetPersonCountLabel()
        {
            string countText = "تعداد  :  ";
            this.PersonCountLabel.Text = countText + ((this.PersonsDataGridView.RowCount - 1).ToString());
        }


        #endregion


        ////////////////////////////////////////////////////////////////////////////


        #endregion


        ////////////////////////////////////////////////////////////////////////////




    }
}
