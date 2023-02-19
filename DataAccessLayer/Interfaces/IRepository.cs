using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    /// <summary>
    /// برای پیاده سازی اصل وارونگی وابستگی در solid، یا همون dependency inversion principle ، تا دو ماژول ، به همدیگه وابسته نشوند ، این اینترفیس ساخته شد
    /// تا این ماژول در لایه ی دیتابیس ، با ماژول یا ماژول های در لایه ی منطق تجاری ، توسط این اینترفیس ارتباط داشته باشن .
    /// 
    /// بخاطر اصل جداسازی اینترفیس ها در اصول solid ، یا همون اصل interface segregation principle ، اینترفیس ها را از هم جدا کردم .
    /// </summary>
    public interface IRepository<TEntity> :  ISyncRepository<TEntity>, IAsyncRepository<TEntity> where TEntity : class
    {

    }
}
