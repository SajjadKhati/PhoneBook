using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FastMember;

namespace BusinessLogicLayer.Modules.PhoneBook
{
    internal class DataTableWork
    {
        ///  برای استفاده از قفل ، باید از شی ای واحد و ترجیحا فقط خواندنی استفاده کنیم نه از متغییر یا فیلدی شی گرا .
        /// بنابراین فیلد مورد نظر حتما static و ترجیحا فقط خواندنی باشد تا از این طریق ، اشاره گر به یک شی را تشخیص دهد .
        private static readonly object lockObject = new object();




        private DataTable DataTable{ get; set; }




        internal DataTableWork(DataTable dataTable)
        {
            if(dataTable == null)
                throw new ArgumentNullException("dataTable", "پارامتر dataTable نمیتواند null باشد .");

            this.DataTable = dataTable;
        }




        /// <summary>
        /// عملیات مربوط به حذف کردن و بارگذاری کردن و تغییر نام سطر و ستون های DataTable
        /// </summary>
        /// <param name="persons"></param>
        internal void ClearLoadChangeColumnName(IEnumerable<Person> persons)
        {
            /// این متد چون در چندین نخ ممکن هست فراخوانی شود بخاطر استفاده از Async پس باید برای نخ جاری ، قفل شود .
            /// 
            ///  برای استفاده از قفل ، باید از شی ای واحد و ترجیحا فقط خواندنی استفاده کنیم نه از متغییر یا فیلدی شی گرا .
            /// بنابراین فیلد مورد نظر حتما static و ترجیحا فقط خواندنی باشد تا از این طریق ، اشاره گر به یک شی را تشخیص دهد .
            lock (lockObject)
            {
                this.ClearAllColumnsAndRowsIfExist();
                this.LoadFromIEnumerable(persons);
                this.ChangeDataTableColumnsName();
            }
        }


        internal void ClearLoadChangeColumnNameForAsyncMethod(object personIenumerable)
        {
            IEnumerable<Person> personIenumConvert = personIenumerable as IEnumerable<Person>;
            if (personIenumConvert == null)
                return;

            this.ClearLoadChangeColumnName(personIenumConvert);
        }


        internal void ClearAllColumnsAndRowsIfExist()
        {
            this.DataTable.Rows.Clear();
            this.DataTable.Columns.Clear();
        }


        internal void LoadFromIEnumerable(IEnumerable<Person> persons)
        {
            if (persons == null)
                throw new ArgumentNullException("persons", "پارامتر persons نمیتواند null باشد .");

            using (IDataReader dataReader = ObjectReader.Create<Person>(persons))
            {
                this.DataTable.Load(dataReader);
            }
        }


        internal void ChangeDataTableColumnsName()
        {
            /// اگر تعداد ستون ها زیاد باشد ، بهتر است از قابلیت reflection در دات نت برای گرفتن نام پروپرتی ها که همان نام ستون است ، استفاده کنیم .
            DataTableColumnInfo[] columnNameAliases = new DataTableColumnInfo[4];
            columnNameAliases[0] = new DataTableColumnInfo("FirstName", "نام");
            columnNameAliases[1] = new DataTableColumnInfo("LastName", "نام خانوادگی");
            columnNameAliases[2] = new DataTableColumnInfo("PhoneNumber", "شماره تماس");
            columnNameAliases[3] = new DataTableColumnInfo("NationalCode", "کد ملی");

            this.ChangeColumnNameByInfos(columnNameAliases);
        }


        private void ChangeColumnNameByInfos(params DataTableColumnInfo[] columnInfos)
        {
            foreach (var columnInfo in columnInfos)
            {
                this.DataTable.Columns[columnInfo.ColumnName].ColumnName = columnInfo.ColumnNameAlias;
            }
        }


        internal void SearchById(int id)
        {
            string searchString = $"PersonID = {id.ToString()}";
            this.DataTable.DefaultView.RowFilter = searchString;
        }








    }
}
