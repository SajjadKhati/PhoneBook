using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DataAccessLayer
{
    /// <summary>
    /// بخاطر اصل جداسازی اینترفیس ها در اصول solid ، یا همون اصل interface segregation principle ، اینترفیس ها را از هم جدا کردم .
    /// </summary>
    public interface IAsyncRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<IEnumerable<TEntity>> SearchByAllFieldsAsync(string firstName = "", string lastName = "", string phoneNumber = "", string nationalCode = "");
    }
}
