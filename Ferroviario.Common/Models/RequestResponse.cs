using System;
using System.Collections.Generic;
using System.Text;

namespace Ferroviario.Common.Models
{
    public class RequestResponse
    {
        public int Id { get; set; }

        public RequestTypeResponse Type { get; set; }

        public DateTime InitialDate { get; set; }

        public DateTime InitialDateLocal => InitialDate.ToLocalTime();

        public DateTime FinishDate { get; set; }

        public DateTime FinishDateLocal => FinishDate.ToLocalTime();

        public string Description { get; set; }

        public string State { get; set; }

        public string Comment { get; set; }

        public UserResponse User { get; set; }
    }
}
