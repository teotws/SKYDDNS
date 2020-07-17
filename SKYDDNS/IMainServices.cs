using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SKYDDNS
{
    public interface IMainServices
    {
        Task<bool> InitTaskAsnyc();
    }
}
