using Ferroviario.Web.Data.Entities;
using Ferroviario.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ferroviario.Web.Helpers
{
    public interface IConverterHelper
    {
        Task<RequestEntity> ToRequestEntityAsync(RequestViewModel model, bool isNew);

        RequestViewModel ToRequestViewModel(RequestEntity requestEntity);
    }
}
