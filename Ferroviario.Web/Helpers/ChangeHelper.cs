using Ferroviario.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ferroviario.Web.Helpers
{
    public class ChangeHelper : IChangeHelper
    {
        public ServiceEntity ToServiceEntity(ShiftEntity shiftEntity)
        {
            return new ServiceEntity
            {

            };
        }
    }
}
