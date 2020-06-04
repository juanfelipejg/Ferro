using Ferroviario.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ferroviario.Web.Helpers
{
    public interface IChangeHelper
    {
        Task<bool> CheckHours(UserEntity userEntity, ShiftEntity shiftEntity);

        Task<int> CheckChanges(UserEntity user);
    }
}
