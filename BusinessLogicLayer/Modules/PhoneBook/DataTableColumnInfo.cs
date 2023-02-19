using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Modules.PhoneBook
{
    internal class DataTableColumnInfo
    {
        internal string ColumnName{ get; set; }


        /// <summary>
        /// نام مستعار و جایگزین برای نام ستون
        /// </summary>
        internal string ColumnNameAlias{ get; set; }




        internal DataTableColumnInfo(string columnName, string columnNameAlias)
        {
            this.ColumnName = columnName;
            this.ColumnNameAlias = columnNameAlias;
        }


    }
}
