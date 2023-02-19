using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace DataAccessLayer
{
    /// <summary>
    /// بخاطر پیاده سازی اصل تک مسئولیتی در اصول solid، اعضای این کلاس جدا شدند 
    /// </summary>
    internal class DatabaseWork<TEntity> where TEntity : class
    {
        private IDbConnection DbConnection { get; }




        internal DatabaseWork(IDbConnection dbConnection)
        {
            this.DbConnection = dbConnection;
        }




        /// <summary>
        /// متد ایه برای کار جستجوی
        /// </summary>
        /// <param name="storedProcName"></param>
        /// <param name="storedProcParameters">
        ///این پارامتر میتواند null ارسال شود .
        /// </param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        internal IEnumerable<TEntity> BaseSearch(string storedProcName, DynamicParameters storedProcParameters)
        {
            if (string.IsNullOrEmpty(storedProcName) == true)
                throw new ArgumentNullException("storedProcName", "نام Stored Procedure داده نشده .");

            this.DbConnection.Open();

            IEnumerable<TEntity> searchResault = this.DbConnection.Query<TEntity>(storedProcName, storedProcParameters,
                commandType: CommandType.StoredProcedure);

            this.DbConnection.Close();

            return searchResault;
        }


        /// <summary>
        /// متد پایه برای کار جستجوی ناهنگام
        /// </summary>
        /// <param name="storedProcName"></param>
        /// <param name="storedProcParameters">
        ///این پارامتر میتواند null ارسال شود .
        /// </param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        internal async Task<IEnumerable<TEntity>> BaseSearchAsync(string storedProcName, DynamicParameters storedProcParameters)
        {
            if (string.IsNullOrEmpty(storedProcName) == true)
                throw new ArgumentNullException("storedProcName", "نام Stored Procedure داده نشده .");

            this.DbConnection.Open();

            IEnumerable<TEntity> searchResault = null;
            ///  چون متد QueryAsync در نخ جدید اجرا میشود ، برای مدیریت کردن استثناها و خطاهای آن ، قسمتِ await را درون بلاک try catch قرار دهیم .
            /// و همچنین برای مدیریت خطا در نخ جدید ، متد مان هم نوع Task یا جنریکش را برگرداند
            /// 
            /// اگر در نخ جدید ، یعنی در متد QueryAsync ، خطایی پرتاب شود ، زمانی که برنامه از ویژال استویو در حالت با دیباگ کردن اجرا شود ، خطا ، مدیریت نمیشود .
            /// بلکه فقط زمانی که برنامه را مستقلا اجرا یا بصورت بدون دیباگ از ویژال استودیو اجرا میکنیم ، فقط در این حالت ، خطا مدیریت میشود .
            try
            {
                searchResault = await this.DbConnection.QueryAsync<TEntity>(storedProcName, storedProcParameters,
                    commandType: CommandType.StoredProcedure);
            }
            catch (Exception exception)
            {
                throw new Exception($"خطایی در نخ جدید هنگام جستجو در پایگاه داده رخ داده است  :\n\n{exception.Message}", exception);
            }
            finally
            {
                this.DbConnection.Close();
            }

            return searchResault;
        }


        internal bool BaseExecute(string storedProcName, DynamicParameters storedProcParameters)
        {
            if (string.IsNullOrEmpty(storedProcName) == true)
                throw new ArgumentNullException("storedProcName", "نام Stored Procedure داده نشده .");
            if (storedProcParameters == null)
                throw new ArgumentNullException("storedProcParameters", "پارامتر storedProcParameters داده نشد .");

            this.DbConnection.Open();

            try
            {
                this.DbConnection.Execute(storedProcName, storedProcParameters,
                    commandType: CommandType.StoredProcedure);

                return true;
            }
            catch (SqlException sqlException)
            {
                throw new DataException("زمان اجرای دستورات sql ، خطایی رخ داده .", sqlException);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                this.DbConnection.Close();
            }
            
        }


        internal DynamicParameters CreateStoredProcParameters(List<StoredProcParametersInfo> storedProcParameterList)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            for (int paramsCounter = 0; paramsCounter < storedProcParameterList.Count; paramsCounter++)
            {
                dynamicParameters.Add(storedProcParameterList[paramsCounter].ParameterName, 
                    storedProcParameterList[paramsCounter].ParameterValue, storedProcParameterList[paramsCounter].ParameterType);
            }

            return dynamicParameters;
        }



    }
}
