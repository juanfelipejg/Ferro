using Ferroviario.Common.Models;
using System.Collections.Generic;

namespace Ferroviario.Common.Helpers
{
    public class TransformHelper : ITransformHelper
    {
        public Service ToService(ServiceResponse serviceResponse)
        {
            Service service = new Service();

            service.Name = serviceResponse.Name;

            service.ServiceDetails = serviceResponse.ServiceDetail;

            return service;
        }

    }

}
