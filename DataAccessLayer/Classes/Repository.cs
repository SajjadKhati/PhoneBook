using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    /// <summary>
    /// این کلاس ، الگوی Repository را برای دیتابیس ، پیاده سازی میکند .
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        #region Fields

        private DatabaseWork<TEntity> databaseWork;

        #endregion


        ///////////////////////////////////////////////////////////////////////////


        #region Properties


        private IDbConnection DbConnection { get;}


        #endregion


        ///////////////////////////////////////////////////////////////////////////


        #region  Methods


        #region  Constractor Methods


        public Repository(IDbConnection dbConnection)
        {
            this.DbConnection = dbConnection;
            this.databaseWork = new DatabaseWork<TEntity>(this.DbConnection);
        }


        #endregion


        ///////////////////////////////////////////////////////////////////////////


        #region  Interfaces Impliments Methods


        public IEnumerable<TEntity> GetAll()
        {
            string storedProcName = "SelectAllPersonStoredProc";
            return this.databaseWork.BaseSearch(storedProcName, null);
        }


        public IEnumerable<TEntity> SearchByAllFields(string firstName = "", string lastName = "", string phoneNumber = "", string nationalCode = "")
        {
            List<StoredProcParametersInfo> storedProcParameterList = ListGenerator.CreatList(
                new StoredProcParametersInfo("@firstNameParam", firstName, DbType.String),
                new StoredProcParametersInfo("@lastNameParam", lastName, DbType.String) ,
                new StoredProcParametersInfo("@phoneNumberParam", phoneNumber, DbType.AnsiString) ,
                new StoredProcParametersInfo("@nationalCodeParam", nationalCode, DbType.AnsiString)
            );

            string storedProcName = "SearchPersonByAllFieldsStoredProc";
            return this.SearchByStoredProcParameters(storedProcParameterList, storedProcName);
        }


        public IEnumerable<TEntity> SearchByID(int id)
        {
            List<StoredProcParametersInfo> storedProcParameterList = ListGenerator.CreatList(
                new StoredProcParametersInfo("@personID", id, DbType.Int32));

            string storedProcName = "SearchOnePersonByIDStoredProc";
            return this.SearchByStoredProcParameters(storedProcParameterList, storedProcName);
        }




        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            string storedProcName = "SelectAllPersonStoredProc";
            /// چون خود این متدِ GetAllAsync ، درون نخ جاری (اصلی) اجرا میشود ، نیازی به try catch کردنِ قسمتِ await در اینجا ، چندان ضروری نیست
            /// و میتوان در متد فراخوانی کننده ی این متد یا متد ی که در چندین پشته ی قبلی ای قرار دارد که این متد را فراخوانی میکند ، try catch را در صورت نیاز ، در آنجا قرار داد.
            /// همچنین بخاطر کمبود وقت 
            return await this.databaseWork.BaseSearchAsync(storedProcName, null);
        }


        public async Task<IEnumerable<TEntity>> SearchByAllFieldsAsync(
            string firstName = "", string lastName = "", string phoneNumber = "", string nationalCode = "")
        {
            List<StoredProcParametersInfo> storedProcParameterList = ListGenerator.CreatList(
                new StoredProcParametersInfo("@firstNameParam", firstName, DbType.String),
                new StoredProcParametersInfo("@lastNameParam", lastName, DbType.String),
                new StoredProcParametersInfo("@phoneNumberParam", phoneNumber, DbType.AnsiString),
                new StoredProcParametersInfo("@nationalCodeParam", nationalCode, DbType.AnsiString)
            );

            string storedProcName = "SearchPersonByAllFieldsStoredProc";
            /// چون خود این متدِ GetAllAsync ، درون نخ جاری (اصلی) اجرا میشود ، نیازی به try catch کردنِ قسمتِ await در اینجا ، چندان ضروری نیست
            /// و میتوان در متد فراخوانی کننده ی این متد یا متد ی که در چندین پشته ی قبلی ای قرار دارد که این متد را فراخوانی میکند ، try catch را در صورت نیاز ، در آنجا قرار داد.
            /// همچنین بخاطر کمبود وقت 
            return await this.SearchByStoredProcParametersAsync(storedProcParameterList, storedProcName);
        }




        public bool Insert(string firstName, string lastName, long phoneNumber, string nationalCode = "")
        {
            if (string.IsNullOrEmpty(firstName) == true)
                throw new ArgumentNullException("firstName", "پارامتر نام شخص داده نشده .");
            if (string.IsNullOrEmpty(lastName) == true)
                throw new ArgumentNullException("lastName", "پارامتر نام خانوادگی شخص ، داده نشده .");

            List<StoredProcParametersInfo> storedProcParameterList = ListGenerator.CreatList
                (new StoredProcParametersInfo("@firstNameParam", firstName, DbType.String) ,
                    new StoredProcParametersInfo("@lastNameParam", lastName, DbType.String) ,
                    new StoredProcParametersInfo("@phoneNumberParam", phoneNumber, DbType.Int64) ,
                    new StoredProcParametersInfo("@nationalCodeParam", nationalCode, DbType.AnsiString)
                    );

            string storedProcName = "InsertIntoPersonStoredProc";
            return this.ExecuteByStoredProcParameters(storedProcParameterList, storedProcName);
        }


        public bool Update(int id, string firstName, string lastName, long phoneNumber, string nationalCode = "")
        {
            if (string.IsNullOrEmpty(firstName) == true)
                throw new ArgumentNullException("firstName", "پارامتر نام شخص داده نشده .");
            if (string.IsNullOrEmpty(lastName) == true)
                throw new ArgumentNullException("lastName", "پارامتر نام خانوادگی شخص ، داده نشده .");

            List<StoredProcParametersInfo> storedProcParameterList = ListGenerator.CreatList(
                new StoredProcParametersInfo("@personIDParam", id, DbType.Int32) ,
                new StoredProcParametersInfo("@firstNameParam", firstName, DbType.String) ,
                new StoredProcParametersInfo("@lastNameParam", lastName, DbType.String) ,
                new StoredProcParametersInfo("@phoneNumberParam", phoneNumber, DbType.Int64) ,
                new StoredProcParametersInfo("@nationalCodeParam", nationalCode, DbType.AnsiString));

            string storedProcName = "UpdatePersonStoredProc";
            return this.ExecuteByStoredProcParameters(storedProcParameterList, storedProcName);
        }


        public bool Delete(int id)
        {
            List<StoredProcParametersInfo> storedProcParameterList = ListGenerator.CreatList(
                new StoredProcParametersInfo("@personIDParam", id, DbType.Int32));

            string storedProcName = "DeletePersonStoredProc";
            return this.ExecuteByStoredProcParameters(storedProcParameterList, storedProcName);
        }


        #endregion


        ///////////////////////////////////////////////////////////////////////////


        #region  Private Methods


        private IEnumerable<TEntity> SearchByStoredProcParameters
            (List<StoredProcParametersInfo> storedProcParameterList, string storedProcName)
        {
            if (storedProcParameterList == null || storedProcParameterList.Count < 1)
                throw new ArgumentNullException("storedProcParameterList", "پارامتر storedProcParameterList مقداردهی نشده یا عضوی ندارد");
            if (string.IsNullOrEmpty(storedProcName) == true)
                throw new ArgumentNullException("storedProcName", "پارامتر storedProcName نمیتواند خالی باشد");

            DynamicParameters storedProcParameters = this.databaseWork.CreateStoredProcParameters(storedProcParameterList);
            return this.databaseWork.BaseSearch(storedProcName, storedProcParameters);
        }


        private async Task<IEnumerable<TEntity>> SearchByStoredProcParametersAsync
            (List<StoredProcParametersInfo> storedProcParameterList, string storedProcName)
        {
            if (storedProcParameterList == null || storedProcParameterList.Count < 1)
                throw new ArgumentNullException("storedProcParameterList", "پارامتر storedProcParameterList مقداردهی نشده یا عضوی ندارد");
            if (string.IsNullOrEmpty(storedProcName) == true)
                throw new ArgumentNullException("storedProcName", "پارامتر storedProcName نمیتواند خالی باشد");

            DynamicParameters storedProcParameters = this.databaseWork.CreateStoredProcParameters(storedProcParameterList);
            return await this.databaseWork.BaseSearchAsync(storedProcName, storedProcParameters);
        }


        private bool ExecuteByStoredProcParameters(List<StoredProcParametersInfo> storedProcParameterList, string storedProcName)
        {
            if (storedProcParameterList == null || storedProcParameterList.Count < 1)
                throw new ArgumentNullException("storedProcParameterList", "پارامتر storedProcParameterList مقداردهی نشده یا عضوی ندارد");
            if (string.IsNullOrEmpty(storedProcName) == true)
                throw new ArgumentNullException("storedProcName", "پارامتر storedProcName نمیتواند خالی باشد");

            DynamicParameters storedProcParameters = this.databaseWork.CreateStoredProcParameters(storedProcParameterList);
            return this.databaseWork.BaseExecute(storedProcName, storedProcParameters);
        }


        #endregion


        ///////////////////////////////////////////////////////////////////////////


        #endregion


        ///////////////////////////////////////////////////////////////////////////



    }
}
