using System;
using System.Collections.Generic;
using System.Text;

namespace EasySave.Exceptions
{
    [Serializable]
    public class BusinessSoftwareRunningException : Exception
    {
        public BusinessSoftwareRunningException()
            : base(String.Format("Business Software is running")) 
        {

        }
    }
}
