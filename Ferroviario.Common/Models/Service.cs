using System;
using System.Collections.Generic;
using System.Text;

namespace Ferroviario.Common.Models
{
    public class Service : ServiceDetailResponse
    {
        public string Name { get; set; }

        public ServiceDetailResponse ServiceDetails { get; set; }
    }

}
