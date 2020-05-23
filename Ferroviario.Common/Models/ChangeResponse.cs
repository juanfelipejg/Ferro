using System;
using System.Collections.Generic;
using System.Text;

namespace Ferroviario.Common.Models
{
    public class ChangeResponse
    {
        public int Id { get; set; }

        public DateTime Date => DateTime.Today.AddDays(1).ToUniversalTime();

        public DateTime DateLocal => Date.ToLocalTime();

        public UserResponse FirstDriver { get; set; }

        public ShiftResponse FirstDriverService { get; set; }

        public UserResponse SecondDriver { get; set; }

        public ShiftResponse SecondDriverService { get; set; }
    }
}
