using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ferroviario.Common.Models
{
    public class ChangeRequest
    {
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public Guid FirstDriverId { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public int FirstShift { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public Guid SecondDriverId { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public int SecondShift { get; set; }

        [Required]
        public string CultureInfo { get; set; }
    }
}
