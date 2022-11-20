using System;
using System.Collections.Generic;
using System.Text;

namespace EasySave.Exceptions
{
    class CantFindCryptoSoftException : Exception
    {
        public CantFindCryptoSoftException()
            : base(String.Format("Can't find CryptoSoft software, install it and retry"))
        {

        }
    }
}
