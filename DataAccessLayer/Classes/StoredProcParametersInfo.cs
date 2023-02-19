using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    internal class StoredProcParametersInfo
    {
        public string ParameterName { get; set; }
        public object ParameterValue { get; set; }
        public DbType ParameterType { get; set; }




        public StoredProcParametersInfo(string parameterName, object parameterValue, DbType parameterType)
        {
            this.ParameterName = parameterName;
            this.ParameterValue = parameterValue;
            this.ParameterType = parameterType;
        }


    }
}
