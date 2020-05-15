using System;
using System.Collections.Generic;
using System.Text;

namespace Ferroviario.Common.Models
{
    public class ShiftResponse
    {
        public int Id { get; set; }

        public UserResponse User { get; set; }

        public ServiceResponse Service { get; set; }

        public DateTime Date { get; set; }

        public DateTime DateLocal => Date.ToLocalTime();
    }
}
