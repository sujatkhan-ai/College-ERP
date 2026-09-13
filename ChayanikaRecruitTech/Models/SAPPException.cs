using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChayanikaRecruitTech.Models
{
    public class SAPPException: Exception
    {
        public SAPPException()
        { }

        public SAPPException(string message)
            : base(message)
        { }

        public SAPPException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
