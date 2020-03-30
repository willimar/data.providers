using System;
using System.Collections.Generic;
using System.Text;

namespace data.provider.core.Exceptions
{
    public class DataUpdateException: Exception
    {
        public DataUpdateException(string mesage) : base(mesage)
        {
            
        }
    }
}
