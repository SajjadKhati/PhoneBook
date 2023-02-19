using BusinessLogicLayer.Modules.PhoneBook;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhoneBook.ContactWithPhoneBook
{
    /// <summary>
    /// این کلاس برای ارتباط لایه ی UI با ماژول PhoneBook هست .
    /// تا کار جداسازی کارهای مربوط به ارتباط با این ماژول به پروژه های دیگر ، راحت تر و ماژولار سازیِ بهتری انجام گیرد .
    ///
    /// اگر قرار بر استفاده از یک شی برای ارتباط با لایه ی منطق تجاری بود ، بهتر هست که برای این کلاس ، الگوی طراحی Singlton را پیاده سازی کنیم .
    ///
    /// اغلب try catch ها در این کلاس هندل شود .
    /// </summary>
    internal class ContactWithPhoneBook
    {
        internal IPhoneBook<Person> PhoneBook { get; set; }




        internal ContactWithPhoneBook(string fullDatabaseFilePath)
        {
            if (string.IsNullOrEmpty(fullDatabaseFilePath) == true)
                throw new ArgumentNullException("fullDatabaseFilePath", "آدرس کامل فایل دیتابیس نیاز هست");

            this.PhoneBook = PhoneBookFactory.CreatePhoneBook(fullDatabaseFilePath);
        }




        /// <summary>
        /// این متد ، فقط فراخوانی کننده ی متد Dispose شیِ PhoneBook هست .
        /// </summary>
        internal void Dispose()
        {
            this.PhoneBook?.Dispose();
        }




        internal async Task<IEnumerable<Person>> GetAllDataAsync()
        {
            IEnumerable<Person> allPersonEnumerable = null;
            try
            {
                allPersonEnumerable = await this.PhoneBook.GetAllDataAsync();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }

            return allPersonEnumerable;
        }


        internal IEnumerable<Person> SearchByAllFields(string firstName = "", string lastName = "", string phoneNumber = "", string nationalCode = "")
        {
            IEnumerable<Person > searchedPersonEnumerable = null;
            try
            {
                searchedPersonEnumerable = this.PhoneBook.SearchByAllFields(firstName, lastName, phoneNumber, nationalCode);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return searchedPersonEnumerable;
        }




        private void ShowSqlExceptionErrorMessage(DataException dataException)
        {
            if (this.IsUniqueException(dataException) == true)
            {
                string errorMessage = "بعلت درج کلید واحد برای یکی از فیلدها ، داده ، ثبت نشد\nلطفا داده ای متفاوت نسبت به داده های موجود در فیلد مورد نظر ، ثبت کنید";

                MessageBox.Show($"{errorMessage}\n\n{dataException.Message}", "خطای ثبت کلید واحد",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show(dataException.Message, "خطای اجرای دستورات درج در sql",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
        }


        private bool IsUniqueException(DataException dataException)
        {
            if (dataException == null || dataException.InnerException == null)
                return false;

            SqlException sqlException = dataException.InnerException as SqlException;
            return sqlException.Message.Contains("UNIQUE KEY");
        }


        internal bool Add(PersonFormInformation personFormInformation)
        {
            bool isAdded = false;
            try
            {
                long phoneNumberConverted = Convertor.PhoneNumberWithMessage(personFormInformation.PhoneNumber);
                if(phoneNumberConverted < 0)
                    return false;

                Person person = new Person(personFormInformation.FirstName, personFormInformation.LastName,
                    phoneNumberConverted, personFormInformation.NationalCode);

                isAdded = this.PhoneBook.Add(person);
                if(isAdded == true)
                    MessageBox.Show("اطلاعات با موفقیت ثبت شد");
            }
            catch (DataException dataException)
            {
                this.ShowSqlExceptionErrorMessage(dataException);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return isAdded;
        }


        internal bool Edit(PersonFormInformation personFormInformation)
        {
            bool isEdited = false;
            try
            {
                int persinIDConverted = Convertor.StringToUInt32(personFormInformation.PersonID);
                if (persinIDConverted < 0)
                    throw new Exception("شناسه ی شخص  تبدیل نشد .");

                long phoneNumberConverted = Convertor.PhoneNumberWithMessage(personFormInformation.PhoneNumber);
                if (phoneNumberConverted < 0)
                    return false;

                Person person = new Person(persinIDConverted, personFormInformation.FirstName,
                    personFormInformation.LastName, phoneNumberConverted, personFormInformation.NationalCode);

                isEdited = this.PhoneBook.Edit(person);
                if (isEdited == true)
                    MessageBox.Show("اطلاعات با موفقیت ویرایش شد");
            }
            catch (DataException dataException)
            {
                this.ShowSqlExceptionErrorMessage(dataException);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return isEdited;
        }



        internal bool Delete(string personID)
        {
            bool isDeleted = false;
            try
            {
                int persinIDConverted = Convertor.StringToUInt32(personID);
                if (persinIDConverted < 0)
                    throw new Exception("شناسه ی شخص  تبدیل نشد .");

                isDeleted = this.PhoneBook.Delete(persinIDConverted);
                if(isDeleted == true)
                    MessageBox.Show("اطلاعات با موفقیت حذف شد");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return isDeleted;
        }




    }
}
