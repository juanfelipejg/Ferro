using System;
using System.Collections.Generic;
using System.Text;

namespace Ferroviario.Common.Models
{
    public class ServiceResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public TimeSpan InitialHour { get; set; }

        public string InitialStation { get; set; }

        public TimeSpan FinalHour { get; set; }

        public string FinalStation { get; set; }

        public ServiceDetailResponse ServiceDetail { get; set; }

        public TimeSpan Duration => FinalHour - InitialHour;
    }
}
