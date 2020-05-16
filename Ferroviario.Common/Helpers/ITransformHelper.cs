using Ferroviario.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ferroviario.Common.Helpers
{
        public interface ITransformHelper
        {
            Service ToService(ServiceResponse serviceResponses);
        }

}
