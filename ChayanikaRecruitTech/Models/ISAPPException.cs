using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Microsoft.AspNetCore.Mvc.Filters
{
    public interface ISAPPException: IFilterMetadata
    {
        
        void OnException(ExceptionContext context);
        
    }
}
