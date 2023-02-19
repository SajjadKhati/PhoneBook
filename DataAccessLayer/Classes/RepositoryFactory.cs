using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    /// <summary>
    /// پیاده سازی الگوی طراحی Factory Method
    /// هر چند چون فقط یک کلاس Repository داریم که از اینترفیس IRepository مشتق میشود و بنابراین این الگو چندان مناسب این مورد نیست اما این کار برای آینده نگری انجام میشود .
    /// بنابراین این کلاس ، وظیفه ی ساختن شی از کلاس Repository که نوع جنریکش از نوع Person هست را دارد.
    ///
    /// همچنین چون این کلاس ، وظیفه ی ساختن شیِ جدیدی از DbConnection را دارد و بنابراین وظیفه ی Dispose کردنِ این شی هم به عهده ی این کلاس قرار میگیرد ،
    /// پس به همین دلیل ، اینترفیس IDisposable را در این کلاس پیاده سازی میکنیم .
    /// </summary>
    public class RepositoryFactory<TEntity> : IDisposable where TEntity : class
    {
        private string databaseFilePath;

        private string connectionString;

        /// <summary>
        /// این متغییر ، برای فلگ کردنِ این هست که آیا برنامه نویس متد dispose را فراخوانی کرد یا در متد تخریب کننده ، اون را صدا زدیم
        /// </summary>
        private bool disposed = false;



        private IDbConnection SqlConnection{ get; set; }




        public RepositoryFactory(string fullDatabaseFilePath)
        {
            if (string.IsNullOrEmpty(fullDatabaseFilePath) == true)
                throw new ArgumentNullException("fullDatabaseFilePath", "آدرس کامل فایل دیتابیس نیاز هست");
            if(File.Exists(fullDatabaseFilePath) == false)
                throw new FileNotFoundException("این فایل وجود ندارد .", fullDatabaseFilePath);

            this.databaseFilePath = fullDatabaseFilePath;
            this.connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""" + this.databaseFilePath +
                                    @""";Integrated Security=True;Connect Timeout=30";
        }




        /// <summary>
        /// چون فقط یک نوع داریم که اینترفیس IRepository را پیاده سازی کرد ، پس در ورودی این متد ، مقداری برای تشخیص شی ساختن ، نمیگیریم .
        /// </summary>
        /// <returns></returns>
        public IRepository<TEntity> GetRepository()
        {
            if (this.SqlConnection == null)
                this.SqlConnection = new SqlConnection(this.connectionString);

            return new Repository<TEntity>(this.SqlConnection);
        }




        public void Dispose()
        {
            this.Dispose(true);
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
                    if (this.SqlConnection != null)
                        this.SqlConnection.Dispose();
                }

                /// فلگ کردن .
                this.disposed = true;
            }
        }


        /// <summary>
        /// متد تخریب کننده ، توسط GC فراخوانی میشود . برای نابود کردن شی Managed .
        /// منظور از نابود کردن ، Dispose کردن نیست .
        /// 
        /// که درون آن متد Dispose را فراخوانی میکنیم که اگه صریحا توسط برنامه نویس اجرا نشد ، در این زمان ، فراخوانی شود .
        /// </summary>
        ~RepositoryFactory()
        {
            this.Dispose(false);
        }


    }
}
