using BusinessLogicLayer.Modules.Validation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace BusinessLogicLayer.Modules.PhoneBook
{
    /// <summary>
    /// این کلاس با کلاس RelationshipWithRepository ، رابطه ی aggregation دارد.
    /// </summary>
    public class PhoneBook : IPhoneBook<Person>
    {
        #region  Fields


        private DataTableWork dataTableWork;


        private RelationshipWithRepository<Person> relationshipWithRepository;


        /// <summary>
        /// این متغییر ، برای فلگ کردنِ این هست که آیا برنامه نویس متد dispose را فراخوانی کرد یا در متد تخریب کننده ، اون را صدا زدیم
        /// </summary>
        private bool disposed = false;

        #endregion


        ///////////////////////////////////////////////////////////////////////////


        #region  Properties


        public DataTable PersonsDataTable { get; private set; }


        #endregion


        ///////////////////////////////////////////////////////////////////////////


        #region  Methods


        #region  Constractor & Destructor Methods


        public PhoneBook(string fullDatabaseFilePath)
        {
            this.InitializeDataMembers(fullDatabaseFilePath);
        }




        ~PhoneBook()
        {
            this.Dispose(disposing: false);
        }


        #endregion


        ///////////////////////////////////////////////////////////////////////////


        #region  Interfaces Implements Methos


        public IEnumerable<Person> GetAllData()
        {
            IEnumerable<Person> allPersonEnumerable = this.relationshipWithRepository.GetAllDataFromRepository();
            List<Person> allPersonList = Convert.ConvertIenumerableToList(allPersonEnumerable);
            /// وجود این متد در اینجا که خیلی به کار این متد مربوط نمیشود ، و حدودا باعث نقض اولین اصل solid که تک مسئولیتی هست ، میشود ، صرفا بخاطر کمبود وقت هست
            /// وگرنه اینترفیس را باید توسعه بدهم .
            this.dataTableWork.ClearLoadChangeColumnName(allPersonList);
            return allPersonList;
        }


        public IEnumerable<Person> SearchByAllFields(string firstName = "", string lastName = "", string phoneNumber = "", string nationalCode = "")
        {
            IEnumerable<Person> searchedPersonEnumerable = this.relationshipWithRepository.SearchByAllFieldsFromRepository(
                firstName, lastName, phoneNumber, nationalCode);
            List<Person> searchedPersonList = Convert.ConvertIenumerableToList<Person>(searchedPersonEnumerable);
            this.dataTableWork.ClearLoadChangeColumnName(searchedPersonList);
            return searchedPersonList;
        }


        /// <summary>
        /// این متد هر چند اطلاعات را از دیتابیس برمیگرداند اما مستقلا درون DataTable همان جستجو را انجام میدهد .
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Person SearchById(int id)
        {
            Person searchedPerson = this.relationshipWithRepository.SearchByIdFromRepository(id);
            this.dataTableWork.SearchById(id);
            return searchedPerson;
        }



        /// <summary>
        /// برای گرفتن داده از دیتابیس و پر کردنش در DataTable در نخ و Task ای مجزا .
        /// 
        /// اما در حجم پایینِ داده ها ، و کلا در کارهایی که زمان خیلی کمی مصرف میکنند ، متدهایی مثل این که در نخ مجزایی اجرا میشوند ،
        /// بخاطر Context Switch و lock ها ، سرعت انجام کار پایین تری نسبت به اجرای همان متد در یک نخ دارند .
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Person>> GetAllDataAsync()
        {
            IEnumerable<Person> allPersonEnumerableAsync = allPersonEnumerableAsync = await this.relationshipWithRepository.GetAllDataFromRepositoryAsync();

            /// فقط متد زیر در نخ جاری که در اینجا معمولا نخ اصلی هست مجددا فراخوانی میشود یعنی متد زیر یعنی متد ConvertIenumerableToList باعث میشود تا -
            /// تا زمانی که در حال اجراست ، نخ جاری و نخ اصلی ، پاسخگو نباشد .
            /// بنابراین بهتر است که این متد هم در نخی دیگر فراخوانی شود .
            List<Person> allPersonList = Convert.ConvertIenumerableToList(allPersonEnumerableAsync);
            this.dataTableWork.ClearLoadChangeColumnName(allPersonList);

            /// چون انجام عملیات در نخ جدید روی DataTable ، گاها ممکن است باعث ناسازگاری هایی روی کنترلی که به این DataTable مان Binding شد ، شود ،
            /// پس عملیات روی DataTable را مجددا در همان نخ اصلی انجام میدهیم .
            /// 
            /// برای مدیریت استثنا و خطاهایی که در نخ جدید توسط Task ها رخ میدهد ،
            /// باید در قسمت await ای که آن Task را درونش فراخوانی میکنیم و منتظرش میمانیم ، مدیریت کنیم .
            /// و try catch را در آنجا ققرار دهیم .

            ///try
            ///{
            ///    /// در نخی جدید فراخوانی میشود .
            ///    /// وجود این متد در اینجا که خیلی به کار این متد مربوط نمیشود ، و حدودا باعث نقض اولین اصل solid که تک مسئولیتی هست ، میشود ، صرفا بخاطر کمبود وقت هست
            ///    /// وگرنه اینترفیس را باید توسعه بدهم .
            ///
            ///
            ///    await Task.Factory.StartNew(action: new Action<object>(
            ///        this.dataTableWork.ClearLoadChangeColumnNameForAsyncMethod), state: allPersonList);
            ///}
            ///catch (Exception exception)
            ///{
            ///    throw new Exception("پرتاب خطا در نخ جدید ، توسط متد GetAllDataAsync مدیریت شد", exception);
            ///}

            return allPersonList;
        }




        public bool Add(Person type)
        {
            if(CheckValidate.IsAddMethodValidate(type) == false)
                return false;

            bool isInserted = this.relationshipWithRepository.InsertToRepository(
                type.FirstName, type.LastName, type.PhoneNumber, type.NationalCode);

            this.GetAllData();

            return isInserted;
        }


        public bool Edit(Person type)
        {
            if(CheckValidate.IsEditMethodValidate(type) == false)
                return false;

            bool isUpdated = this.relationshipWithRepository.UpdateToRepository(
                type.PersonID, type.FirstName, type.LastName, type.PhoneNumber, type.NationalCode);

            this.GetAllData();

            return isUpdated;
        }


        public bool Delete(int id)
        {
            if (CheckValidate.IsIDValidate(id) == false)
                return false;

            bool isDeleted = this.relationshipWithRepository.DeleteFromRepository(id);

            this.GetAllData();

            return isDeleted;
        }




        /// <summary>
        /// چون معمولا استفاده از این کلاس ، در تمام طول عمر لایه ی UI استفاده میشود ، پس توصیه میشود که این متد ، یا فراخوانی نشود ،
        /// چون بصورت اتوماتیک ، زمانی که destructor ئه شی این کلاس توسط GC فراخوانی میشود ، در آنجا ، این متد فراخوانی میشود
        /// یا اینکه موقع بسته شدن پنجره ی اصلی در UI ، این متد فراخوانی شود .
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void Dispose()
        {
            this.Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }


        protected virtual void Dispose(bool disposing)
        {
            /// چک میکند که آیا قبلا متد Dispose فراخوانی شد یا نه .
            if (this.disposed == false)
            {
                ///  اگر برنامه نویس ، متد dispose را _ صریحا یا توسط استفاده از Using_ فراخوانی کرد ، پس -
                /// فقط در این صورت ، منابعِ Dispose کردنی ای که درون این کلاس ازش شیِ جدیدی ساخته بودیم را Dispose میکنیم .
                /// مثل شی پروپرتیِ SqlConnection که درون این کلاس ازش شیِ جدیدی ساختیم و همچنین شیِ Dispose کردنی هست .
                if (disposing == true)
                {
                    if(this.PersonsDataTable != null)
                        this.PersonsDataTable.Dispose();
                }

                /// فلگ کردن .
                disposed = true;
            }
        }


        #endregion


        ///////////////////////////////////////////////////////////////////////////


        #region  Private Methods


        private void InitializeDataMembers(string fullDatabaseFilePath)
        {
            this.PersonsDataTable = new DataTable();
            this.dataTableWork = new DataTableWork(this.PersonsDataTable);
            this.relationshipWithRepository = new RelationshipWithRepository<Person>(fullDatabaseFilePath);
        }


        #endregion


        #endregion


        ///////////////////////////////////////////////////////////////////////////


    }
}
