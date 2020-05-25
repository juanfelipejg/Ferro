using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ferroviario.Common.Models
{
    public class ChangeRequest
    {
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string FirstDriverId { get; set; }

        public int FirstShift { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string SecondDriverId { get; set; }
    }
}
